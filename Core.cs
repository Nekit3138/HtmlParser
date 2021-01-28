using System;
using System.Net.Http;

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

        public string GetPage()
        {
            return http;
        }

        public string Find(string attr, string attrValue)
        {
            if (http == null)
                throw new Exception("The link is empty, use method SetPage first!");
            int index = http.IndexOf($"{attr}=\"{attrValue}\"");
            if (index == -1)
            {
                return "Not found";
            }

            int startIndex = 0;
            int endIndex = 0;
            string result = "";
            for (var i = index; i != http.Length; i++)
            {
                if (http[i] == '>')
                {
                    startIndex = i;
                }
                else if (http[i] == '<')
                {
                    endIndex = i;
                    break;
                }
            }

            for (var i = startIndex + 1; i != endIndex; i++)
            {
                result += http[i];
            }

            return result;
        }
    }
}