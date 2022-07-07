using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HHParserWinForm.InnerProg{
    public static class MyExtensions{
        public static string GetRefererString(this string _id) => "https://hh.ru/vacancy/" + _id; // + "?from=vacancy_search_list&hhtmFrom=vacancy_search_list&query=c%23";
        public static bool HaveABreak(this int countOfLoop){
            int result = countOfLoop % 10;
            if (result == 0)
                return true;
            else
                return false;
        }
        public static class MakeRandomPause{
            public async static void MicroPause(){
                Random rdm = new Random();
                await Task.Delay(rdm.Next(500, 1000));
            }
            public async static void LittlePause(){
                Random rdm = new Random();
                await Task.Delay(rdm.Next(1000, 2000));
            }
            public async static void SmallPause(){
                Random rdm = new Random();
                await Task.Delay(rdm.Next(6000, 10000));
            }
            public async static void MediumPause(){
                Random rdm = new Random();
                await Task.Delay(rdm.Next(10000, 20000));
            }
            public async static void LongPause(){
                Random rdm = new Random();
                await Task.Delay(rdm.Next(100000, 200000));
            }
            public async static void SetPause(float _seconds){
                int timer = Convert.ToInt32(_seconds * 1000);
                await Task.Delay(timer);
            }
        }
    }
    
    public static class WebDriverExtensions{
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds){
            if (timeoutInSeconds > 0){
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
