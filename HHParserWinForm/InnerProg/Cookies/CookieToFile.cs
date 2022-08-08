using Newtonsoft.Json;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace HHParserWinForm.InnerProg.Cookies
{
    class CookiesToFile
    {
        public void ConvertCookiesToFile(ReadOnlyCollection<Cookie> _cookies)
        {
            var _convertedCookies = CookieToFile(_cookies);
            File.WriteAllText("cookies.txt", JsonConvert.SerializeObject(_convertedCookies, Formatting.Indented));
        }

        private CookieModel[] CookieToFile(ReadOnlyCollection<Cookie> _cookies)
        {
            List<CookieModel> temp_cook = new List<CookieModel>();
            foreach (var c in _cookies)
            {
                temp_cook.Add(new CookieModel(c.Domain, c.Expiry, c.IsHttpOnly, c.IsHttpOnly, c.Name, c.Path, c.SameSite, c.Secure, c.Value));
            }
            return temp_cook.ToArray();
        }
    }
}
