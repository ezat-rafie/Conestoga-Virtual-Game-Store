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
    public class Chocolist_ViewWishList
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
        public void Data_Can_See_Friends_WishList()
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
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[3]/a"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptFriend_btnViewWishList_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_wishListTitle\"]"));
            Assert.That(element.Text.Equals("Trevor's Wish List"));
        }

        [Test]
        [Order(2)]
        public void Data_WishList_Click_Goes_To_Item()
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
            element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[3]/a"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptFriend_btnViewWishList_0\"]"));
            element.Click();
            element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_rptWishListGame_HyperLink2_0\"]"));
            element.Click();
            try
            {
                element = _driver.FindElement(By.XPath("//*[@id=\"MainContent_lblTitle\"]"));
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
