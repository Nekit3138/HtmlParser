using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace HtmlParser
{
    public class Parser
    {
        private HttpClient client;

        private string http;

        public Parser()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
        }

        public void SetPage(string link)
        {
            http = client.GetAsync(link).Result.Content.ReadAsStringAsync().Result;
        }

        public string GetContent()
        {
            return http;
        }

        public string Find(string tag, List<string> attr_attrValue)
        {
            if (http == null)
                throw new Exception("The link is empty, use method SetPage first!");
            var result = new Regex($"<{tag}.*>(.*)</{tag}>").Matches(http);
            foreach (var cls in attr_attrValue)
            {
                result = new Regex($"<{tag}.*{cls}.*>(.*)</{tag}>")
                    .Matches(http);
            }

            return result.FirstOrDefault() != null
                ? result[0].Groups[1].Value
                : "Not found";
        }

        public List<string> FindAll(string tag, List<string> attr_attrValue)
        {
            if (http == null)
                throw new Exception("The link is empty, use method SetPage first!");
            var result = new Regex($"<{tag}.*>(.*)</{tag}>").Matches(http);
            foreach (var cls in attr_attrValue)
            {
                result = new Regex($"<{tag}.*{cls}.*>(.*)</{tag}>")
                    .Matches(http);
            }

            return result.FirstOrDefault() != null
                ? result.Select(i => i.Groups[1].Value).ToList()
                : new List<string>();
        }
    }
}