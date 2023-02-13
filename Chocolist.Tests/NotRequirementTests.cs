using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;

namespace Chocolist.Tests
{
    [TestFixture]
    public class Chocolist_Homepage_Navigation
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
        public void Navigate_Root_Is_Homepage()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");
            IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/a/span/span"));
            Assert.AreEqual(element.Text, "Chocolist");
        }

        [Test]
        public void Navigate_HomeIcon_Is_Homepage()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");
            IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[1]/a/i"));
            element.Click();            
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/a/span/span"));
            Assert.AreEqual(element.Text, "Chocolist");
        }

        [Test]
        public void Navigate_PersonIcon_Is_SignIn()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");
            IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
            Assert.AreEqual(element.Text, "Sign In");
        }

        [Test]
        public void Navigate_GroupIcon_Is_FriendsFamily()
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
            Assert.That(element.Text.Equals("Friends & Family"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }

    [TestFixture]
    public class Chocolist_Defaults
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
        public void Defaults_User_Is_Guest()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44343");
            IWebElement element = _driver.FindElement(By.XPath("//*[@id=\"userProfile\"]"));
            String userPopup = element.GetAttribute("data-original-title");
            Assert.AreEqual(userPopup, "Guest");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }

}