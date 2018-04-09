using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSSniffer
{
    class CSharp2Python
    {

        public CSharp2Python()
        {
        }

        public string PythonPath()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "cmd.exe";
            start.Arguments = "/c where python";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }//pythonpath
        /// <summary>
        /// executes python script with given path and returns output
        /// </summary>
        /// <param name="scriptpath"></param>
        /// <returns></returns>
        public string ExecuteScriptWithReturn(string scriptpath)
        {
            if (scriptpath != null)
            {
                try
                {
                    string path = PythonPath();
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.FileName = path;
                    start.Arguments = string.Format("{0}", scriptpath);
                    start.UseShellExecute = false;
                    start.RedirectStandardOutput = true;
                    using (Process process = Process.Start(start))
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "no command";
        }//exec

        /// <summary>
        /// executes python script with given path
        /// </summary>
        /// <param name="scriptpath"></param>
        public void ExecuteScript(string scriptpath)
        {
            if (scriptpath != null)
            {
                try
                {
                    string path = PythonPath();
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/c python " +scriptpath;                    
                    process.StartInfo = startInfo;
                    process.Start();                    
                    process.WaitForExit();
                }
                catch (Exception ex)
                { }
            }//exec
        }//execscript

        /// <summary>
        /// executes command on cmd and returns output
        /// </summary>
        /// <param name="command"></param>
        public string ExecuteCommandWithReturn(string command)
        {
            if (command != null)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "cmd.exe";
                start.Arguments = "/c " + command;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                try
                {


                    using (Process process = Process.Start(start))
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            return result;
                        }
                    }
                }//try
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "no command added!";
        }//cmdreturn   
                    
        /// <summary>
        /// executes cmd comment 
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteCommand(string command)
        {
            if (command != null)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/c " + command;
                    process.StartInfo = startInfo;
                    process.Start();
                }
                catch (Exception ex)
                { }
            }
        }
    }
}
