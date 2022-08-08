using System.Net;

namespace HHParserWinForm.InnerProg.Cookies
{
    class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
            this.ResponseCookies = new CookieCollection();

            this.Headers.Add("accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            this.Headers.Add("accept-encoding", "gzip, deflate, br");
            //this.Headers.Add("accept", "application/json");

            this.Headers.Add("origin", "https://hh.ru");

            this.Headers.Add("sec-ch-ua-mobile", "?0");
            this.Headers.Add("sec-ch-ua-platform", "Windows");
            //this.Headers.Add("sec-fetch-dest", "empty");
            //this.Headers.Add("sec-fetch-mode", "cors");
            //this.Headers.Add("sec-fetch-site", "same-origin");
            this.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36");

            this.Headers.Add("x-hhtmfrom", "vacancy_search_list");
            this.Headers.Add("x-hhtmsource", "vacancy");
            this.Headers.Add("x-requested-with", "XMLHttpRequest");
        }
        public CookieContainer CookieContainer { get; private set; }
        public CookieCollection ResponseCookies { get; set; }
    }
}
