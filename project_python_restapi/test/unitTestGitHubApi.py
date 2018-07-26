import urllib.request
import json
import unittest

class TestRESTapi(unittest.TestCase):

    def test_api(self):
        url = 'https://api.github.com/repos/lumyslinski/app_projects'
        result = ''
        with urllib.request.urlopen(url, timeout=60000) as response:
            result = json.loads(response.read())
        print(result)
        self.assertTrue(result['full_name']     == 'lumyslinski/app_projects')
        self.assertTrue(result['description']   == 'This category contains mine software implementations')
        self.assertTrue(result['clone_url']     == 'https://github.com/lumyslinski/app_projects.git')
        self.assertTrue(result['created_at']    == '2018-06-23T01:18:09Z')
        # stargazers_count could by changed any time

if __name__ == '__main__':
    unittest.main()