using Newtonsoft.Json;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;

namespace HHParserWinForm.InnerProg.Cookies
{
    class FileToCookies{
        public string GetCookieStringFromFile(){
            var cookiesFromFile = JsonConvert.DeserializeObject<CookieModel[]>(File.ReadAllText("cookies.txt"));
            string allCookies = "";
            foreach (var c in cookiesFromFile){
                allCookies += c.name + "=" + c.value + "; ";
            }
            return allCookies;
        }
        public Cookie[] ConvertFileToSeleniumCookies(){
            var cookiesFromFile = JsonConvert.DeserializeObject<CookieModel[]>(File.ReadAllText("cookies.txt"));
            var convertedCookies = FileToCookie(cookiesFromFile);

            return convertedCookies;
        }
        private Cookie[] FileToCookie(CookieModel[] _cookies){
            List<Cookie> temp_cookies = new List<Cookie>();
            foreach (CookieModel c in _cookies){
                temp_cookies.Add(new Cookie(c.name, c.value, c.domain, c.path, c.expirationDate, (bool)c.secure, (bool)c.httpOnly, c.sameSite));
            }
            return temp_cookies.ToArray();
        }
    }
}
