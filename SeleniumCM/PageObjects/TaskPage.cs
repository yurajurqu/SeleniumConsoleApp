using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumCM.Helpers;
using SeleniumCM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeleniumCM.PageObjects
{
    class TaskPage
    {

        log4net.ILog log = log4net.LogManager.GetLogger(typeof(TaskPage));
        string tareaId="";
        string newTaskId;
        string timeSpent;
        string taskCycle;
        string taskDueDate;
        string taskPlannedFor;
        string streamsInstalled;
        string taskName;
        string description;
        string taskStartTime;
        string ticketUser;
        TimeTask timeTask = new TimeTask();

        private IWebDriver driver;
        public string TM { get; set; }

        [FindsBy(How = How.ClassName, Using = "ValueLabelHolder")]//ValueLabelHolder ValueHolder
        private IList<IWebElement> valueHolders;


        [FindsBy(How = How.ClassName, Using = "ValueHolder")]//ValueLabelHolder ValueHolder
        private IList<IWebElement> divValueHolders;

        public void GoTo(int taskcode)
        {
            driver.Navigate().GoToUrl(String.Format(ConfigurationManager.AppSettings["ResourcePage"], taskcode));
        }

        public TaskPage(IWebDriver driver, string TM, TimeTask timeTask)
        {
            this.driver = driver;
            this.TM = TM;

            PageFactory.InitElements(driver, this);

            this.timeTask = timeTask;




            if (timeTask == null)
            {
                timeSpent = ConfigurationManager.AppSettings["TimeSpentHr"];
                description = ConfigurationManager.AppSettings["description"];
                taskStartTime = ConfigurationManager.AppSettings["TaskStartTime"];
                ticketUser = ConfigurationManager.AppSettings["ticketUser"];
                taskName = ConfigurationManager.AppSettings["taskName"];
                taskCycle = ConfigurationManager.AppSettings["taskCycle"];
                streamsInstalled = ConfigurationManager.AppSettings["StreamsInstalled"] == "" ? "1" : ConfigurationManager.AppSettings["StreamsInstalled"];
                taskPlannedFor = ConfigurationManager.AppSettings["taskPlannedFor"] == "" ? DateTime.Today.ToString("MMMM-yy") : ConfigurationManager.AppSettings["taskPlannedFor"];
                taskDueDate = ConfigurationManager.AppSettings["taskDueDate"] == "" ? DateTime.Today.ToString("MMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US")) : ConfigurationManager.AppSettings["taskDueDate"];
            }
            else
            {
                timeSpent = timeTask.TimeSpentHr;
                description = timeTask.Description;
                taskStartTime = timeTask.TaskStartTime;
                ticketUser = timeTask.TicketUser;
                taskName = timeTask.TaskName;
                taskCycle = timeTask.TaskCycle;
                streamsInstalled = timeTask.StreamsInstalled == null ? "1" : timeTask.StreamsInstalled;
                taskPlannedFor = timeTask.TaskPlannedFor == null ? DateTime.Today.ToString("MMMM-yy") : timeTask.TaskPlannedFor;
                taskDueDate = timeTask.TaskDueDate == null ? DateTime.Today.ToString("MMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US")) : timeTask.TaskDueDate;
            }
        }


        public void UpdateTask()
        {







            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("select.Select[aria-label='Status'] option[value='com.ibm.team.workitem.taskWorkflow.action.startWorking']")));

                var selects = driver.FindElements(By.CssSelector("select.Select[aria-label='Status']"));
                foreach (var item in selects)
                {
                    Console.WriteLine("****");
                    Console.WriteLine(item.GetAttribute("innerHTML"));
                }

                var selectorState = driver.FindElements(By.CssSelector("select.Select[aria-label='Status']"))[1];
                Console.WriteLine(selectorState.GetAttribute("innerHTML"));
                var selectElement = new SelectElement(selectorState);
                selectElement.SelectByText("En Trabajo");



                var saveBtn = driver.FindElements(By.CssSelector("button.primary-button"))[1];
                saveBtn.Click();

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

                wait.Until(d => !d.FindElements(By.ClassName("TitleText"))[1].Text.Contains('*'));


                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("com_ibm_team_apt_web_ui_internal_parts_TimeSpentAttributePart_3")));
                driver.FindElement(By.Id("com_ibm_team_apt_web_ui_internal_parts_TimeSpentAttributePart_3")).Click();
                driver.SwitchTo().ActiveElement().Clear();
                driver.SwitchTo().ActiveElement().SendKeys(timeSpent);
                driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);
                driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);

                var descriptionField = driver.FindElement(By.CssSelector("div[aria-label='Rich Text Editor, editor10']"));
                descriptionField.Click();
                driver.SwitchTo().ActiveElement().SendKeys(Keys.Shift + Keys.Home);
                driver.SwitchTo().ActiveElement().SendKeys(Keys.Delete);
                driver.SwitchTo().ActiveElement().SendKeys(description == null ? "" : description);



                if (taskStartTime != "" && taskStartTime != null)
                {
                    var startInput = driver.FindElement(By.CssSelector("input[aria-labelledby='Start Work Date']"));
                    startInput.Clear();
                    startInput.SendKeys(taskStartTime);
                    startInput.SendKeys(Keys.Enter);

                }


                saveBtn = driver.FindElements(By.CssSelector("button.primary-button"))[1];
                saveBtn.Click();

                wait.Until(d => !d.FindElements(By.CssSelector("span.TitleText"))[1].GetAttribute("innerHTML").Contains('*'));
                 tareaId = driver.FindElements(By.CssSelector("span.TitleText"))[1].GetAttribute("innerHTML").Replace("Tarea ", "");

                var spans = driver.FindElements(By.CssSelector("span.TitleText"));
                foreach (var span in spans)
                {
                    Console.WriteLine("*****");
                    Console.WriteLine(span.GetAttribute("innerHTML"));
                    Console.WriteLine("*****");
                }
                Console.WriteLine("Nueva tarea {0}", tareaId);
                newTaskId = tareaId;
                log.InfoFormat("Tarea creada {0} --> ticket {1}", tareaId, timeTask.IdTicket);

                CloseTask("Completada");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error udpating inicio******************************");
                Console.WriteLine(e.StackTrace);

                Console.WriteLine(e.Message);
                var selectorState = driver.FindElements(By.CssSelector("select.Select[aria-label='Status']"))[1];

                var selectElement = new SelectElement(selectorState);
                selectElement.SelectByText("Invalida");
                var saveBtn = driver.FindElements(By.CssSelector("button.primary-button"))[1];
                saveBtn.Click();
                Console.WriteLine("Error udpating fin ******************************");
                Console.WriteLine(e.ToString());
                
                log.ErrorFormat("Tarea invalidada {0} --> ticket {1}", tareaId, timeTask.IdTicket);
                log.Error(e.StackTrace);
                log.Error(e.ToString());
            }
        }

        public void CloseTask(string state)
        {

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("select.Select[aria-label='Status'] option[value='com.ibm.team.workitem.taskWorkflow.action.complete']")));
            var selectorState = driver.FindElements(By.CssSelector("select.Select[aria-label='Status']"));

            foreach (var select in selectorState)
            {
                Console.WriteLine("$$$$$$$$$$$$$$$$$$$$");
                Console.WriteLine(select.GetAttribute("innerHTML"));
            }
            Console.WriteLine("####################");
            Console.WriteLine(selectorState[1].GetAttribute("innerHTML"));


            var selectElement = new SelectElement(selectorState[1]);

            Thread.Sleep(4000);
            selectElement.SelectByText(state);  //Invalida


            var btns = driver.FindElements(By.CssSelector("button.primary-button"));
            foreach (var item in btns)
            {
                Console.WriteLine("-----");
                Console.WriteLine(item.GetAttribute("innerHTML"));
            }

            var saveBtn = driver.FindElements(By.CssSelector("button.primary-button"))[1];
            saveBtn.Click();

            wait.Until(d => !d.FindElements(By.CssSelector("span.TitleText"))[1].GetAttribute("innerHTML").Contains('*'));
            Thread.Sleep(2000);
            ScreenshotHelper.TakeScreenshotTimeTask(driver, newTaskId);


            string LogText = string.Format("{0},{1},{2},{3}", newTaskId, timeTask.IdTicket, timeTask.TimeSpentHr, timeTask.TaskName);
            LoggerHelper.LogWrite(LogText);

            //en trabajocom.ibm.team.workitem.taskWorkflow.state.s2
            //invalida com.ibm.team.workitem.taskWorkflow.action.a1

        }

        public void CreateTask2()
        {



            divValueHolders[1].Click();
            var currentInput = driver.FindElements(By.CssSelector("input[dojoattachpoint='_searchInput']"))[2];

            Console.WriteLine(currentInput.GetAttribute("innerHTML"));

            currentInput.SendKeys(taskName);


            //input dojoattachpoint _searchInput  index 2 (ie 3rd)
            Thread.Sleep(1000);//10-Registro de Defectos   14-Control de Calidad
            //wait for highlight
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("span.Highlight")));

            currentInput.SendKeys(Keys.Enter);

            divValueHolders[2].Click();
            currentInput = driver.SwitchTo().ActiveElement();
            currentInput.SendKeys(taskPlannedFor);
            Thread.Sleep(2000);
            currentInput.SendKeys(Keys.Down);
            currentInput.SendKeys(Keys.Enter);

            divValueHolders[3].Click();
            currentInput = driver.SwitchTo().ActiveElement();
            currentInput.SendKeys(ticketUser);
            Thread.Sleep(4000);
            wait.Until(d => d.FindElement(By.CssSelector("span.Highlight")).GetAttribute("innerHTML").Contains(ticketUser));
            Console.WriteLine("debugging");
            Console.WriteLine(driver.FindElement(By.CssSelector("span.Highlight")).GetAttribute("innerHTML").Contains(ticketUser));
            currentInput.SendKeys(Keys.Enter);

       



            divValueHolders[4].Click();
            currentInput = driver.SwitchTo().ActiveElement();
            currentInput.SendKeys(this.TM);
            Thread.Sleep(2000);
            currentInput.SendKeys(Keys.Enter);

            divValueHolders[5].Click();
            currentInput = driver.SwitchTo().ActiveElement();
            currentInput.SendKeys(Keys.Down);
            currentInput.SendKeys(Keys.Enter);


            var cycleInput = driver.FindElement(By.CssSelector("input[aria-labelledby='Ciclo']"));
            cycleInput.Clear();
            cycleInput.SendKeys(taskCycle);


            var dateInput = driver.FindElement(By.CssSelector("input[aria-labelledby='Due Date']"));
            dateInput.Clear();
            dateInput.SendKeys(taskDueDate);


            if (!taskName.Contains("Defectos"))
            {
                var streamsInstalledInput = driver.FindElement(By.CssSelector("input[aria-labelledby='Streams Instalados']"));
                streamsInstalledInput.Clear();

                streamsInstalledInput.SendKeys(streamsInstalled);
            }






            var saveBtn = driver.FindElements(By.CssSelector("button.primary-button"))[1];
            saveBtn.Click();
        }
    }
}
