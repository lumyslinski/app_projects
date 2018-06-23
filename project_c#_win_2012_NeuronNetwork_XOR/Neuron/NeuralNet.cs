using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Neuron
{
    public class NeuralNet
    {
        public const int LEARNING_EPOCHS = 500;
        protected const double THRESHOLD_MSE = 0.001;

        public bool lastProp;
        List<List<Neuron>> layers;

        private double prevAvgMSE = 0;

        public NeuralNet(string fileName)
        {
            layers = new List<List<Neuron>>();
            initLogging(fileName);
            readNeuralNet();
        }

        public void addLayer(List<Neuron> layer)
        {
            layers.Add(layer);
        }

        private void PropagateOutputs(List<Double> signal, List<List<Double>> outputs)
        {
            for (int l = 0; l < layers.Count(); l++) // init outputs
            {
                outputs.Add(new List<double>());
            }

            for (int l = 0; l < layers.Count(); l++) // layer
            {
                if (l == 0) // first layer - signals
                {
                    outputs[l] = propagate(signal, l); // feed
                }
                if (l > 0) // hiddens layer - outputs
                {
                    outputs[l] = propagate(outputs[l - 1], l); // bp
                }
            }
        }


        public List<Double> propagate(List<Double> signal, int layerId)
        {
            List<Double> result = new List<Double>();

                for (int n = 0; n < layers[layerId].Count(); n++) // neuron
                {
                    var neuron = layers[layerId][n];
                    result.Add(neuron.propagate(signal));
                }

            return result;
        }

        public List<List<Double>> computeDeltas(List<List<Double>> outputs, double correctValue)
        {
            List<List<Double>> result = new List<List<Double>>(layers.Count() - 1);

            for (int l = 0; l < layers.Count(); l++) // layer
            {
                result.Add(new List<double>());
            }

            for (int l = (layers.Count()-1); l >= 0; l--) // layer
            {
                for (int n = 0; n < layers[l].Count; n++) // neuron output
                {
         
                    if (l == (layers.Count() - 1)) // last layer
                    {
                        result[l].Add(correctValue - outputs[l][n]);
                    }
                    else
                    {
                        result[l].Add(computeDeltaForHiddenLayers(result[l + 1], layers[l + 1], n));
                    }
                }
            }

            return result;
        }

        protected double computeDeltaForHiddenLayers(List<double> nextDeltas, List<Neuron> neurons, int weightIndex)
        {
            double result = 0;

            for (int n = 0; n <= (neurons.Count-1); n++)
            {
                var delta = nextDeltas[n];
                result += neurons[n].getWeights().ElementAt(weightIndex+1) * delta;
            }

            return result;
        }

        private void learnNeuronsDeltaRule(List<Double> signal, List<List<Double>> outputs, List<List<Double>> deltas)
        {
            for (int l = 0; l < layers.Count(); l++) // layer
            {
                for (int n = 0; n < layers[l].Count(); n++) // neuron output
                {
                    var neuron = layers[l][n];
                    var weightsCount = neuron.getWeights().Count;
                    List<Double> newWeights = new List<Double>(weightsCount);

                    for (int i = 0; i < weightsCount; i++)
                    {
                        double newWeight = 0;
                        if (l == 0) // first layer - signals
                        {
                            newWeight = neuron.getWeights().ElementAt(i) + neuron.getWeightUpdate(i, deltas[l][n], signal);
                        }
                        if (l > 0) // first layer - signals
                        {
                            newWeight = neuron.getWeights().ElementAt(i) + neuron.getWeightUpdate(i, deltas[l][n], outputs[l-1]);
                        }

                        newWeights.Add(newWeight);
                    }

                    //if (l > 0)  {
                        neuron.setWeights(newWeights);
                    //}
                }
            }
        }

        private double learnBackpropagate(List<Double> signal, double correctValue)
        {
            if (lastProp)
            {
                var debug = true;
            }

            List<List<Double>> outputs = new List<List<double>>();

            PropagateOutputs(signal, outputs);

            PrintOutput(outputs);

            List<List<Double>> deltas = computeDeltas(outputs, correctValue);

            PrintDeltas(deltas);

            learnNeuronsDeltaRule(signal, outputs, deltas);

            PrintWeights();

            this.Logger.WriteLine("\n LAST MSE = " + ConvertDoubleToString(computeMeanSquaredError(outputs.Last().Last(), correctValue)));

            return outputs.Last().Last(); // return last value from last neuron of last layer           
        }
      
        private void PrintDeltas(List<List<Double>> deltas)
        {
            this.Logger.WriteLine("\n Deltas: \n");
            for (int l = 0; l < layers.Count(); l++) // layer
            {
                this.Logger.WriteLine("\t Layer: " + l.ToString());

                for (int d = 0; d < deltas[l].Count; d++)
                {
                    this.Logger.WriteLine("\t\t" + deltas[l][d] + " ");
                }
                this.Logger.WriteLine("\n");
            }
        }

        public void PrintOutput(List<List<Double>> outputs)
        {
            this.Logger.WriteLine("\n Output: \n");
            for (int l = 0; l < layers.Count(); l++) // layer
            {
                this.Logger.WriteLine("\t Layer: " + l.ToString());
                for (int o = 0; o < outputs[l].Count; o++)
                {
                    this.Logger.WriteLine("\t\t" + outputs[l][o] + " ");
                }
                this.Logger.WriteLine("\n");
            }
        }


        public void PrintWeights()
        {
            this.Logger.WriteLine("\n Weights: \n");

            for (int l = 0; l < layers.Count(); l++) // layer
            {
                this.Logger.WriteLine("\t Layer: " + l.ToString()+"\n");
                for (int n = 0; n < layers[l].Count(); n++) // neuron output
                {
                    List<double> weights = layers[l][n].getWeights();

                    for (int w = 0; w < weights.Count; w++)
                    {
                        this.Logger.WriteLine("\t\t" + weights[w] + " ");
                    }
                    this.Logger.WriteLine("\n");
                }
            }
        }

        public bool learnEpoch(List<List<Double>> signals, List<Double> correctValues, int epochId, int maxEpochs)
        {
            this.Logger.WriteLine("-------------START OF epoch: " + epochId.ToString() + "------------------",true);

            bool learned = false;
            bool overLearned = false;

            List<double> outputsForSignals = new List<double>();

            for (int i = 0; i < signals.Count(); i++) // layer
            {
                this.Logger.WriteLine("\n------------------------START of Example " + i + "--------------------------------------------\n");
                this.Logger.WriteLine("\n Signals: ");

                for (int j = 0; j < signals[i].Count(); j++)
                {
                    this.Logger.WriteLine(" " + signals[i][j] + " ");
                }

                this.Logger.WriteLine(" Correct value: " + correctValues[i]);

                this.lastProp = epochId == maxEpochs;
                outputsForSignals.Add(learnBackpropagate(signals[i], correctValues[i]));

                this.Logger.WriteLine("\n--------------------END of Example " + i + "--------------------------------------------\n");
            }

            List<double> mseList = computeMeanSquaredErrors(outputsForSignals, correctValues);
            double avgMse = mseList.Average();
            
            this.Logger.WriteLine(("\n 0 XOR 0 = " + correctValues[0].ToString() + " ? y=" + String.Format("{0,10:0.0000}", outputsForSignals[0]) + "\t mse=" + ConvertDoubleToString(mseList[0])),true);
            this.Logger.WriteLine(("\n 0 XOR 1 = " + correctValues[1].ToString() + " ? y=" + String.Format("{0,10:0.0000}", outputsForSignals[1]) + "\t mse=" + ConvertDoubleToString(mseList[1])),true);
            this.Logger.WriteLine(("\n 1 XOR 0 = " + correctValues[2].ToString() + " ? y=" + String.Format("{0,10:0.0000}", outputsForSignals[2]) + "\t mse=" + ConvertDoubleToString(mseList[2])),true);
            this.Logger.WriteLine(("\n 1 XOR 1 = " + correctValues[3].ToString() + " ? y=" + String.Format("{0,10:0.0000}", outputsForSignals[3]) + "\t mse=" + ConvertDoubleToString(mseList[3])),true);
            
            if (avgMse <= THRESHOLD_MSE)
            {
                learned = true;
            }

            

            if (!learned && prevAvgMSE > avgMse)
            {
                overLearned = true;

                this.Logger.WriteLine("\n AVG MSE = " + avgMse.ToString() + " is increasing... Difference is " + ConvertDoubleToString(prevAvgMSE - avgMse), true);
                prevAvgMSE = avgMse;
                //return overLearned;
            }
            else
            {
                this.Logger.WriteLine("\n AVG MSE = " + avgMse.ToString() + " is learned well ? " + learned.ToString(), true);
                prevAvgMSE = avgMse;
            }

            this.Logger.WriteLine("\n -------------END OF epoch: " + epochId.ToString() + "------------------\n", true);

            if (epochId == maxEpochs || learned)
            {
                this.Logger.CloseWrite();
            }

            return learned;
        }

        private string ConvertDoubleToString(double ind)
        {
            return String.Format("{0,10:0.##########}", ind);
        }

        public List<double> computeMeanSquaredErrors(List<Double> outputs, List<Double> correctValues)
        {
            List<double> result = new List<double>();
                
                    double se = 0;
                    for (int i = 0; i < outputs.Count(); i++)
                    {
                        double delta = correctValues[i] - outputs[i];
                        se = ((delta * delta) / 2.0);
                        result.Add(se);
                    }
                
            return result;
        }

        public double computeMeanSquaredError(Double output, Double correctValue)
        {
            double result = 0;
            double delta = correctValue - output;
                   result = (delta * delta);

            return result / 2.0;
        }

        public void readNeuralNet()
        {
       

		try {
            using (StreamReader reader = new StreamReader(this.Logger.GetNetPath()))
                {

			    String line;
                int l = 0;

			    while ((line = reader.ReadLine()) != null) {
				    String[] neurons = line.Split(';');
                    List<Neuron> layer = new List<Neuron>();
                    this.Logger.WriteLine(("\n Layer " + l.ToString() + ": \n"),true);
                    int n = 0;
				    foreach (String neuron in neurons) {
                    
                        if (!String.IsNullOrEmpty(neuron))
                        {
                            this.Logger.WriteLine(("\t Neuron " + n.ToString() + " with weights: "),true);
                            this.Logger.WriteLine((neuron),true);
                            layer.Add(new NonlinearNeuron(readWeights(neuron.Trim())));
                        }

                        n++;
				    }

				    this.addLayer(layer);
                    l++;
                    this.Logger.WriteLine(("\n"),true);
			    }
                
                
                this.Logger.WriteLine(("\n Network builded. \n"),true);
            }
            
		} catch (IOException e) {
            e.StackTrace.ToString();
		}

	}

        public static List<List<Double>> readAttributes(String fileName)
        {
            List<List<Double>> result = new List<List<Double>>();

            try
            {
                String line;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        List<Double> exampleAttributes = new List<Double>();
                        String[] values = Regex.Split(line, "\\s+");

                        // the last value is decision
                        for (int a = 0; a < values.Count() - 1; a++)
                        {
                            exampleAttributes.Add(Double.Parse(values[a]));
                        }
                        result.Add(exampleAttributes);
                    }
                }
            }
            catch (IOException e)
            {
                e.StackTrace.ToString();
            }
            return result;
        }

        public static List<Double> readDecisions(String fileName)
        {
            List<Double> result = new List<Double>();
            try
            {
                String line;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        String[] values = Regex.Split(line, "\\s+");
                        result.Add(Double.Parse(values[values.Count() - 1]));
                    }

                }
            }
            catch (IOException e)
            {
                e.StackTrace.ToString();
            }
            return result;
        }

        private static List<Double> readWeights(String layer) {
		List<Double> result = new List<Double>();
		String[] weights = Regex.Split(layer, "\\s+");

		foreach (String weight in weights) {
            result.Add(Double.Parse(weight));
		}
		return result;
	}

        public void initLogging(string p)
        {
            this.Logger = new Logger(p);
        }

        public Logger Logger { get; set; }
    }

}