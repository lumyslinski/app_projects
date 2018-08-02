import os
import sys
import unittest


class HashTestClass(unittest.TestCase):

    def test_hashMethod(self):
        input = "5 1 2 + 4 * + 3 -"
        hashResult = ""
        for elem in input:
            code = ord(elem)
            hashResult += str(code)
        self.assertTrue(hashResult, "5332493250324332523242324332513245")

    def test_unHashMethod(self):
        input = "5332493250324332523242324332513245"
        splittedInput = input.split("32")
        unHashResult = ""
        for elem in splittedInput:
            code = chr(int(elem))
            unHashResult += "%s " % code
        self.assertTrue(unHashResult, "5 1 2 + 4 * + 3 -")

    def test_readCache(self):
        rpnResult = "-1"
        try:
            dir = os.path.dirname(os.path.abspath(__file__))
            with open(dir + '/../../CacheResults/5332493250324332523242324332513245.test', 'r') as resultFile:
                rpnResult = resultFile.read()
        except:
            self.fail(sys.exc_info()[0])
        self.assertTrue(rpnResult, "14")


if __name__ == '__main__':
    unittest.main()

