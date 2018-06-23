using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;

namespace Neuron
{
    public abstract class Neuron 
    {
        protected static double LEARNING_RATE = 0.35;
        protected static double THRESHOLD_SIGNAL = 0.9;

        protected abstract double transferFunction(double excitement);
        public abstract double getWeightUpdate(int weightIndex, double delta, List<Double> signal);
       
        private List<Double> weights;

        public void setWeights(List<Double> weights)
        {
            this.weights = weights;
        }

        public List<Double> getWeights()
        {
            return new ReadOnlyCollection<Double>(weights).ToList();
        }

        public double propagate(List<Double> signal)
        {
            double excitement = activationFunction(signal);
            double output = transferFunction(excitement);
            return output;
            //return output > 0 ? 1 : 0;
        }


        private double activationFunction(List<Double> signal)
        {
            double result = THRESHOLD_SIGNAL * weights.ElementAt(0);

            try
            {

                for (int i = 0; i < signal.Count(); i++)
                {
                    result += signal.ElementAt(i) * weights.ElementAt(i + 1);
                }
            }
            catch (Exception exp)
            {
                throw new Exception("Set proper number of weights for signals (signals+1)");
            }

            return result;
        }


    }
}
