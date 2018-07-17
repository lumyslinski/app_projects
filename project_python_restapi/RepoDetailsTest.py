import urllib.request
import json
import unittest

class TestRESTapi(unittest.TestCase):

    def test_api(self):
        apiToken = None
        with open('api.token', 'r') as tokenFile:
            apiToken = tokenFile.read()
        url = 'https://api.github.com/repos/lumyslinski/app_projects'
        req = urllib.request.Request(url)
        req.add_header('Authorization', 'token %s' % apiToken)
        jsonResult = None
        with urllib.request.urlopen(url, timeout=60000) as response:
            jsonResult = json.loads(response.read())
        print(jsonResult)
        self.assertTrue(jsonResult['full_name']     == 'lumyslinski/app_projects')
        self.assertTrue(jsonResult['description']   == 'This category contains mine software implementations')
        self.assertTrue(jsonResult['clone_url']     == 'https://github.com/lumyslinski/app_projects.git')
        self.assertTrue(jsonResult['created_at']    == '2018-06-23T01:18:09Z')
        # stargazers_count could by changed any time


if __name__ == '__main__':
    unittest.main()