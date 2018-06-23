using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAnalyzer.Concrete
{
    public class AnalyzerResultExceptionModel
    {
        public List<AnalyzerResultModel> FoundKeywords { get; set; }
        public string Error { get; set; }
       
    }
}
