using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Neuron
{
    class Program
    {
        static void Main(string[] args)
        {
            string cki;
            
            Console.WriteLine("Type 'e' to exit or type max epoch to run.");
            cki = Console.ReadLine();

            while (!cki.ToString().Equals("e")) 
            {
                Console.WriteLine("learning...Please wait for generating report...");
                Logger l = new Logger();

                RunNeuralNet(cki,l);

                Console.WriteLine("\n \n Report you can find here: " + l.GetOutputPath() + "!\n\nType 'e' to exit or type max epoch to run. \n");

                cki = Console.ReadLine();
            } 

        }

        private static void RunNeuralNet(string epochs, Logger l) {
            NeuralNet network = new NeuralNet(l.GetOutputPath());
            List<List<Double>> attributes = NeuralNet.readAttributes(l.GetAtrPath());
            List<Double> decisions = NeuralNet.readDecisions(l.GetDecPath());
            int lepochs = NeuralNet.LEARNING_EPOCHS;
            

        try
        {
            lepochs = int.Parse(epochs);
        }
        catch (Exception exp)
        {
        }

        Stopwatch watch = new Stopwatch();
        watch.Start();
        Console.WriteLine("Started: {0}", DateTime.Now.ToShortTimeString());

        for (int i = 0; i <= lepochs; i++)
            {
                if (network.learnEpoch(attributes, decisions, i, lepochs))
                {
                    Console.WriteLine("Network learned!");
                    break; // learned!!!
                }
            }

        watch.Stop();
        Console.WriteLine("Elapsed: {0}", watch.Elapsed);
        Console.WriteLine("In milliseconds: {0}", watch.ElapsedMilliseconds);

   
        }

        
    }
}
