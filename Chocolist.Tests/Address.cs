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
    public class Chocolist_Address
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
        public void Data_Valid_Address_Save_Okay()
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
            element = _driver.FindElement(By.XPath("//*[@id=\"userProfile\"]"));
            element.Click();
            _driver.Navigate().GoToUrl("https://localhost:44343/Pages/User/Profile/?tabIndex=1");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddAddressName\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("TESTINGFULLNAME");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddAddress1\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("Test Address Line 1");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddCity\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("TestCity");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddPostal\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("H0H0H0");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnSaveAddress\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgAddress\"]"));
            Assert.That(element.Text.Equals("Address added successfully"));
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div[3]/div[2]/div[2]/div[6]/a[2]"));
            element.Click();
            _driver.SwitchTo().Alert().Accept();
        }

        [Test]
        [Order(2)]
        public void Data_Invalid_Address_Save_Unsuccessful()
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
            element = _driver.FindElement(By.XPath("//*[@id=\"userProfile\"]"));
            element.Click();
            _driver.Navigate().GoToUrl("https://localhost:44343/Pages/User/Profile/?tabIndex=1");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddAddressName\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("TESTINGFULLNAME");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddAddress1\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("Test Address Line 1");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddCity\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("TestCity");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtAddPostal\"]"));
            element.Click();
            element.Clear();
            element.SendKeys("111111");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnSaveAddress\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgAddress\"]"));
            Assert.That(element.Text.Equals("Valid postal code is required"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
