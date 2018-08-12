using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAnalyzer.Concrete;

namespace WebAnalyzer.Core
{
    public class AnalyzerProccessor
    {
        public const string MetaKeywordsTag = "meta name=\"keywords\" content=\"";
        List<AnalyzerResultModel> result;

        public AnalyzerProccessor()
        {
            result = new List<AnalyzerResultModel>();
        }

        public List<AnalyzerResultModel> GetResult(int foundIndex, string pageData)
        {
            string foundString = pageData.Substring(foundIndex + MetaKeywordsTag.Length);
            StringBuilder foundKeyWord = new StringBuilder();
            for (int i = 0; i < foundString.Length; i++)
            {
                char c = foundString[i];
                switch (c)
                {
                    case ',': Add(foundKeyWord); foundKeyWord.Clear(); break;
                    case '>': Add(foundKeyWord); // add last found keyword
                        // we have to break the loop somehow
                        i = foundString.Length + 1;
                        break;
                    case ' ': break;
                    case '"': break;
                    case '\\': break;
                    case '/': break;
                    default: foundKeyWord.Append(c); break;
                }
            }

            foreach (var keyWord in result)
            {
                keyWord.Occurrence = Regex.Matches(pageData, keyWord.Keyword).Count;
            }

            return result;
        }

        private void Add(StringBuilder foundKeyWord)
        {
            result.Add(new AnalyzerResultModel() { Keyword = foundKeyWord.ToString() });
        }
    }
}
