import os
import sys
import re
import matplotlib
import pandas as pd
import numpy as np
from os.path import splitext
import ipaddress as ip
import tldextract
import whois
import datetime
from urllib.parse import urlparse

if __name__ == "__main__":
    my_path = os.path.abspath(os.path.dirname(__file__))
    ds_path = os.path.join(my_path, r"..\Resources\dataset.csv")
    df = pd.read_csv(ds_path)
    #df=df.sample(frac=1)
    df = df.sample(frac=1).reset_index(drop=True)
    print(df.head())##first 5 examples of dataframe 
    print("the number of data points in data set  "+str(len(df)))

Suspicious_TLD=['webcam','click','yokohama','men','zip','cricket','link','work','party','gq','kim','country','science','tk']
Suspicious_Domain=['luckytime.co.kr','mattfoll.eu.interia.pl','trafficholder.com','dl.baixaki.com.br','bembed.redtube.comr','tags.expo9.exponential.com','deepspacer.com','funad.co.kr','trafficconverter.biz']

	
# Method to count number of dots
def countdots(url):  
    return url.count('.')

    # Method to count number of delimeters
def countdelim(url):
    count = 0
    delim=[';','_','?','=','&']
    for each in url:
        if each in delim:
            count = count + 1
    
    return count

    # Is IP addr present as th hostname, let's validate

import ipaddress as ip #works only in python 3

def isip(uri):
    try:
        if ip.ip_address(uri):
            return 1
    except:
        return 0



#method to check the presence of hyphens
def isPresentHyphen(url):
    return url.count('-')


#method to check the presence of @

def isPresentAt(url):
    return url.count('@')

def isPresentDSlash(url):
    return url.count('//')

def countSubDir(url):
    return url.count('/')

def get_ext(url):
    """Return the filename extension from url, or ''."""
    
    root, ext = splitext(url)
    return ext

def countSubDomain(subdomain):
    if not subdomain:
        return 0
    else:
        return len(subdomain.split('.'))

def countQueries(query):
    if not query:
        return 0
    else:
        return len(query.split('&'))

'''
featureSet = pd.DataFrame(columns=('url','no of dots','presence of hyphen','len of url','presence of at',\
'presence of double slash','no of subdir','no of subdomain','len of domain','no of queries','is IP','presence of Suspicious_TLD',\
'presence of suspicious domain','create_age(months)','expiry_age(months)','update_age(days)','country','file extension','label'))'''

featureSet = pd.DataFrame(columns=('url','no of dots','presence of hyphen','len of url','presence of at',\
'presence of double slash','no of subdir','no of subdomain','len of domain','no of queries','is IP','presence of Suspicious_TLD',\
'presence of suspicious domain','label'))

from urllib.parse import urlparse
import tldextract
def getFeatures(url, label): 
    result = []
    url = str(url)
    
    #add the url to feature set
    result.append(url)
    
    #parse the URL and extract the domain information
    path = urlparse(url)
    ext = tldextract.extract(url)
    
    #counting number of dots in subdomain    
    result.append(countdots(ext.subdomain))
    
    #checking hyphen in domain   
    result.append(isPresentHyphen(path.netloc))
    
    #length of URL    
    result.append(len(url))
    
    #checking @ in the url    
    result.append(isPresentAt(path.netloc))
    
    #checking presence of double slash    
    result.append(isPresentDSlash(path.path))
    
    #Count number of subdir    
    result.append(countSubDir(path.path))
    
    #number of sub domain    
    result.append(countSubDomain(ext.subdomain))
    
    #length of domain name    
    result.append(len(path.netloc))
    
    #count number of queries    
    result.append(len(path.query))
    
    #Adding domain information
    
    #if IP address is being used as a URL     
    result.append(isip(ext.domain))
    
    #presence of Suspicious_TLD
    result.append(1 if ext.suffix in Suspicious_TLD else 0)
    
    #presence of suspicious domain
    result.append(1 if '.'.join(ext[1:]) in Suspicious_Domain else 0 )
     
    '''
      
    #Get domain information by asking whois
    domain = '.'.join(ext[1:])
    w = whois.whois(domain)
    
    avg_month_time=365.2425/12.0
    
                  
    #calculate creation age in months
                  
    if w.creation_date == None or type(w.creation_date) is str :
        result.append(-1)
        #training_df['create_age(months)'] = -1
    else:
        if(type(w.creation_date) is list): 
            create_date=w.creation_date[-1]
        else:
            create_date=w.creation_date

        if(type(create_date) is datetime.datetime):
            today_date=datetime.datetime.now()
            create_age_in_mon=((today_date - create_date).days)/avg_month_time
            create_age_in_mon=round(create_age_in_mon)
            result.append(create_age_in_mon)
            #training_df['create_age(months)'] = create_age_in_mon
            
        else:
            result.append(-1)
            #training_df['create_age(months)'] = -1
    
    #calculate expiry age in months
                  
    if(w.expiration_date==None or type(w.expiration_date) is str):
        #training_df['expiry_age(months)'] = -1
        result.append(-1)
    else:
        if(type(w.expiration_date) is list):
            expiry_date=w.expiration_date[-1]
        else:
            expiry_date=w.expiration_date
        if(type(expiry_date) is datetime.datetime):
            today_date=datetime.datetime.now()
            expiry_age_in_mon=((expiry_date - today_date).days)/avg_month_time
            expiry_age_in_mon=round(expiry_age_in_mon)
            #training_df['expiry_age(months)'] = expiry_age_in_mon
            #### appending  in months Appended to the Vector
            result.append(expiry_age_in_mon)
        else:
            #training_df['expiry_age(months)'] = -1
            result.append(-1)#### expiry date error so append -1

    #find the age of last update
                  
    if(w.updated_date==None or type(w.updated_date) is str):
        #training_df['update_age(days)'] = -1
        result.append(-1)
    else:
        if(type(w.updated_date) is list):
            update_date=w.updated_date[-1]
        else:
            update_date=w.updated_date
        if(type(update_date) is datetime.datetime):
            today_date=datetime.datetime.now()
            update_age_in_days=((today_date - update_date).days)
            result.append(update_age_in_days)
            #training_df['update_age(days)'] = update_age_in_days #### appending updated age in days Appended to the Vector
        else:
            result.append(-1)
            #training_df['update_age(days)'] = -1
    
    #find the country who is hosting this domain
    if(w.country == None):
        #training_df['country'] = "None"
        result.append("None")
    else:
        #training_df['country'] = w.country
        result.append(w.country)
     ''' 
    
    #result.append(get_ext(path.path))
    result.append(str(label))
    return result
