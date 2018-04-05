using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumCM.Helpers;
using SeleniumCM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeleniumCM.PageObjects
{
    class NewTaskPage
    {
        private int TicketId { get; set; }
        private IWebDriver driver;

        
        [FindsBy(How = How.ClassName, Using = "DropdownArrowElement")]
        private IList<IWebElement> dropdownElements;

        [FindsBy(How = How.Id, Using = "dijit_MenuItem_305_text")]
        [FindsBy(How = How.CssSelector, Using = "#com_ibm_team_workitem_web_ui_internal_view_layout_NameValueLayout_1 > table > tbody > tr:nth-child(3) > td.com-ibm-team-workitem-namevalue-value-td > span > select")]
        private IWebElement addChildrenSpan;


    [FindsBy(How=How.ClassName,Using="selectSelect")]
        private IList<IWebElement> WITypeSelector;



        [FindsBy(How = How.ClassName, Using = "createItemLink")]
        private IWebElement createLink;



        [FindsBy(How = How.ClassName, Using = "ValueLabelHolder")]
        private IList<IWebElement> valueLabelHolders;

        public NewTaskPage(IWebDriver driver,  int ticketId)
        {
            this.driver = driver;
            this.TicketId=ticketId;
            PageFactory.InitElements(driver, this);
        }

        
          public void VisitTicketLinksTab(TimeTask task)
        {
              
            var url = string.Format(ConfigurationManager.AppSettings["ResourcePage"], this.TicketId);
            //driver.Navigate().Refresh();
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Navigate().GoToUrl(url);

          

            
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            //Console.WriteLine("source");
            //Console.WriteLine(driver.PageSource);
           var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(d => d.FindElement(By.ClassName("TitleText")).GetAttribute("innerHTML").Contains(this.TicketId.ToString()));
            Console.WriteLine("working id {0}",driver.FindElement(By.ClassName("TitleText")).GetAttribute("innerHTML"));

           
            
              var labelHolders=driver.FindElements(By.ClassName("ValueLabelHolder"));


              string TMString = "";

              string cycle = "";
            //0 es Ticket Falla para ticket de fallas
            var ticketType = labelHolders[1].Text;
            Console.WriteLine("Ticket type {0}",ticketType);
            if (ticketType == "Falla") //falla
            {
                TMString=labelHolders[9].Text;
            }
            else  //solicitud
            {
                TMString = labelHolders[10].Text;
            }
            Console.WriteLine("TM is {0}",TMString);
  
            driver.FindElement(By.LinkText("Enlaces")).Click();
         
              //relationship dropdown index is 1
            var spans = driver.FindElements(By.CssSelector("span.DropdownArrow")); //DropdownArrowElement
           
            var dropDown = driver.FindElements(By.CssSelector("span.DropdownArrow"))[1];

              
            dropDown.Click();
            
            DelayHelper.Delay(driver, 10);


            // mouseover dijit_MenuItem_23_text id y 6 down 
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.Id("dijit_MenuItem_4_text"))).Perform();
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Down);
           

             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("dijit_MenuItem_9_text"))));
          
                var addchildren = driver.FindElement(By.Id("dijit_MenuItem_9_text"));
                addchildren.Click();
                DelayHelper.Delay(driver, 20);

              

                //#com_ibm_team_workitem_web_ui_internal_view_layout_NameValueLayout_0 > table > tbody > tr:nth-child(3) > td.com-ibm-team-workitem-namevalue-value-td > span > select
                var selections = driver.FindElements(By.ClassName("selectSelect"));
                Console.WriteLine("nro:{0}", selections.Count);
                Console.WriteLine("nro:{0}", selections[0].Text);
                var selector = selections[0];
                
                var selectElement = new SelectElement(selector);

               

                wait.Until(d=>d.FindElements(By.ClassName("selectSelect")).Count==2);
                Console.WriteLine(driver.FindElements(By.ClassName("selectSelect"))[1].GetAttribute("innerHTML"));
                wait.Until(d => new SelectElement( d.FindElements(By.ClassName("selectSelect"))[1]).Options.Count == 13);
                Console.WriteLine("done waiting for 13 options");
             
                Console.WriteLine("*****Selecting tarea option******");
                Console.WriteLine(driver.FindElements(By.ClassName("selectSelect"))[1].GetAttribute("innerHTML"));

                var selectOptions = new SelectElement(driver.FindElements(By.ClassName("selectSelect"))[1]);
                selectOptions.SelectByText("Tarea");
               
              var wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
              wait2.Until(d => d.FindElement(By.ClassName("createItemLink")).Text == "Create Linked Tarea...");


              driver.FindElement(By.ClassName("createItemLink")).Click();
            

                var taskPage = new TaskPage(driver,TMString,task);
                taskPage.CreateTask2();
               taskPage.UpdateTask();
         
            

            

        }
    }
}
