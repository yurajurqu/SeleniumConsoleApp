using OpenQA.Selenium;
using SeleniumCM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SeleniumCM.Helpers
{
    public  class ScreenshotHelper
    {

        public static void TakeScreenshot(IWebDriver driver,int id)
        {
            ITakesScreenshot  screenshotDriver  = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();


            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string screenshotDirectory = Path.Combine(currentDirectory, ConfigurationManager.AppSettings["ScreenshotsDirectory"]);
            string photoName = string.Format("Ticket_{0}.png", id);
            string screenshotPath = Path.Combine(screenshotDirectory, photoName);


            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
        }
        public static void TakeScreenshotTimeTask(IWebDriver driver, string newTaskId)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();


            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string screenshotDirectory = Path.Combine(currentDirectory, ConfigurationManager.AppSettings["ScreenshotsDirectory"]);


            //todo fix directory creation
            if (!Directory.Exists(screenshotDirectory))
               Directory.CreateDirectory(screenshotDirectory);

            string photoName = string.Format("TimeTask_{0}.png", newTaskId);
            string screenshotPath = Path.Combine(screenshotDirectory, photoName);


            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
        }

        public static void TakeScreenshotReport(IWebDriver driver)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();


            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string screenshotDirectory = Path.Combine(currentDirectory, ConfigurationManager.AppSettings["ScreenshotsDirectory"]);


            //todo fix directory creation
            if (!Directory.Exists(screenshotDirectory))
                Directory.CreateDirectory(screenshotDirectory);

            string photoName = string.Format("HoursReport_{0}.png", DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss"));
            string screenshotPath = Path.Combine(screenshotDirectory, photoName);


            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
        }




    }
}
