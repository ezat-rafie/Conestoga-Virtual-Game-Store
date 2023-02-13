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
    public class Chocolist_SelectingGames
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
        public void Data_Clicking_Game_Shows_Details()
        {
            bool isValid = true;
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");

            String gameTitle = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[3]/div[4]/div/div/div[2]/div/div/h5")).Text;
            IWebElement element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptGame_HyperLink2_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_lblTitle\"]"));
            Assert.That(element.Text.Equals(gameTitle));
        }

        [Test]
        [Order(2)]
        public void Data_HomePage_Shows_List_Of_Games()
        {
            bool isValid = true;
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");

            try
            {
                IWebElement element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptGame_HyperLink2_0\"]"));
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
