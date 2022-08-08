using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;

namespace HHParserWinForm.InnerProg.Sender
{
    class AboutBrowser
    {
        public bool FinalPage = new bool();
        public int NumberOfSearchingPage = 1;
        public bool BrowserIsStarted = new bool();
        public string SearchingWorlds = "";
        public string Filter = "";

        public static int NumberOfThreads = 0;
        public IWebDriver Driver;
        public ChromeOptions ChromeOptions = new ChromeOptions();
        public ChromeDriverService DriverService = ChromeDriverService.CreateDefaultService();

        public string Cookies = "";
        public int NumberOfResume = new int();
        public int CountSubmittedResume = 0;

        public StringBuilder autorizationErrors = new StringBuilder();
        public StringBuilder verificationErrors = new StringBuilder();
        public int exitError = 0;
        public int SendingErrors = 0;

        public void SetOptions()
        {
            ChromeOptions.AddArguments("--disable-blink-features=AutomationControlled");
            ChromeOptions.AddArgument("ignore-certificate-errors");
            //chromeOptions.AddArguments($"--proxy-server=socks5://{textBox5.Text}");
        }
    }
}