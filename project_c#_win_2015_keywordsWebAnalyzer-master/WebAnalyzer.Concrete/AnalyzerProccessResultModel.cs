using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAnalyzer.Concrete
{
    public class AnalyzerProccessResultModel
    {
        public List<AnalyzerItemResultModel> FoundKeywords { get; set; }
        public string Error { get; set; }

        public override string ToString()
        {
            if (FoundKeywords != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var k in FoundKeywords)
                {
                    sb.Append(string.Format("{0}={1},",k.Keyword,k.Occurrence));
                }
                return sb.ToString().TrimEnd(',');
            }
            return null;
        }
    }
}
