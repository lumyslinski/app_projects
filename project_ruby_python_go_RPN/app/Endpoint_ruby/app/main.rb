require 'pathname'
require_relative 'rpn.rb'

filePath = ''
if ARGV.length == 0
  filePath = "default.txt"
  print("Missing argument file path for calculation file! Loading default file "+filePath+" \n")
else
  filePath = ARGV[0]
end
p = Pathname.new(filePath)
if p.absolute?
  rpn = Rpn.new
  calcs = rpn.load(filePath)
  rpn.process(calcs)
else
  print("File path should be absolute!")
end


