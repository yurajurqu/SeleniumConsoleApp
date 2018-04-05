using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.PageObjects
{
    class CMAttentionPage
    {
        private IWebDriver driver;

        public CMAttentionPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public void VisitAttentionPage(int id)
        {
            var url = String.Format("https://172.19.112.112:9443/ccm/web/projects/CLARO.CERTIFICACION#action=com.ibm.team.workitem.viewWorkItem&id={0}&tab=esfuerzoscm", id);
            driver.Navigate().GoToUrl(url);
            driver.Navigate().Refresh();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [FindsBy(How = How.ClassName, Using = "DetailsSplitTable")]
        private IWebElement table;

        [FindsBy(How = How.CssSelector, Using = "button.primary-button")]
        private IWebElement saveBtn;

        
        
    }
}
