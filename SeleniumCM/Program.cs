using AutoIt;
using LinqToExcel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumCM.Helpers;
using SeleniumCM.Models;
using SeleniumCM.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SeleniumCM
{
    class Program
    {
        public static IWebDriver driver = null;

        public static void Login()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string driverFolder = Path.Combine(currentDirectory, ConfigurationManager.AppSettings["DriversDirectory"]);

            string downloadsDirectory = Path.Combine(currentDirectory, ConfigurationManager.AppSettings["DownloadsDirectory"]);
            if (!Directory.Exists(downloadsDirectory))
                Directory.CreateDirectory(downloadsDirectory);

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-infobars");
            options.AddUserProfilePreference("download.default_directory", downloadsDirectory);
            options.AddUserProfilePreference("disable-popup-blocking", "true");

            driver = new ChromeDriver(driverFolder, options);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            //login
            LoginPage login = new LoginPage(driver);
            login.VisitLoginPage();
            Console.WriteLine("user {0}", ConfigurationManager.AppSettings["RTCUser"]);
            Console.WriteLine("pass {0}", ConfigurationManager.AppSettings["RTCPass"]);
            login.Login(ConfigurationManager.AppSettings["RTCUser"], ConfigurationManager.AppSettings["RTCPass"]);

        }
        public static void Logout()
        {
            driver.Close();
        }

        static void Main(string[] args)
        {

            log4net.Config.BasicConfigurator.Configure();
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

            log.Info("Starting");

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("es");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            Console.WriteLine("Options ");
            Console.WriteLine("1: Cerrar Actividades CM");
            Console.WriteLine("2: Crear Tarea");
            Console.WriteLine("3: Cerrar Tarea");
            Console.WriteLine("4: Crear Tareas (excel)");
            Console.WriteLine("5: Download report as csv");
            Console.WriteLine("6: Open app (IE)");
            Console.WriteLine("8: Crear defecto");
            Console.Write("Escoger opcion: ");
            var option = Console.ReadLine();
            Console.WriteLine("Opcion escogida es: {0}", option);



            switch (option)
            {
                case "8":

                    var defectsExcel = new ExcelQueryFactory("Defects.xlsx");


                    var defects = from x in defectsExcel.Worksheet<DefectDTO>() select x;
                    foreach (var def in defects)
                    {

                        if (def.IdAuthorization != null)
                        {
                            Login();
                            log.InfoFormat("Creando Defecto: {0}", def.ToString());
                            var autPage = new AutorizationPage(driver);
                            string autPageUrl = "";
                            autPageUrl += ConfigurationManager.AppSettings["URLBase"];
                            autPageUrl += ConfigurationManager.AppSettings["WISearchURLPartialHistory"];
                            autPage.GoToURL(string.Format(autPageUrl, def.IdAuthorization));
                            string areaProyecto = autPage.AreaProyecto();
                            log.InfoFormat("Area proyecto {0}", areaProyecto);

                             string PrmUrl = "";
                            PrmUrl += ConfigurationManager.AppSettings["URLBase"];
                            PrmUrl += ConfigurationManager.AppSettings["WISearchURLPartialHistory"];
                            Console.WriteLine("Prmurl {0}",PrmUrl);
                            autPage.GoToURL(string.Format(PrmUrl, autPage.Father()));
                            

                            SolPage solPage = new SolPage(driver);
                            solPage.FillAttributes(def);
                            

                            var defPage = new DefectPage(driver);
                            string newDefUrl = "";
                            newDefUrl += ConfigurationManager.AppSettings["URLBase"];
                            newDefUrl += ConfigurationManager.AppSettings["ProjectAreaURLPartial"];
                            newDefUrl += ConfigurationManager.AppSettings["URLdefectPartial"];

                            defPage.GoToURL(string.Format(newDefUrl, areaProyecto));

                            defPage.CreateDefect(def);

                            var input=Console.ReadLine();

                            Logout();
                        }
                    }
                    break;


                case "6":
                    string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string driverFolder = Path.Combine(currentDirectory, ConfigurationManager.AppSettings["DriversDirectory"]);
                    var options = new InternetExplorerOptions
                    {
                        IgnoreZoomLevel = true,
                        ForceCreateProcessApi = true,
                        BrowserCommandLineArguments = "-private",
                        PageLoadStrategy = PageLoadStrategy.None


                    };


                    string site = ConfigurationManager.AppSettings["IEUrl"];

                    string usuario = ConfigurationManager.AppSettings["IEUser"];

                    string clave = ConfigurationManager.AppSettings["IEPassword"];


                    driver = new InternetExplorerDriver(driverFolder, options);
                    var NetAppLogin = new NetAppLogin(driver);
                    NetAppLogin.GoToURL(site);

                    Console.WriteLine("now is {0}", DateTime.Now.ToLongTimeString());

                    Console.WriteLine("about to start....");
                    Console.WriteLine("now is {0}", DateTime.Now.ToLongTimeString());
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.WinWaitActive("Windows Security");
                    AutoItX.Sleep(500);
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{TAB}");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{TAB}");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{ENTER}");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{TAB}");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{ENTER}");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Sleep(500);
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("TIM");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{ASC 092}");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send(usuario);
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{TAB}");
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Sleep(500);
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send(clave);
                    AutoItX.WinActivate("Windows Security");
                    AutoItX.Send("{ENTER}");






                    break;

                case "5":
                    Login();
                    var savedQueryPage = new SavedQueryPage(driver);
                    savedQueryPage.downloadCSV();
                    log.Info("Se descargó reporte en download");
                    Logout();


                    break;


                case "4":

                    var tasksExcel = new ExcelQueryFactory("Tasks.xlsx");


                    var timeTasks = from x in tasksExcel.Worksheet<TimeTask>() select x;
                    foreach (var task in timeTasks)
                    {

                        if (task != null && task.ToString() != null && task.IdTicket != "" && task.IdTicket != null)
                        {
                            Login();
                            log.InfoFormat("Creando Tarea: {0}", task.ToString());
                            Console.WriteLine(task.ToString());
                            Console.WriteLine("taskplanned for {0}", task.TaskPlannedFor == null);
                            int taskCode = Int32.Parse(task.IdTicket);
                            NewTaskPage taskPage = null;
                            taskPage = new NewTaskPage(driver, taskCode);

                            if (task.TimeSpentHr.Contains(","))
                            {
                                task.TimeSpentHr = task.TimeSpentHr.Replace(",", ".");
                                Console.WriteLine("new timespent {0}", task.TimeSpentHr);
                            }
                            taskPage.VisitTicketLinksTab(task);

                            Logout();
                        }
                        else
                        {
                            log.Error("No se procesan tareas nulas");
                        }
                    }

                    //report
                    Login();
                    savedQueryPage = new SavedQueryPage(driver);
                    savedQueryPage.downloadCSV();
                    log.Info("Se descargó reporte en download");
                    Logout();

                    break;

                case "3"://close task tarea
                    var taskCode1 = int.Parse(ConfigurationManager.AppSettings["TaskID"]);
                    var page1 = new TaskPage(driver, "", null);
                    page1.GoTo(taskCode1);
                    page1.CloseTask("Completada");  //Completada o Invalida o En Trabajo

                    break;




                case "2":  //crear task

                    var taskPage2 = new NewTaskPage(driver, int.Parse(ConfigurationManager.AppSettings["IdTicket"]));
                    taskPage2.VisitTicketLinksTab(null);



                    break;

                case "1":
                    try
                    {

                        Login();

                        int startRow = int.Parse(ConfigurationManager.AppSettings["startRow"]);
                        int endRow = int.Parse(ConfigurationManager.AppSettings["endRow"]);
                        int processingAssignations = endRow - startRow + 1;
                        Console.WriteLine("Skipping {0} attention(s)", startRow - 2);
                        Console.WriteLine("Processing {0} attention(s)", processingAssignations);


                        var excel = new ExcelQueryFactory("Modelo.xlsx");
                        excel.AddTransformation<AssignationExcel>(x => x.DDS, cellValue => cellValue == "Y");
                        excel.AddTransformation<AssignationExcel>(x => x.Installed, cellValue => cellValue != "N");
                        excel.AddTransformation<AssignationExcel>(x => x.SourcesCompiled, cellValue => cellValue != "N");
                        excel.AddTransformation<AssignationExcel>(x => x.Special, cellValue => cellValue == "Y");

                        var assignations = from x in excel.Worksheet<AssignationExcel>() select x;

                        var assignationList = assignations.ToList().Skip(startRow - 2).Take(processingAssignations);








                        foreach (var task in assignationList)
                        {
                            Console.WriteLine("Processing Actividad id {0} summary {1} installed {2}", task.Id, task.Summary, task.Installed);

                            //crear assignation request
                            var request = new AssignationRequest();
                            if (task.Cycle > 0) request.Cycle = task.Cycle;
                            if (task.DDS) request.DDS = task.DDS;
                            request.Id = task.Id;
                            if (!task.Installed) request.Installation = task.Installed;
                            if (task.NumberDefects > 0) request.NumberDefects = task.NumberDefects;
                            if (task.NumberPbls > 0) request.NumberPBL = task.NumberPbls;
                            if (!task.SourcesCompiled) request.SourcesCompiled = task.SourcesCompiled;
                            request.TimeSpent = task.TotalTime;
                            Console.WriteLine("Task state {0}", task.ClosingState);
                            if (task.ClosingState == 1) request.ClosingState = SeleniumCM.Models.AssignationRequest.AttentionClosingState.ExitosoConErrores;
                            if (task.ClosingState == 2) request.ClosingState = SeleniumCM.Models.AssignationRequest.AttentionClosingState.Stopper;
                            if (task.Special) request.Special = task.Special;

                            var atencion = AssignationFactory.createAssignation(request);
                            //Console.ReadLine();



                            //buscar actividad
                            var activity = task.Id;
                            var url = String.Format("https://172.19.112.112:9443/ccm/web/projects/CLARO.CERTIFICACION#action=com.ibm.team.workitem.viewWorkItem&id={0}&tab=esfuerzoscm", activity);
                            driver.Navigate().GoToUrl(url);
                            driver.Navigate().Refresh();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

                            IWebElement table = null;
                            table = driver.FindElement(By.ClassName("DetailsSplitTable"));
                            var i = 0;
                            foreach (var tarea in atencion.Tasks)
                            {
                                //editar campos  t1

                                IWebElement field = null;
                                field = table.FindElements(By.TagName("input"))[i];
                                field.Clear();
                                field.SendKeys(tarea.ReportTime.ToString());
                                i++;

                            }

                            //guardar
                            IWebElement saveBtn = null;
                            saveBtn = driver.FindElement(By.CssSelector("button.primary-button"));
                            saveBtn.Click();

                            //cambiar estado a Status
                            IWebElement statusList = null;
                            statusList = driver.FindElement(By.CssSelector("select[aria-label='Status']"));//select status
                            var selectElement = new SelectElement(statusList);
                            selectElement.SelectByText("Finalizado");

                            //cambiar estado a Resolution
                            var resolutionList = driver.FindElement(By.CssSelector("select[aria-label='Resolution']"));//select status
                            selectElement = new SelectElement(resolutionList);
                            Console.WriteLine("Closing state {0}", request.ClosingState.ToString());
                            switch (request.ClosingState)
                            {
                                case SeleniumCM.Models.AssignationRequest.AttentionClosingState.Exitoso:
                                    selectElement.SelectByText("Exitoso");
                                    break;
                                case SeleniumCM.Models.AssignationRequest.AttentionClosingState.ExitosoConErrores:
                                    selectElement.SelectByText("Exitoso con Errores");
                                    break;
                                case SeleniumCM.Models.AssignationRequest.AttentionClosingState.Stopper:
                                    selectElement.SelectByText("Stopper (No Instalado)");
                                    break;

                                default:
                                    break;
                            }


                            //guardar final
                            saveBtn.Click();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                            ScreenshotHelper.TakeScreenshot(driver, request.Id);

                        }

                    }
                    catch (Exception e)
                    {

                        Console.WriteLine("Exception Message {0}", e.Message);
                        Console.WriteLine("Exception Trace {0}", e.StackTrace);
                    }
                    break;
                default:
                    break;
            }
            //page object return
            return;






        }
    }
}
