using WebAnalyzer.Contracts;

namespace WebAnalyzer.Concrete
{
    public class AnalyzerItemResultModel : IAnalyzerResultModel
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

        public override string ToString()
        {
            return string.Format("{0}={1}", Keyword, Occurrence);
        }
    }
}
