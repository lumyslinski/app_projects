import unittest
import os,sys
CURRENT_DIR = os.path.dirname(os.path.abspath(__file__))
sys.path.append(os.path.dirname(CURRENT_DIR+"/../app/"))
from MainApp import app

class TestLocalRestApi(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()
        self.app.testing = True

    def testAppStatusCode(self):
        result = self.app.get('/')
        self.assertEqual(result.status_code, 200)

    def testAppResultData(self):
        result = self.app.get('/')
        originalData = '{"message": "You can query only in this form: /repositories/<string:owner>/<string:repository>/<string:token>"}\n'
        testData = bytes(result.data).decode('utf-8')
        self.assertEqual(testData, originalData)
