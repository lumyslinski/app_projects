using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAnalyzer.Core;

namespace WebAnalyzer.Tests
{
    [TestClass]
    public class UnitTestWebAnalyzer
    {
        [TestMethod]
        public void CheckKeywords()
        {
            try
            {
                string content = null;
                using (StreamReader sr = File.OpenText("TestMeta.html"))
                {
                    content = sr.ReadToEnd();
                }
                if(!String.IsNullOrEmpty(content))
                {
                    AnalyzerProccessor analyzerProccessor = new AnalyzerProccessor();
                    var result = analyzerProccessor.Proccess(content);
                    Assert.IsTrue(result.ToString() == "wp.pl=437,WP=31,WirtualnaPolska=0,Pogoda=30,Wiadomości=6,Newsy=1,Informacje=3,Sport=20,Finanse=8,Rozrywka=3,Program=7,Telewizja=1,#dziejesiewpolsce=1");
                }
            } catch(Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
    }
}
