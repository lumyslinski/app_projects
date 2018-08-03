import os
import sys
import time
import threading

exitFlag = 0

class FileCacheLoader(threading.Thread):
    def __init__(self, requestData):
        threading.Thread.__init__(self)
        self.requestData = requestData
        self.timeOutLimit = 200
        self.result = ""

    def run(self):
        elapsedTime = 0
        interval = 5
        while len(self.result) < 0 or elapsedTime <= self.timeOutLimit:
            if exitFlag:
                break
            else:
                self.result = self.loadFileCache(self.requestData)
            if len(self.result) < 0:
                time.sleep(interval)
                elapsedTime += interval
            else:
                break

    @staticmethod
    def loadFileCache(fileName):
        rpnResult = ""
        filePath = ""
        try:
            dir = os.path.dirname(os.path.abspath(__file__))
            filePath = dir + '/../../../CacheResults/' + fileName
            with open(filePath, 'r') as resultFile:
                rpnResult = resultFile.read()
        except FileNotFoundError:
            pass
        except:
            sys.stderr.write("Error: %s" % sys.exc_info()[0])
        return rpnResult






