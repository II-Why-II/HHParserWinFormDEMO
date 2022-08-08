using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HHParserWinForm.InnerProg.Parser
{
    class ProgramParser
    {
        public async Task GoToBasePageGetListOfLinksAndMakeTheResponcies(Sender.AboutBrowser _aboutBrowser, SubmittedExcel.CreatingExcelFile _excel, string _searchingUrl)
        {
            var listOfLinks = await GetListOfLinksFromPageOrNull(_aboutBrowser, _searchingUrl);
            if (listOfLinks != null)
            {
                foreach (var link in listOfLinks)
                {
                    await MakeResponce(_aboutBrowser, _excel, link);
                    MyExtensions.MakeRandomPause.LittlePause();
                }
            }
        }
        private async Task<List<string>> GetListOfLinksFromPageOrNull(Sender.AboutBrowser _aboutBrowser, string _searchingUrl)
        {
            var thisPage = await GetBasePage(_aboutBrowser.Driver, _searchingUrl);
            if (!IsFinalPage(thisPage))
            {
                string[] thisPageArray = thisPage.Split('\"');
                List<string> resultList = new List<string>();
                foreach (string word in thisPageArray)
                { //search in base page
                    if (word.Contains("https://hh.ru/vacancy") && !word.Contains("script") && !word.Contains("from"))
                    {
                        resultList.Add(word);
                    }
                }
                return resultList;
            }
            else
                _aboutBrowser.FinalPage = true;
            return null;
        }
        private async Task<string> GetBasePage(IWebDriver _browser, string _searchingUrl)
        {
            try
            {
                _browser.Navigate().GoToUrl(_searchingUrl);
            }
            catch { }//throw new Exception("Error: loading page"); }
            MyExtensions.MakeRandomPause.MediumPause();

            Actions action = new Actions(_browser);
            IWebElement scrollingCharge = _browser.FindElement(By.XPath(Sender.Paths.PageParser.ScrollingChargeInBottom));
            action.ScrollToElement(scrollingCharge).MoveToElement(scrollingCharge);
            MyExtensions.MakeRandomPause.SmallPause();

            string thisPage = _browser.PageSource;
            return thisPage;
        }
        private VacancyPageParser parser = new VacancyPageParser();
        private async Task MakeResponce(Sender.AboutBrowser _aboutBrowser, SubmittedExcel.CreatingExcelFile excel, string _url)
        {
            string vacancyPage = GetPage(_aboutBrowser.Driver, _url);
            if (vacancyPage != string.Empty)
            {
                try
                {
                    PageModel thisPageModel = parser.ParsingPage(vacancyPage, _url);
                    if (NeedMakeResponceCheckingWords(thisPageModel))
                    {
                        try
                        {
                            if (NeedResponceCheckingDatabase(thisPageModel))
                            {
                                Sender.SenderOfRequests send = new Sender.SenderOfRequests();
                                bool isSended = await send.SendRequest(_aboutBrowser, _aboutBrowser.Driver, excel, thisPageModel);
                                if (isSended)
                                {
                                    DefaultStrings.MongoCollectionVacancies.InsertOne(thisPageModel);
                                }
                                _ = 1;
                            }
                        }
                        catch { /* throw new Exception("Error: cant send response"); */ }
                    }
                }
                catch { /* throw new Exception("Error: loading page"); */ }
                excel.SavingExcel("Responses");
            }
            _ = 1;
        }
        private string GetPage(IWebDriver _browser, string _url)
        {
            try
            {
                _browser.Navigate().GoToUrl(_url);
            }
            catch {
                return string.Empty;
            }//throw new Exception("Error: loading page"); }
            MyExtensions.MakeRandomPause.SmallPause();
            string thisPage = _browser.PageSource;
            return thisPage;
        }
        private bool NeedMakeResponceCheckingWords(PageModel _pageModel)
        {
            if (_pageModel.ExperienceYear.ToLower().Contains("более 6 лет"))
            {
                return false;
            }
            foreach (var word in DefaultStrings.IgnoreVacanciesByName)
            {
                if (_pageModel.VacancyName.ToLower().Contains(word))
                {
                    return false;
                }
            }
            foreach (var word in DefaultStrings.MustHaveAnyTagInVacanceName)
            {
                if (_pageModel.VacancyName.ToLower().Contains(word))
                {
                    return true;
                }
            }
            return false;
        }
        private bool NeedResponceCheckingDatabase(PageModel _pageModel)
        {
            var mongoCollectionVacancies = DefaultStrings.MongoDatabaseHHParse.GetCollection<PageModel>("Vacancies");
            var filter = Builders<PageModel>.Filter.Eq(e => e.EmployerName, _pageModel.EmployerName);

            var filtredVacancies = mongoCollectionVacancies.Find(filter).ToList();
            foreach (var vac in filtredVacancies)
            {
                if (vac.EmployerName == _pageModel.EmployerName && vac.VacancyName == _pageModel.VacancyName)
                    return false;
            }
            return true;
        }
        private bool IsFinalPage(string _thisPage)
        {
            if (_thisPage.Contains("error-content-wrapper"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
