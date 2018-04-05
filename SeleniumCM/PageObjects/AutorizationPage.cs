using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.PageObjects
{
    public class AutorizationPage
    {
        private IWebDriver driver;


        public AutorizationPage(IWebDriver driver)
        {
            this.driver = driver;
         
        }
        public void GoToURL(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public string Father()
        {
            string prm = "";
            driver.FindElement(By.LinkText("Enlaces")).Click();
            driver.FindElement(By.CssSelector("span[aria-label='Anexos']")).Click();

            var strikes = driver.FindElements(By.TagName("strike"));
            foreach (var item in strikes)
            {
                
                if (item.GetAttribute("innerHTML").Contains("PRM."))
                {
                     prm = item.GetAttribute("innerHTML").Split(':')[0];
                    Console.WriteLine("prm is {0}",prm);
                    break;
                }
            }

            return prm;
        }

        public string GrandFather()
        {
            return "";
        }

        public string AreaProyecto()
        {
            var tds = driver.FindElements(By.CssSelector("td.HistoryColumn1"));
            var areaproyecto = "";
            foreach (var td in tds)
            {
                if(td.GetAttribute("innerHTML").Contains(".GESTION."))
                {
                    if (td.GetAttribute("innerHTML").Contains("01.GESTION.SOLICITUDES.1"))
                    { areaproyecto = "01.GESTION.SOLICITUDES.1"; return areaproyecto; }
                if (td.GetAttribute("innerHTML").Contains("01.GESTION.SOLICITUDES.2"))
                { areaproyecto = "01.GESTION.SOLICITUDES.2"; return areaproyecto; };
                if (td.GetAttribute("innerHTML").Contains("02.GESTION.FALLAS.1"))
                { areaproyecto = "02.GESTION.FALLAS.1"; return areaproyecto; }
                
                }

                
            }
            areaproyecto = "NA";
            return areaproyecto;
        }
    }
}
