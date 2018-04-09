import os
import pandas as pd
import pickle
from predicturl import getFeatures as set_features

#load the model
my_path = os.path.abspath(os.path.dirname(__file__))
p_path = os.path.join(my_path, r"..\TrainedModel\complete_model.sav")
loaded_model = pickle.load(open(p_path, 'rb'))

#predict

def checkurl(address):
  "checks if the following url is malicious 1=malicious 0=safe"
  result = pd.DataFrame(columns=('url','no of dots','presence of hyphen','len of url','presence of at',\
 'presence of double slash','no of subdir','no of subdomain','len of domain','no of queries','is IP','presence of Suspicious_TLD',\
 'presence of suspicious domain','label'))
  results = set_features(address, '0')
  result.loc[0] = results
  result = result.drop(['url','label'],axis=1).values
  str = loaded_model.predict(result)
  print(str)
  return str




