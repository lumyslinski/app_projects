from flask_restful import Resource
from FileCacheLoader import FileCacheLoader
from ProducerKafka import ProducerKafka
from Logger import Logger

class ApiController(Resource):

    def get(self, requestData):
        response = self.getRPNresult(requestData)
        logger = Logger(requestData, response)
        logger.start()
        return response

    def getRPNresult(self, requestData):
        rpnCacheResult = FileCacheLoader.loadFileCache(requestData)
        if len(rpnCacheResult) > 0:
            return rpnCacheResult
        else:
            p = ProducerKafka(requestData)
            if len(p.error) > 0:
                return p.error
            else:
                fileCacheLoader = FileCacheLoader(requestData)
                fileCacheLoader.start()
                fileCacheLoader.join()
                rpnCacheResult = fileCacheLoader.result
                if len(rpnCacheResult) > 0:
                    return rpnCacheResult
                else:
                    return "try again..."
