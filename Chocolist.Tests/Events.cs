using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chocolist.Tests
{
    [TestFixture]
    public class Chocolist_Events
    {
        private IWebDriver _driver;
        [SetUp]
        public void Setup()
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AcceptInsecureCertificates = true;
            _driver = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox\\firefox.exe", firefoxOptions);
        }

        [Test]
        [Order(1)]
        public void Data_Clicking_RSVP_Registers_For_Event()
        {
            bool isValid = true;
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");

            IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
            isValid = element.Text.Equals("Sign In") ? isValid : false;
            element = _driver.FindElement(By.XPath("//*[@id=\"emailLogin\"]"));
            element.SendKeys("testinguser@gmail.com");
            element = _driver.FindElement(By.XPath("//*[@id=\"passwordLogin\"]"));
            element.Click();
            element.SendKeys("TestingPassword1!");
            element = _driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
            element.Click();
            _driver.Navigate()
                .GoToUrl("https://localhost:44343/?tabIndex=2");

            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptEvent_hLinkEvent2_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnRSVP\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ResultMessage\"]"));
            Assert.That(element.Text.Equals("Event Registered"));
        }

        [Test]
        [Order(2)]
        public void Data_When_Registered_Firstname_Shows_On_Event()
        {
            bool isValid = true;
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");
            IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
            isValid = element.Text.Equals("Sign In") ? isValid : false;
            element = _driver.FindElement(By.XPath("//*[@id=\"emailLogin\"]"));
            element.SendKeys("testinguser@gmail.com");
            element = _driver.FindElement(By.XPath("//*[@id=\"passwordLogin\"]"));
            element.Click();
            element.SendKeys("TestingPassword1!");
            element = _driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
            element.Click();
            _driver.Navigate()
                .GoToUrl("https://localhost:44343/?tabIndex=2");

            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptEvent_hLinkEvent2_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAttendance\"]"));
            Assert.That(element.Text.Contains("TestingFirst"));
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnCancelRSVP\"]"));//*[@id="MainContent_txtAttendance"]
            element.Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
