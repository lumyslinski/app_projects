using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAnalyzer.Contracts;

namespace WebAnalyzer.Concrete
{
    public class AnalyzerResultModel : IAnalyzerResultModel
    {
        public string Keyword
        {
            get;
            set;
        }

        public int Occurrence
        {
            get;
            set;
        }
    }
}