#done with preparing the features

if __name__ == "__main__":
    for i in range(len(df)):
        features = getFeatures(df["URL"].loc[i], df["Lable"].loc[i])    
        featureSet.loc[i] = features



    print("the first five rows and features  "+str(featureSet.head()))

    X = featureSet.drop(['url','label'],axis=1).values
    y = featureSet['label'].values

    import sklearn.ensemble as ek
    from sklearn import model_selection#, tree, linear_model
    from sklearn.feature_selection import SelectFromModel
    from sklearn.externals import joblib
    #from sklearn.naive_bayes import GaussianNB
    from sklearn.metrics import confusion_matrix
    from sklearn.pipeline import make_pipeline
    from sklearn import preprocessing
    #from sklearn import svm
    #from sklearn.linear_model import LogisticRegression

    #training and cross validation
    model = { #"DecisionTree":tree.DecisionTreeClassifier(max_depth=10),
             "RandomForest":ek.RandomForestClassifier(n_estimators=50),
             #"Adaboost":ek.AdaBoostClassifier(n_estimators=50),
             #"GradientBoosting":ek.GradientBoostingClassifier(n_estimators=50),
             #"GNB":GaussianNB(),
             #"LogisticRegression":LogisticRegression()   
    }

    X_train, X_test, y_train, y_test = model_selection.train_test_split(X, y ,test_size=0.2)
    '''
    print()
    print("scores:")
    results = {}
    for algo in model:
        clf = model[algo] 
        clf.fit(X_train,y_train)
        score = clf.score(X_test,y_test)
        print ("%s : %s " %(algo, score))
        results[algo] = score




    #after scores for different algos are computed, choose winner
    winner = max(results, key=results.get)
    print("winner is: "+winner)

    clf = model[winner]
    res = clf.predict(X)
    mt = confusion_matrix(y, res)
    print("False positive rate : %f %%" % ((mt[0][1] / float(sum(mt[0])))*100))
    print('False negative rate : %f %%' % ( (mt[1][0] / float(sum(mt[1]))*100)))
      
      #commented beacause functions for cross-validation are obsolete and raise erorrs (sklearn moved the functions and changed the cv iterators in a new class)
    '''

    clf=model["RandomForest"]
    clf.fit(X_train,y_train)
    score = clf.score(X_test,y_test)
    print ("%s : %s " %("RandomForest", score))
    res =clf.predict(X)
    mt=confusion_matrix(y, res)
    print("False positive rate : %f %%" % ((mt[0][1] / float(sum(mt[0])))*100))
    print('False negative rate : %f %%' % ( (mt[1][0] / float(sum(mt[1]))*100)))


    #now we save the model for use in app
    import pickle

    #for unpickling python version 3.6.4
    my_path = os.path.abspath(os.path.dirname(__file__))
    p_path = os.path.join(my_path, r"..\TrainedModel\complete_model.sav")
    with open(p_path, 'wb') as f:
        pickle.dump(clf, f)

	