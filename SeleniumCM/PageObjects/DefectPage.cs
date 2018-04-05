using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeleniumCM.PageObjects
{
    public class DefectPage
    {
        private IWebDriver driver;


        public DefectPage(IWebDriver driver)
        {
            this.driver = driver;
         
        }
        public void GoToURL(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public void CreateDefect(DefectDTO def)
        {
            Thread.Sleep(2000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));


            var codigoTextbox = driver.FindElements(By.ClassName("RichTextEditorWidget"))[0];
            codigoTextbox.SendKeys(def.Codigo);

            var severidadSpan = driver.FindElements(By.ClassName("ValueLabelHolder"))[1];
            severidadSpan.Click();
            Console.WriteLine(def.Severidad);
            switch (def.Severidad)
            {
                case "Grave":
                    wait.Until(d => driver.FindElement(By.XPath("//span[text() = 'Grave']")));

                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    break;
                case "Moderado":
                    wait.Until(d => driver.FindElement(By.XPath("//span[text() = 'Moderado']")));
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    break;

                case "Leve":
                    wait.Until(d => driver.FindElement(By.XPath("//span[text() = 'Leve']")));

                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    break;

                default: break;

            }
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);

            var prioridadSpan = driver.FindElements(By.ClassName("ValueLabelHolder"))[5];
            prioridadSpan.Click();
            Console.WriteLine(def.Prioridad);
            switch (def.Prioridad)
            {
                case "Baja":
                    wait.Until(d => driver.FindElement(By.XPath("//span[text() = 'Baja']")));
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    break;
                case "Medio":
                    wait.Until(d => driver.FindElement(By.XPath("//span[text() = 'Medio']")));
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    break;
                case "Alta":
                    wait.Until(d => driver.FindElement(By.XPath("//span[text() = 'Medio']")));
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
                    break;

                default: break;
            }
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);

            
            var nombreAppspan = driver.FindElements(By.ClassName("ValueLabelHolder"))[11];
            nombreAppspan.Click();
            Console.WriteLine("Nombre App {0}", def.NombreAplicacion);
            driver.SwitchTo().ActiveElement().SendKeys(def.NombreAplicacion);
            wait.Until(d => driver.FindElement(By.XPath("//span[@class='Highlight'][text() = '" + def.NombreAplicacion + "']")));
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);


            //todo planificado para



            var codigoProyFallaTextbox = driver.FindElements(By.ClassName("RichTextEditorWidget"))[1];
            codigoProyFallaTextbox.SendKeys(def.CodigoProyectoFalla);

            var tipoDefectoSpan = driver.FindElements(By.ClassName("ValueLabelHolder"))[7];
            tipoDefectoSpan.Click();
            Console.WriteLine("Tipo defecto {0}",def.TipoDefecto);
            driver.SwitchTo().ActiveElement().SendKeys(def.TipoDefecto);
            wait.Until(d => driver.FindElement(By.XPath("//span[@class='Highlight'][text() = '"+def.TipoDefecto+"']")));
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);

            var descripcionTextbox = driver.FindElements(By.ClassName("RichTextEditorWidget"))[6];
            descripcionTextbox.SendKeys(def.Descripcion);

            var comentariosTextbox = driver.FindElements(By.ClassName("RichTextEditorWidget"))[7];
            comentariosTextbox.SendKeys(def.Comentarios);

           //todo
            //gerencia
            //    proveedores
            //    proceso negocio

           
            //            adjunto
            //add parent WI autorizacion
                
        }
    }
}
