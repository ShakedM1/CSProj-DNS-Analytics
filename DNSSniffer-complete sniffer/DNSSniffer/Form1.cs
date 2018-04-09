using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;
using System.Windows.Forms;
using System.Windows;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System.IO;
using System.Text.RegularExpressions;

namespace DNSSniffer
{

    public partial class Form1 : Form
    {
        public static bool savelog = true;
        public int CaptureIndex;
        //get all live capture devices
        IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
        public bool sniffing = false;
        public static string logpath = GetUniqueFilePath(@"C:\log.txt");
        public Form1()
        {
            InitializeComponent();
            TrainModel();
            MessageBox.Show(CheckUrl("http://imageortho.com/images/home").ToString());
            GetCaptureDevices();
        }


        private void SelectInterface_Click(object sender, EventArgs e)
        {
            int selected;

            if (InterfaceListBox.SelectedItem != null && !sniffing)
            {

                selected = InterfaceListBox.SelectedIndex;
                MessageBox.Show(selected.ToString());
                CaptureIndex = selected;
                InterfaceStatus.Text = "Status: " + InterfaceListBox.SelectedItem.ToString() + " Selected";
            }
            else
            {
                if (sniffing)
                    MessageBox.Show("You have to stop sniffing to change interface!");
                else
                    MessageBox.Show("Please select an interface");
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }//

        public void GetCaptureDevices()
        {
            //check if there are any available
            if (allDevices.Count == 0)
            {
                MessageBox.Show("No interfaces found! Make sure WinPcap is installed.");
                return;
            }

            // append all the network interfaces to the listbox
            for (int i = 0; i != allDevices.Count; ++i)
            {
                LivePacketDevice device = allDevices[i];
                string str = "";
                if (device.Description != null)
                    str += ((i + 1) + ". " + device.Description);
                else
                    str += ((i + 1) + ". " + " (No description available)");

                InterfaceListBox.Items.Add(str);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SniffButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
                //create the log file with introduction line
                if (savelog)
                {
                    string str = "Log File Created " + DateTime.Now.ToString() + " Network Interface: " + InterfaceListBox.SelectedItem.ToString();
                    string path = GetUniqueFilePath(logpath);
                    System.IO.File.WriteAllText(path, str);
                    logpath = path;
                }
                //
                SniffButton.Text = "Stop Sniffing";
                SniffingStatus.Text = "Status: Sniffing";
                sniffing = true;
                //show the table
                ReportListView.Visible = true;
            }
            else if (sniffing)
            {
                backgroundWorker1.CancelAsync();
                SniffButton.Text = "Start Sniffing";
                SniffingStatus.Text = "Not Sniffing";
                sniffing = false;
            }
        }

        //To be activated only on DNS packets!(port 53 tcp or udp)
        public static DnsDomainName[] ExtractDnsQueries(Packet pkt)
        {
            IpV4Datagram ip = pkt.Ethernet.IpV4;
            UdpDatagram udp = ip.Udp;
            DnsDatagram dns = udp.Dns;
            //check if dns datagram is query
            if (dns.IsQuery)
            {
                //No check for query count is needed becuase the number of queries is equal to querycount
                DnsDomainName[] names = new DnsDomainName[dns.QueryCount];
                int i = 0;
                foreach (DnsQueryResourceRecord record in dns.Queries)
                {
                    names[i] = record.DomainName;
                    i++;
                }
                return names;
            }
            return null;
            //returns null if dns is not query

        }//extreactqueries

        public void AddToListView(string time, string ip, string report)
        {
            ListViewItem item = new ListViewItem();
            item.Text = time;
            //item.SubItems.Add(time);
            item.SubItems.Add(ip);
            item.SubItems.Add(report);
            ReportListView.Items.Add(item);
            ReportListView.Items[ReportListView.Items.Count - 1].EnsureVisible();
        }

        private void ReportListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Take the selected adapter
            PacketDevice selectedDevice = allDevices[CaptureIndex];

            // Open the device
            using (PacketCommunicator communicator =
                selectedDevice.Open(65536,                                  // portion of the packet to capture
                                                                            // 65536 guarantees that the whole packet will be captured on all the link layers
                                    PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                    1000))                                  // read timeout
            {
                MessageBox.Show("Listening on " + selectedDevice.Description + "...");

                // Retrieve the packets
                Packet packet;
                do
                {
                    PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
                    using (BerkeleyPacketFilter filter = communicator.CreateFilter("port 53"))
                    {
                        // Set the filter
                        communicator.SetFilter(filter);
                    }
                    switch (result)
                    {
                        case PacketCommunicatorReceiveResult.Timeout:
                            // Timeout elapsed
                            continue;
                        case PacketCommunicatorReceiveResult.Ok:

                            string timestamp = packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff");

                            //Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" +
                            //                  packet.Length);
                            DnsDomainName[] names = ExtractDnsQueries(packet);
                            IpV4Datagram ip = packet.Ethernet.IpV4;
                            UdpDatagram udp = ip.Udp;
                            DnsDatagram dns = udp.Dns;


                            if (names != null)
                            {

                                foreach (DnsDomainName domain in names)
                                {

                                    //if (domain == null)
                                    //    Console.WriteLine("No queries");

                                    if (domain != null)
                                    {
                                        string url = domain.ToString();
                                        bool malicious =  CheckUrl(url); //bool storing if the url is potentially malicious
                                        if (malicious)
                                        {
                                            CloseBrowsers();
                                            url += " Potentially Malicious, browser terminated";
                                        }//malicious url detected
                                        AddToListView(timestamp, ip.Source.ToString(), url);
                                        //add the packet details to the log file
                                        if (savelog)
                                        {
                                            string str = timestamp + " " + ip.Source.ToString() + " " + url;
                                            Appendlog(logpath, str);
                                        }
                                        //Console.WriteLine(domain.ToString());
                                    }
                                }
                            }

                            break;
                        default:
                            throw new InvalidOperationException("The result " + result + " should never be reached here");
                    }
                } while (!backgroundWorker1.CancellationPending);
            }

        }//background worker

