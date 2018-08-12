using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAnalyzer.Contracts
{
    public interface IAnalyzerResultModel
    {
        string Keyword { get; set; }
        int Occurrence { get; set; }
    }

    
}
