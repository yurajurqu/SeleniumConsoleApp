using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.Helpers
{
    class DelayHelper
    {

        public static void Delay(IWebDriver driver, int s)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(s);
        }
    }
}
