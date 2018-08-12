﻿using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAnalyzer.Concrete
{
    public sealed class AnalyzerHelper
    {
        public static bool IsValidUrl(string url)
        {
            if (!String.IsNullOrEmpty(url) && (url.Contains("http://") || url.Contains("https://")))
            {
                return true;
            }
            return false;
        }

        public static async Task<AnalyzerProccessResultModel> GetFoundKeywords(string url)
        {
            AnalyzerProccessResultModel result = new AnalyzerProccessResultModel();
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = ASCIIEncoding.UTF8;
                string pageData = await wc.DownloadStringTaskAsync(new Uri(url));
                if (String.IsNullOrEmpty(pageData)) result.Error = "Blank page";
                else
                {
                    result = new AnalyzerProccessor().Proccess(pageData);
                }
            }
            catch (Exception exp)
            {
                result.Error = exp.Message;
            }
            return result;
        }
    }
}
