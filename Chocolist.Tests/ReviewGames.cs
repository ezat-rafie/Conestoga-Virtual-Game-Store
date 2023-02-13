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
    public class Chocolist_ReviewGames
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
        public void Data_Reviews_Show_On_Game_Detail()
        {
            bool isValid = true;
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");

            IWebElement element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptGame_HyperLink2_0\"]"));
            element.Click();
            try
            {
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div[2]/div/div[2]/div[1]/div/div/div/div/div[3]/div[1]/div/span[2]"));
            }
            catch (Exception e)
            {
                isValid = false;
            }
            Assert.That(isValid = true);
        }

        [Test]
        [Order(2)]
        public void Data_Review_Can_Be_Entered()
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
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptGame_HyperLink2_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnAddReview\"]"));
            element.Click();
            try
            {
                element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_btnReviewItem\"]"));
            }
            catch (Exception e)
            {
                isValid = false;
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
