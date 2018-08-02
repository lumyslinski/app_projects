# get a filepath with input data
# load it
# get length of calculations
# process calculations via api in python
# print result for each calculation

require 'net/http'
require 'pathname'

def hash(data)
  return data.codepoints.to_a.join()
end

def getApiResult(calculation)
  url = "http://0.0.0.0:9999/rpn/" + hash(calculation)
  uri = URI(url)
  return Net::HTTP.get(uri).gsub!(/^\"|\"?$/, '')
end

calculationsCount = 0
calculations      = []
filePath          = ''

if ARGV.length == 0
  filePath = Dir.pwd + "/test.txt"
  print("Missing argument file path for calculation file! Loading default file "+filePath+" \n")
else
  filePath = ARGV[0]
end
p = Pathname.new(filePath)
if p.absolute?
  File.open(p.realpath) do |file|
    file.each do |line|
      r = line.gsub(/\n/,'')
      calculations.push(r)
    end
  end
  if calculations.length > 0
    calculationsCount = calculations[0].to_i
  end
  calculations.each_with_index do |calculation, index|
    if index > 0 && index <= calculationsCount
      printf '%s', getApiResult(calculation)
    end
  end
else
  print("File path should be absolute!")
end


