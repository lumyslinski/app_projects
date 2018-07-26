import urllib.request
import json
import sys
from urllib.error import HTTPError
from flask_restful import Resource


class GitHubApi(Resource):
    __apiUrl = 'https://api.github.com/repos/{owner}/{repository}'

    def get(self, owner, repository, token=''):
        url = self.__apiUrl.format(owner=owner, repository=repository)
        req = urllib.request.Request(url, data=None)
        if len(token) > 0:
            req.add_header('Authorization', 'token %s' % token)
        try:
            with urllib.request.urlopen(req) as response:
                result = json.loads(response.read())
                return {'fullName': result['full_name'],
                        'description': result['description'],
                        'cloneUrl': result['clone_url'],
                        'stars': result['stargazers_count'],
                        'createdAt': result['created_at']
                        }
        except HTTPError as err:
            return "HTTPError [{0}]".format(err)
        except:
            return "Unexpected error [{0}]".format(sys.exc_info()[0])
