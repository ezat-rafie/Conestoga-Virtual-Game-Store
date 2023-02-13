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
    public class Chocolist_Reports
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
        public void Data_Report_Dropdown_Populates()
        {
            bool isValid = true;
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");

            IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
            isValid = element.Text.Equals("Sign In") ? isValid : false;
            element = _driver.FindElement(By.XPath("//*[@id=\"emailLogin\"]"));
            element.SendKeys("testemp@test.ca");
            element = _driver.FindElement(By.XPath("//*[@id=\"passwordLogin\"]"));
            element.Click();
            element.SendKeys("Choco123!");
            element = _driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[6]/a"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ddlSelectReport\"]"));
            element.Click();
            Assert.That(element.FindElements(By.TagName("option")).Count>0);
        }

        [Test]
        [Order(2)]
        public void Data_Running_Reports_Results_In_Data_Population()
        {
            bool isValid = true;
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");

            IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
            isValid = element.Text.Equals("Sign In") ? isValid : false;
            element = _driver.FindElement(By.XPath("//*[@id=\"emailLogin\"]"));
            element.SendKeys("testemp@test.ca");
            element = _driver.FindElement(By.XPath("//*[@id=\"passwordLogin\"]"));
            element.Click();
            element.SendKeys("Choco123!");
            element = _driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[6]/a"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ddlSelectReport\"]"));
            element.Click();
            element.SendKeys("f");
            element.SendKeys(Keys.Return);
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnRunReport\"]"));
            element.Click();
            var timeout = 10000; // in milliseconds
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ResultMessage\"]"));
            Assert.That(element.Text.Equals(""));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
