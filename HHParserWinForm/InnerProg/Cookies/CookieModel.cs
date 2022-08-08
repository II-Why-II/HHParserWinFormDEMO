using System;

namespace HHParserWinForm.InnerProg.Cookies
{
    class CookieModel
    {
        public CookieModel(string domain, DateTime? expirationDate, bool? hostOnly, bool? httpOnly, string name, string path, string sameSite, bool? secure, string value)
        {
            this.domain = domain;
            this.expirationDate = expirationDate;
            this.hostOnly = hostOnly;
            this.httpOnly = httpOnly;
            this.name = name;
            this.path = path;
            this.sameSite = sameSite;
            this.secure = secure;
            this.value = value;
        }
        public string domain { get; set; }
        public DateTime? expirationDate { get; set; }
        public bool? hostOnly { get; set; }
        public bool? httpOnly { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string sameSite { get; set; }
        public bool? secure { get; set; }
        public string value { get; set; }
    }
}
