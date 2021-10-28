using System.Linq;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using System.IO;

namespace AnkiBatcher
{
    public class GoogleSolver
    {
        private bool _calledGoogle = false;
                
        public IEnumerable<GoogleAnswer> EnumerateGoogleAnswers(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException($"'{nameof(question)}' cannot be null or whitespace.", nameof(question));
            }

            if (_calledGoogle) Thread.Sleep(3000);
            else _calledGoogle = true;

            var queryString = HttpUtility.ParseQueryString("");
            queryString["q"] = question;

            // From Web
            var url = $"https://www.google.com/search?{queryString}";
            var web = new HtmlWeb();
            var doc = web.Load(url);
                        
            //var nodes = doc.DocumentNode.SelectNodes("//div[@data-attrid='wa:/description']");
            //var nodes = doc.DocumentNode.SelectNodes("//div[@data-attrid]");
            var results = doc.DocumentNode.SelectNodes(@"//div[@class='g']");
            if (results == null) yield break;
            foreach (var node in results)
            {
                var link = node.SelectSingleNode(".//@href");
                if (link == null) continue;
                var href = link.GetAttributeValue("href", null);

                var descriptonEl = link.SelectSingleNode("parent::div/following-sibling::div");
                if (descriptonEl == null) continue;

                yield return new GoogleAnswer
                {
                    Url = href,
                    Description = descriptonEl.InnerText,
                };
            }
        }
    }
}
