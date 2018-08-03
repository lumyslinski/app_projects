require 'net/http'

class Rpn

  def hash(data)
    return data.codepoints.to_a.join()
  end

  def getApiResult(calculation)
    url = "http://0.0.0.0:9999/rpn/" + hash(calculation)
    uri = URI(url)
    return Net::HTTP.get(uri).gsub!(/^\"|\"?$/, '')
  end

  def load(path)
    calculations = []
    File.open(path.realpath) do |file|
      file.each do |line|
        r = line.gsub(/\n/, '')
        calculations.push(r)
      end
    end
    return calculations
  end

  def process(calculations)
    calculationsCount = 0
    if calculations.length > 0
      calculationsCount = calculations[0].to_i
    end
    if calculationsCount > 0
      calculations.each_with_index do |calculation, index|
        if index > 0 && index <= calculationsCount
          printf '%s', getApiResult(calculation)
        end
      end
    end
  end
end