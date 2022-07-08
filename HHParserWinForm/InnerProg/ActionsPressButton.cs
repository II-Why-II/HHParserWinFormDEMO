using HHParserWinForm.InnerProg.Parser;
using HHParserWinForm.InnerProg.Sender;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace HHParserWinForm.InnerProg
{
    class ActionsPressButton{
        SenderOfRequests send = new SenderOfRequests();
        public async Task<bool> StartButton(AboutBrowser _aboutBrowser, string _textBox1) {
            _aboutBrowser.Driver = new ChromeDriver(_aboutBrowser.DriverService, _aboutBrowser.ChromeOptions, TimeSpan.FromSeconds(100));
            Paths.PageLogin.PhoneNumber = _textBox1;
            await send.LoginSend(_aboutBrowser.Driver);
            return true;
        }
        public async Task LoginButton(AboutBrowser _aboutBrowser, string _textBox2){
            string smsCode = _textBox2;
            if (!string.IsNullOrEmpty(smsCode)){
                _aboutBrowser.Driver.FindElement(By.XPath(Paths.PageLogin.SmsWritingBox)).SendKeys(smsCode);
                MyExtensions.MakeRandomPause.LittlePause();
                _aboutBrowser.Driver.FindElement(By.XPath(Paths.PageLogin.SecondButton)).Click();
            }
            MyExtensions.MakeRandomPause.SetPause(4);
        }
        private static SubmittedExcel.CreatingExcelFile excel = new SubmittedExcel.CreatingExcelFile();
        private ProgramParser parse = new ProgramParser();
        
        public void SaveValuesButton(AboutBrowser _aboutBrowser, string _textBox3, string _textBox4, int _comboBox1Index){
            excel.AddingDataToTable();
            _aboutBrowser.SearchingWorlds = _textBox3.Replace("#", "%23");
            _aboutBrowser.Filter = _textBox4.Replace(",", "%2C").Replace(" ", "+");
            if (_comboBox1Index != -1)
                _aboutBrowser.NumberOfResume = _comboBox1Index;
        }
        public async Task GoToPageAndDoTheEvil(AboutBrowser _aboutBrowser, int _numberOfpage){
            _aboutBrowser.NumberOfSearchingPage++;
            AboutBrowser.NumberOfThreads++;

            GetStartPageUrl searchingUrl = new GetStartPageUrl(_aboutBrowser.SearchingWorlds, _aboutBrowser.Filter, _numberOfpage);
            await parse.GoToBasePageGetListOfLinksAndMakeTheResponcies(_aboutBrowser, excel, searchingUrl.LoadingSearchPage);

            MyExtensions.MakeRandomPause.MediumPause();
            excel.SavingExcel("ResponsesAfterPageIsEnd");
            if (MyExtensions.HaveABreak(_numberOfpage))
                MyExtensions.MakeRandomPause.MediumPause();
            AboutBrowser.NumberOfThreads--;
            System.Windows.Forms.MessageBox.Show("Рассылка окончена.");
        }
    }
}
