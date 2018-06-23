using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;

namespace Neuron
{
    public class Logger
    {
        private string LogFile;
        public string path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "").Replace("\\bin\\Release", "");
        private StreamWriter writer;

        public Logger()
        {
            LogFile = path + @"\output\report_" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm.ss.ff", CultureInfo.InvariantCulture) + ".log";
        }

        public Logger(string p)
        {
            LogFile = p;
            writer = new StreamWriter(LogFile, true);
        }

        public string GetNetPath()
        {
            return path + @"\data\net.ann";
        }

        public string GetAtrPath()
        {
            return path + @"\data\xor.dat";
        }

        public string GetDecPath()
        {
            return path + @"\data\xor.dat";
        }

        public string GetOutputPath()
        {
            return LogFile;
        }

        public void WriteLine(string txt, bool writeToConsole = false)
        {
            //File.AppendAllText(LogFile,txt);
            writer.WriteLine(txt);
           
            if (writeToConsole)
            {
                Console.Write(txt);
            } 
        }

        public void CloseWrite()
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }
}
