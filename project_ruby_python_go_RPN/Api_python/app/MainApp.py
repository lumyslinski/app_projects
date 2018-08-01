from flask import Flask
from flask_restful import Resource, Api
from ProducerKafka import ProducerKafka

url = '/rpn/<string:requestData>'

class SimpleText(Resource):
    def get(self):
        return {'message': 'You can query only in this form: '+url}


app = Flask(__name__)
api = Api(app)
api.add_resource(SimpleText, '/')
api.add_resource(ProducerKafka, url)
