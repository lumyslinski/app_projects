using System.Collections.Generic;
using System.Text;
using WebAnalyzer.Contracts;

namespace WebAnalyzer.Concrete
{
    public class AnalyzerProccessResultModel
    {
        public List<IAnalyzerResultModel> FoundKeywords { get; set; }
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
