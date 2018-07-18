import urllib.request
import json
import sys
from urllib.error import HTTPError
from flask_restful import Resource
import os.path

class GitHubApi(Resource):
    __apiToken   = None
    __apiUrl     = 'https://api.github.com/repos/{0}/{1}'
    __timeoutApi = 60000

    def __init__(self):
        try:
            with open('api.token', 'r') as tokenFile:
                apiToken = tokenFile.read()
            #simple check
            if len(apiToken) != 40:
                raise ValueError('apiToken is empty or invalid!')
        except OSError as err:
            print("OS error [{0}]".format(err))
        except:
            print("Unexpected error [{0}]".format(sys.exc_info()[0]))
            raise

    def get(self,owner,repositoryName):
        apiUrlReady = self.__apiUrl.format(owner,repositoryName)
        req = urllib.request.Request(apiUrlReady)
        req.add_header('Authorization', 'token %s' % self.__apiToken)
        jsonApiResult = None
        try:
            with urllib.request.urlopen(apiUrlReady, timeout=self.__timeoutApi) as response:
                jsonApiResult = json.loads(response.read())
            jsonReturnResult = {'fullName':     jsonApiResult['full_name'],
                                'description':  jsonApiResult['description'],
                                'cloneUrl':     jsonApiResult['clone_url'],
                                'stars':        jsonApiResult['stargazers_count'],
                                'createdAt':    jsonApiResult['created_at']
                                }
            return jsonReturnResult
        except HTTPError as err:
            return "HTTPError [{0}]".format(err)
        except:
            return "Unexpected error [{0}]".format(sys.exc_info()[0])