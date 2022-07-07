using HHParserWinForm.InnerProg.Parser;
using HHParserWinForm.InnerProg.SubmittedExcel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HHParserWinForm.InnerProg.Sender
{
    class SenderOfRequests{
        public async Task<bool> SendRequest(AboutBrowser _aboutBrowser, IWebDriver _browser, CreatingExcelFile excel, PageModel _vacance){
            string id = _vacance.Link.Replace("https://hh.ru/vacancy/", "");
            bool submitted = await CallingResponses(_aboutBrowser, _browser, "https://hh.ru" + _vacance.ResponseLinkHaha, id, MyExtensions.GetRefererString(id));
            _ = 1;
            if (submitted) { 
                excel.AddingDataToTable(_vacance);
                return true;
            }
            else
                return false;
        }
        public async Task LoginSend(IWebDriver _browser){
            GetStartPageUrl loginPage = new GetStartPageUrl("/account/login?backurl=%2F&hhtmFrom=main");
            string url = loginPage.StartPage;
            try{
                _browser.Navigate().GoToUrl(url);
            }
            catch {
                return;
            }
            try{
                ((ITakesScreenshot)_browser).GetScreenshot();
            }
            catch { }

            _browser.FindElement(By.XPath(Paths.PageLogin.TelephoneBox)).SendKeys(Paths.PageLogin.PhoneNumber);
            MyExtensions.MakeRandomPause.LittlePause();
            _browser.FindElement(By.XPath(Paths.PageLogin.FirstButton)).Click();
        }
        private async Task<bool> CallingResponses(AboutBrowser _aboutBrowser, IWebDriver _browser, string _urlReaponse, string _id, string _referer){
            //ClickingOnPageResponce(_browser, _url);
            bool submited = await MakeResponse(_aboutBrowser, _browser, _urlReaponse);
            //_ = GetResponcePage(_about, _url, _referer);
            _ = 1;
            return submited;
        }
        private void ClickingOnPageResponce(IWebDriver _browser, string _url){
            try{
                _browser.Navigate().GoToUrl(_url);
            }
            catch { }
            try{
                _ = ((ITakesScreenshot)_browser).GetScreenshot();
            }
            catch { }

            _browser.FindElement(By.XPath(Paths.NewPageVacancy.IsResponsed)); 

            Actions action = new Actions(_browser);
            IWebElement responseButton = _browser.FindElement(By.XPath(Paths.NewPageVacancy.ResponseButton));

            action.ScrollToElement(responseButton)
                .MoveToElement(responseButton)
                .Click(responseButton);
            Thread.Sleep(3000);
            _ = 1;
        }
        private async Task<bool> MakeResponse(AboutBrowser _aboutBrowser, IWebDriver _browser, string _responseUrl){
            try{
                _browser.FindElement(By.XPath(Paths.NewPageVacancy.IsResponsed));
                return false;
            }
            catch{
                try{
                    try{
                        _browser.Navigate().GoToUrl(_responseUrl);
                        MyExtensions.MakeRandomPause.SmallPause();
                        ((ITakesScreenshot)_browser).GetScreenshot();
                    }
                    catch (Exception){ }
                    try {
                        if (_aboutBrowser.NumberOfResume > 0){
                            var _r = _browser.FindElements(By.XPath(Paths.NewPageVacancy.ChangeResume)); 
                            _r[_aboutBrowser.NumberOfResume].Click(); 
                            MyExtensions.MakeRandomPause.MicroPause();
                        }
                    }
                    catch { }
                    try {
                        _browser.FindElement(By.XPath(Paths.NewPageVacancy.SecondResponseSendButton)).Click();
                        MyExtensions.MakeRandomPause.LittlePause();
                    }
                    catch { }
                    try{
                        _browser.FindElement(By.XPath(Paths.NewPageVacancy.NeedLetter));
                        MyExtensions.MakeRandomPause.LittlePause();
                        _browser.FindElement(By.XPath(Paths.NewPageVacancy.IsLetter)).Click();
                        MyExtensions.MakeRandomPause.MicroPause();
                        _browser.FindElement(By.XPath(Paths.NewPageVacancy.IsLetter)).SendKeys(Paths.NewPageVacancy.Message);
                        MyExtensions.MakeRandomPause.MicroPause();
                        _browser.FindElement(By.XPath(Paths.NewPageVacancy.SecondResponseSendButton)).Click();
                        MyExtensions.MakeRandomPause.LittlePause();
                    }
                    catch{
                        MyExtensions.MakeRandomPause.MicroPause();
                    }
                        
                    try{
                        _browser.FindElement(By.XPath(Paths.NewPageVacancy.AnyCountry)).Click();
                        MyExtensions.MakeRandomPause.LittlePause();
                    }
                    catch { }
                    try{
                         var _r = _browser.FindElement(By.XPath(Paths.NewPageVacancy.ErrorFillDailyRequests));
                        if (_r != null){
                            _aboutBrowser.FinalPage = true;
                            return false;
                        }
                    }
                    catch{ }

                    return true;
                }
                catch { 
                    return false; 
                }
            }
        }
        private Cookies.CookieAwareWebClient client = new Cookies.CookieAwareWebClient();
        private string GetResponcePage(AboutBrowser _about, string _url, string _referer){
            if (client.Headers.AllKeys.Contains("referer")){
                if (string.IsNullOrEmpty(_referer))
                    client.Headers.Remove("referer");
                else
                    client.Headers["referer"] = _referer;
            }
            else{
                if (!string.IsNullOrEmpty(_referer))
                    client.Headers.Add("referer", _referer);
            }

            _about.Cookies = Cookies.FileToCookies.GetCookieStringFromFile();
            if (client.Headers.AllKeys.Contains("cookie")){
                if (!string.IsNullOrEmpty(_about.Cookies))
                    client.Headers["cookie"] = _about.Cookies;
            }
            else{
                if (!string.IsNullOrEmpty(_about.Cookies))
                    client.Headers.Add("cookie", _about.Cookies);
            }

            string result = client.DownloadString(_url);
            
            var cookies = client.ResponseHeaders["Set-cookie"];
            File.WriteAllText("ResponseHeaders-Set-cookie.txt", cookies);

            
            var cc = Cookies.GetAllCookies.GetAllCookiesFromHeader(client.ResponseHeaders["Set-Cookie"], "hh.ru");
            string rr = "";
            foreach (var cookie in cc)
            {
                rr += cookie + "; ";
            }
            cookies = rr.TrimEnd().TrimEnd(';');
            

            Console.WriteLine("Waiting 5 sec");
            Thread.Sleep(5000);
            return result;
        }
    }
}
