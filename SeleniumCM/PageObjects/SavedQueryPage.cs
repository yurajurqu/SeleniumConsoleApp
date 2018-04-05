using OpenQA.Selenium;
using SeleniumCM.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeleniumCM.PageObjects
{
    public class SavedQueryPage
    {
        private IWebDriver driver;



        public SavedQueryPage(IWebDriver driver)
        {
            this.driver = driver;
            string savedQueryPageURLFormat=ConfigurationManager.AppSettings["URLBase"]+ConfigurationManager.AppSettings["SavedQueryPath"];
            string queryId ="";

            if (ConfigurationManager.AppSettings["RTCUser"].Equals("E700389"))
                queryId = ConfigurationManager.AppSettings["SavedQueryIDDifferentAccount"];
            else
                 queryId=ConfigurationManager.AppSettings["SavedQueryID"];
            driver.Navigate().GoToUrl(string.Format(savedQueryPageURLFormat, queryId));
            
        }
        public void downloadCSV()
        {

            driver.FindElement(By.CssSelector("img[alt='Download as Spreadsheet (.csv)']")).Click();
            Thread.Sleep(6000);
            ScreenshotHelper.TakeScreenshotReport(driver);
            Thread.Sleep(1000);
        }

    }
}