        private void ReportListView_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            ReportListView.Items.Clear();
        }

        public static string GetUniqueFilePath(string filepath)
        {
            if (File.Exists(filepath))
            {
                string folder = Path.GetDirectoryName(filepath);
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string extension = Path.GetExtension(filepath);
                int number = 1;

                Match regex = Regex.Match(filepath, @"(.+) \((\d+)\)\.\w+");

                if (regex.Success)
                {
                    filename = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }

                do
                {
                    number++;
                    filepath = Path.Combine(folder, string.Format("{0} ({1}){2}", filename, number, extension));
                }
                while (File.Exists(filepath));
            }

            return filepath;
        }//getunuiquepath

        public void Appendlog(string path, string content)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(content);
            }
        }//appendlog

        public static void CloseBrowsers()
        {
            CSharp2Python py = new CSharp2Python();
            //create a temp .txt file for text manipulation
            string str = py.ExecuteCommandWithReturn("tasklist");
            string temppath = @"\temptask.txt";
            Console.WriteLine(str);
            System.IO.File.WriteAllText(@"\temptask.txt", str);
            string[] lines = System.IO.File.ReadAllLines(temppath);
            //check each proccess for it being a browser and if so close it
            foreach (string line in lines)
            {
                if (line.Contains("chrome.exe"))
                {
                    Console.WriteLine("*************************");
                    Console.WriteLine("there's chrome");
                    py.ExecuteCommand("TASKKILL /F /IM chrome.exe");
                }//chrome
                if (line.Contains("iexplore.exe"))
                {
                    Console.WriteLine("*************************");
                    Console.WriteLine("theres explorer");
                    py.ExecuteCommand("TASKKILL /F /IM iexplore.exe");
                }//explorer                                  
            }
            //delete the temp file
            System.IO.File.Delete(temppath);
        }//closebrowsers
        public static bool CheckUrl(string url)
        {
            CSharp2Python py = new CSharp2Python();
            string path = GetScriptPath("");
            string result = py.ExecuteCommandWithReturn("cd " + path + "&" + "python -c \"import classify; classify.checkurl('" + url + "')\"");
            if (result.Contains("1"))
                return true;
            else
                return false;
        }//checkurl

        public static void TrainModel()
        {
            string str = @"..\..\TrainedModel\complete_model.sav";
            if (!File.Exists(str))
            {
                CSharp2Python py = new CSharp2Python();
                MessageBox.Show("creating model...");
                string p1 = "\"" + GetScriptPath("predicturl.py") + "\"";
                py.ExecuteScript(p1);
            }
        }//Train Model

        public static string GetScriptPath(string scriptname)
        {
            string path = Path.GetFullPath(@"..\..\Scripts\" + scriptname);
            return path;
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            //only allow settings change if not sniffing
            if (!sniffing)
            {
                Form2 settingswindow = new Form2();
                if (settingswindow.ShowDialog(this) == DialogResult.OK)
                { MessageBox.Show("Settings Applied"); }
                else { MessageBox.Show("Error:Settings did not apply properly"); }
                settingswindow.Dispose();
                //settingswindow.Show();
            }
            else
                MessageBox.Show("To access settings, stop sniffing.");
        }//settingsbtn

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

