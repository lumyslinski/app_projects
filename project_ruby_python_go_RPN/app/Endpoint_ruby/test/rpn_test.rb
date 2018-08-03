require 'test/unit'
require 'pathname'
require_relative './../app/rpn.rb'

class RpnTest < Test::Unit::TestCase

  # Called before every test method runs. Can be used
  # to set up fixture information.
  def setup
    # Do nothing
  end

  # Called after every test method runs. Can be used to tear
  # down fixture information.

  def teardown
    # Do nothing
  end

  def test_default_calcs
    rpn = Rpn.new
    path = Dir.pwd + "/app/default.txt"
    pathObj = Pathname.new(path)
    calcs = rpn.load(pathObj)
    if calcs.length == 4
      assert_equal("3 4 +",calcs[1])
      assert_equal("5 1 2 + 4 * + 3 -",calcs[2])
      assert_equal("3 4 + 6 0 /",calcs[3])
    else
      raise "Expected 3 calculations + 1 length element!"
    end
  end
end