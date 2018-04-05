using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.PageObjects
{
    public class NetAppLogin
    {
        private IWebDriver driver;
       

        public NetAppLogin(IWebDriver driver)
        {
            this.driver = driver;
         
        }
        public void GoToURL(string url )
        {
            driver.Navigate().GoToUrl(url);
        }

    }
}
