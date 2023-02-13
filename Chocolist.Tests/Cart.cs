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
    public class Chocolist_Cart
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
        public void Data_Item_Adds_To_Cart_Success()
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
            // Get the Top Left Game card
            String gameTitle = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[3]/div[4]/div/div/div[2]/div/div/h5")).Text;
            String gamePrice = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[3]/div[4]/div/div/div[2]/div/div/span[2]")).Text;
            Console.WriteLine(gameTitle);
            Console.WriteLine(gamePrice);
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptGame_btnAddToCart_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"btnCart\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/table/tbody/tr[2]/td[1]"));
            isValid = gameTitle.Equals(element.Text) ? isValid : false;
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/table/tbody/tr[2]/td[4]"));
            isValid = gamePrice.Equals(element.Text) ? isValid : false;
            Assert.That(isValid == true);
        }

        [Test]
        [Order(2)]
        public void Data_Item_Adds_2_To_Cart_Success()
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
            // Get the Top Left Game card
            String gameTitle = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[3]/div[4]/div/div/div[2]/div/div/h5")).Text;
            String gamePrice = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[3]/div[4]/div/div/div[2]/div/div/span[2]")).Text;
            Console.WriteLine(gameTitle);
            Console.WriteLine(gamePrice);
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptGame_btnAddToCart_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"btnCart\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/table/tbody/tr[2]/td[1]"));
            isValid = gameTitle.Equals(element.Text) ? isValid : false;
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/table/tbody/tr[2]/td[4]"));
            isValid = element.Text.Equals(String.Format("{0:c}", double.Parse(gamePrice.Substring(1)) * 2)) ? isValid : false;
            Assert.That(isValid == true);
        }

        [Test]
        [Order(3)]
        public void Data_Item_Remove_Removes_From_Cart_Success()
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
            element = _driver.FindElement(By.XPath("//*[@id=\"btnCart\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_CartList_btnRemove_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div[2]/table/tbody/tr/td"));
            Assert.That(element.Text.Equals("There is nothing in your cart."));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
