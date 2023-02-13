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
    public class Chocolist_CreditCard
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
        public void Data_Valid_CreditCardInfo_Save_Okay()
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
            _driver.Navigate().GoToUrl("https://localhost:44343/Pages/User/Profile/?tabIndex=2");
            element = _driver.FindElement(By.XPath("//*[@id=\"txtAddDisplayName\"]"));
            element.Click();
            element.SendKeys("TEST CREDITCARD");
            element = _driver.FindElement(By.XPath("//*[@id=\"txtAddCardNumber\"]"));
            element.Click();
            element.SendKeys("4111111145551142");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_addExpireMonth\"]"));
            element.Click();
            element.SendKeys("3");
            element.SendKeys(Keys.Return);
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_addExpireYear\"]"));
            element.Click();
            element.SendKeys("30");
            element.SendKeys(Keys.Return);
            element = _driver.FindElement(By.XPath("//*[@id=\"txtAddCvv\"]"));
            element.Click();
            element.SendKeys("737");
            element = _driver.FindElement(By.XPath("//*[@id=\"btnAddNewCard\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_cardMessage\"]"));
            Assert.That(element.Text.Equals("Card registered successfully."));
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div[3]/div[4]/div/div[5]/a[2]"));
            element.Click();
            _driver.SwitchTo().Alert().Accept();
        }

        [Test]
        [Order(2)]
        public void Data_Invalid_CreditCardInfo_Save_Unsuccessful()
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
            _driver.Navigate().GoToUrl("https://localhost:44343/Pages/User/Profile/?tabIndex=2");
            element = _driver.FindElement(By.XPath("//*[@id=\"txtAddDisplayName\"]"));
            element.Click();
            element.SendKeys("TEST CREDITCARD");
            element = _driver.FindElement(By.XPath("//*[@id=\"txtAddCardNumber\"]"));
            element.Click();
            element.SendKeys("AAAAAAAAAAAAAA");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_addExpireMonth\"]"));
            element.Click();
            element.SendKeys("3");
            element.SendKeys(Keys.Return);
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_addExpireYear\"]"));
            element.Click();
            element.SendKeys("30");
            element.SendKeys(Keys.Return);
            element = _driver.FindElement(By.XPath("//*[@id=\"txtAddCvv\"]"));
            element.Click();
            element.SendKeys("737");
            element = _driver.FindElement(By.XPath("//*[@id=\"btnAddNewCard\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_RegularExpressionValidator1\"]"));
            Assert.That(element.Displayed && element.Text.Equals("Valid card number is required"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
