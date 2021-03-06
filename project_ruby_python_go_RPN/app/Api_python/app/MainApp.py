from flask import Flask
from flask_restful import Resource, Api
from ApiController import ApiController

url = '/rpn/<string:requestHashedData>'

class SimpleText(Resource):
    def get(self):
        return {'message': 'You can query only in this form: '+url}


app = Flask(__name__)
api = Api(app)
api.add_resource(SimpleText, '/', '/rpn')
api.add_resource(ApiController, url)
