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
    public class Chocolist_Profile
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
        public void Data_Field_Update_Database_Save_Okay()
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
            
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtLastName\"]"));
            string currentLastName = element.Text;
            element.Click();
            element.Clear();
            if (currentLastName == "TestingLastName")
            {
                element.SendKeys("TestingLastNameA");
            }
            else
            {
                element.SendKeys("TestingLastName");
            }
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ButtonSave\"]"));
            element.Click();
            element.SendKeys(Keys.Return);
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(_driver => _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgProfile\"]")).Text.Equals("Profile updated successfully"));
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgProfile\"]"));
            Assert.That(element.Text.Equals("Profile updated successfully"));
        }

        [Test]
        [Order(2)]
        public void Data_Invalid_BirthDate_Returns_Error()
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

            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_txtBirthDate\"]"));
            element.Click();
            element.SendKeys("2023-10-01");
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ButtonSave\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_RangeValidator1\"]"));
            Assert.That(element.Text.Equals("Birth Date can not be in the future"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
