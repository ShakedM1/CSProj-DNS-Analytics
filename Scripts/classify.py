import sklearn.ensemble as ek
from sklearn import model_selection, tree, linear_model
from sklearn.feature_selection import SelectFromModel
from sklearn.externals import joblib
from sklearn.naive_bayes import GaussianNB
from sklearn.metrics import confusion_matrix
from sklearn.pipeline import make_pipeline
from sklearn import preprocessing
import os
import sys
import re
import pandas as pd
import numpy as np
import pickle
from predicturl import getFeatures as set_features

#load the model
loaded_model = pickle.load(open(r"C:\Users\shaked\Desktop\complete_model.sav", 'rb'))
print(loaded_model)

#predict

def checkurl(address):
  "checks if the following url is malicious"
  result = pd.DataFrame(columns=('url','no of dots','presence of hyphen','len of url','presence of at',\
 'presence of double slash','no of subdir','no of subdomain','len of domain','no of queries','is IP','presence of Suspicious_TLD',\
 'presence of suspicious domain','label'))
  results = set_features(address, '1')
  result.loc[0] = results
  result = result.drop(['url','label'],axis=1).values
  str = loaded_model.predict(result)
  print(str)
  if str == '1':
       return "malicious"
  
  else:
       return "not phishing"

#execute
print(checkurl('https://totems.online/paypal/home/webapps/a65dc/websrc...'))
