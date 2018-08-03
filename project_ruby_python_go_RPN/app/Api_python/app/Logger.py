import os
import threading
import time
from datetime import datetime


class Logger(threading.Thread):
    def __init__(self,dataRequest,dataResponse):
        # calling superclass init
        threading.Thread.__init__(self)
        self.dataRequest = dataRequest
        self.dataResponse = dataResponse

    def unHash(self):
        splittedInput = self.dataRequest.split("32")
        unHashResult = ""
        for elem in splittedInput:
            code = chr(int(elem))
            unHashResult += "%s " % code
        self.dataRequest = unHashResult

    def run(self):
        self.unHash()
        dir = os.path.dirname(os.path.abspath(__file__))
        filePath = dir + '/../../../Logs/log.txt'
        dataToWrite = "{0}: request[{1}] response[{2}] \n".format(str(datetime.now()), self.dataRequest, self.dataResponse)
        with open(filePath, 'a') as f:
            f.write(dataToWrite)
        time.sleep(2)
