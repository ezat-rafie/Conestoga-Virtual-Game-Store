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
    public class Chocolist_Download
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
        public void Data_Download_Button_Exists_For_Games()
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

            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[6]/a"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/table/tbody/tr[2]/td[2]/ul/li"));
            Assert.That(element.Text.Contains("ID:15 - Final Fantasy x 1"));
            try
            {
                element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_OrderList_btnShip_0\"]"));
            }
            catch (Exception e)
            {
                isValid = false;
            }
            Assert.That(isValid = true);
        }

        [Test]
        [Order(2)]
        public void Data_Merch_Do_Not_Show_On_Downloads()
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

            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[6]/a"));
            element.Click();
            try
            {
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/table/tbody/tr[3]/td[1]"));
                if (element.Text.Equals("21"))
                {
                    isValid = false;
                }
            }
            catch (Exception e)
            {
                // No concerns
            }
            Assert.That(isValid == true);
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
