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
    public class Chocolist_Friends_and_Family
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
        public void Data_Blank_Search_Bar_Click_Button_Returns_Value()
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
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[3]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[1]/div/h3"));
            isValid = element.Text.Equals("Friends & Family") ? isValid : false;
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnSearch\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/div/div[2]/div/div[1]"));
            Assert.That(element.GetAttribute("class").Equals("card p-1 mb-2 friend-card"));
        }

        [Test]
        [Order(2)]
        public void Data_Clicking_Request_Puts_Request_In_Sent_Requests()
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
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[3]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[1]/div/h3"));
            isValid = element.Text.Equals("Friends & Family") ? isValid : false;
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnSearch\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptResult_btnRequest_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/div/div[1]/div[3]"));
            Assert.That(element.GetAttribute("class").Equals("card p-1 mb-2 sent-request-card"));
        }

        [Test]
        [Order(3)]
        public void Data_Clicking_Request_Cancel_Displays_Confirmation()
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
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[3]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[1]/div/h3"));
            isValid = element.Text.Equals("Friends & Family") ? isValid : false;
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptSentRequest_btnCancel_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_sentRequestMsg\"]"));
            Assert.That(element.Text.Equals("Request Canceled."));
        }
        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
