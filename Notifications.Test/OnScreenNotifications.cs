using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Notifications.Test
{
    [TestClass]
    public class OnScreenNotifications
    {
        private IWebDriver _driver;
        
        [TestMethod]
        public void GrowlTest()
        {
            _driver.Navigate().GoToUrl("https://www.google.com");
            var JSDriver = (IJavaScriptExecutor)_driver;

            var script_jquery = File.ReadAllText("scripts/Growl/jquery.js");
            JSDriver.ExecuteScript(script_jquery);

            var script_growl = File.ReadAllText("scripts/Growl/growl.js");
            JSDriver.ExecuteScript(script_growl);

            var script_growlcss = File.ReadAllText("scripts/Growl/growl.css");
            JSDriver.ExecuteScript("$('head').append(\"<style type='text/css'> " +
                                   script_growlcss+
                                    "</style>\");");


            // trigger a plain jQuery Growl notification to display on the page
            JSDriver.ExecuteScript("$.growl({ title: 'GET', message: '/' });");
            //wait 1sec to render the message.... Just for debug
            Thread.Sleep(1000);

            JSDriver.ExecuteScript("$.growl.error({ title: 'ERROR', message: 'your message goes here' });");
            Thread.Sleep(1000);

            JSDriver.ExecuteScript("$.growl.notice({ title: 'Notice', message: 'your notice message goes here' });");
            Thread.Sleep(1000);

            JSDriver.ExecuteScript("$.growl.warning({ title: 'Warning!', message: 'your warning message goes here' });");
            
            //Pause at the end to show the messages
            Thread.Sleep(10000);
        }


        [TestInitialize]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30); ;
            //_driver.Navigate().GoToUrl(Url);
        }
        [TestCleanup]
        public void teardown()
        {
            _driver.Quit();
        }
    }
}
