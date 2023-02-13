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
    public class Chocolist_Preferences
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
        public void Data_Field_Update_Platform_Save_Okay()
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

            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ddlPlatform\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div[3]/div[4]/div[1]/label/select/option[4]"));
            element.Click();
            int clickAttempt = 0;

            while (clickAttempt < 2)
            {
                try
                {
                    element =_driver.FindElement(By.XPath("//*[@id=\"MainContent_ButtonSave\"]"));
                    element.Click();
                    element.SendKeys(Keys.Return);
                    break;
                }
                catch (StaleElementReferenceException e)
                { }
                clickAttempt++;
            }
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(_driver => _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgProfile\"]")).Text.Equals("Profile updated successfully"));
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgProfile\"]"));
            Assert.That(element.Text.Equals("Profile updated successfully"));
        }

        [Test]
        [Order(2)]
        public void Data_Update_Genre_Save_Okay()
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

            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ddlGenre\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div[3]/div[4]/div[2]/label/select/option[4]"));
            element.Click();
            int clickAttempt = 0;

            while (clickAttempt < 2)
            {
                try
                {
                    element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_ButtonSave\"]"));
                    element.Click();
                    element.SendKeys(Keys.Return);
                    break;
                }
                catch (StaleElementReferenceException e)
                { }
                clickAttempt++;
            }
            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(_driver => _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgProfile\"]")).Text.Equals("Profile updated successfully"));
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_msgProfile\"]"));
            Assert.That(element.Text.Equals("Profile updated successfully"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
