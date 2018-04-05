using OpenQA.Selenium;
using SeleniumCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumCM.PageObjects
{
    public class SolPage
    {
        private IWebDriver driver;


        public SolPage(IWebDriver driver)
        {
            this.driver = driver;
         
        }
        public void GoToURL(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void FillAttributes(DefectDTO defect)
        {
            string prm = "";
            driver.FindElement(By.LinkText("Enlaces")).Click();
            //driver.FindElement(By.CssSelector("span[aria-label='Anexos']")).Click();

           

            //driver.FindElements(By.XPath("//div[contains(text(),'Parent')]/following::div"))[6].Click();
            var el = driver.FindElement(By.XPath("//a[@class='jazz-ui-ResourceLink'][contains(text(),':')]"));
            //Console.WriteLine(el.GetAttribute("innerHTML"));
            el.Click();

            
            var valuelabelHolders = driver.FindElements(By.ClassName("ValueLabelHolder"));
            //app siempre es index 0

            //ceq
            var ceq = valuelabelHolders[1].GetAttribute("innerHTML");
            Console.WriteLine("ceq {0}", ceq);
            defect.Ceq = ceq;

            //gerencia
            var gerencia = valuelabelHolders[3].GetAttribute("innerHTML");
            Console.WriteLine("gerencia {0}", gerencia);
            defect.Gerencia = gerencia;
            //planned for
            var plannedFor = valuelabelHolders[5].GetAttribute("innerHTML");
            Console.WriteLine("plannedFor {0}", plannedFor);
            defect.PlannedFor = plannedFor;
            //proveedor
            var proveedor = valuelabelHolders[7].GetAttribute("innerHTML");
            Console.WriteLine("proveedor {0}", proveedor);
            defect.Proveedor = proveedor;

            //coordinador
            var coordinador = valuelabelHolders[10].GetAttribute("innerHTML");
            Console.WriteLine("coordinador {0}", coordinador);
            defect.Coordinador = coordinador;

            var richtexts = driver.FindElements(By.ClassName("RichTextEditorWidget"));
            foreach (var item in richtexts)
            {
                Console.WriteLine(item.GetAttribute("innerHTML"));
            }
            var codigoProy = richtexts[3].GetAttribute("innerHTML");
            defect.CodigoProyectoFalla = codigoProy;
           //RichTextEditorWidget  16  fallaproyecto 3
            Console.WriteLine(defect);
            
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
