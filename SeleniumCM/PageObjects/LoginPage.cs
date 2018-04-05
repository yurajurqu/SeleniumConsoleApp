using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.PageObjects
{
    class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
           // PageFactory.InitElements(driver, this);
        }

        //[FindsBy(How=How.Id,Using="jazz_app_internal_LoginWidget_0_userId")]
        //private IWebElement inputUser;
        //[FindsBy(How=How.Id,Using="jazz_app_internal_LoginWidget_0_password")]
        //private IWebElement inputPass;
        //[FindsBy(How=How.XPath,Using="//button[@type='submit']")]
        //private IWebElement loginButton;

        public void VisitLoginPage()
        {
            driver.Navigate().GoToUrl("https://172.19.112.112:9443/jts/auth/authrequired");

     
           //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;



          // driver.Navigate().Refresh();
        }
        public void Login(string user, string pass)
        {
            Console.WriteLine("user {0}",user);
            Console.WriteLine("pass {0}", pass);
            var inputUser = driver.FindElement(By.Id("jazz_app_internal_LoginWidget_0_userId"));
            inputUser.SendKeys(user);
            var inputPass = driver.FindElement(By.Id("jazz_app_internal_LoginWidget_0_password"));
            inputPass.SendKeys(pass);
            var loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            loginButton.Click();
        } 
    }
}
