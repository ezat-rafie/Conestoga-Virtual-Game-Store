using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;

namespace Chocolist.Tests
{
    class JoiningLoggingIn
    {
        [TestFixture]
        public class Chocolist_SignIn
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
            public void Navigation_SignIn_Is_Default()
            {
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                Assert.AreEqual(element.Text, "Sign In");
            }

            [Test]
            public void Validation_SignIn_No_Username__Password_Returns_Error()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
                element.Click();
                try
                {
                    isValid = _driver.FindElement(By.XPath("//*[@id=\"MainContent_RequiredEmail\"]")).Displayed ? isValid : false; ;
                }
                catch (Exception e)
                {
                }
                try
                {
                    isValid = _driver.FindElement(By.XPath("//*[@id=\"MainContent_RequiredPassword\"]")).Displayed ? isValid : false; ;
                }
                catch (Exception e)
                {
                }
                Assert.That(isValid == true);
            }

            [Test]
            public void Validation_SignIn_Bad_Password_Returns_Error()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"emailLogin\"]"));
                element.Click();
                element.SendKeys("testinguser@gmail.com");
                element = _driver.FindElement(By.XPath("//*[@id=\"passwordLogin\"]"));
                element.Click();
                element.SendKeys("testinguser@gmail.com");
                element = _driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
                element.Click();
                try
                {
                    isValid = _driver.FindElement(By.XPath("//*[@id=\"lblLoginError\"]")).Displayed ? isValid : false; ;
                }
                catch (Exception e)
                {
                }
                Assert.That(isValid == true);
            }

            [Test]
            public void Validation_SignIn_Bad_Username_Returns_Error()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"emailLogin\"]"));
                element.SendKeys("testinguserNOTAREALUSER@gmail.com");
                element = _driver.FindElement(By.XPath("//*[@id=\"passwordLogin\"]"));
                element.Click();
                element.SendKeys("TestingPassword1!");
                element = _driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
                element.Click();
                try
                {
                    isValid = _driver.FindElement(By.XPath("//*[@id=\"lblLoginError\"]")).Displayed ? isValid : false; ;
                }
                catch (Exception e)
                {
                }
                Assert.That(isValid == true);
            }

            [Test]
            public void Validation_Valid_SignIn_Goes_To_Homepage()
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
                try
                {
                    isValid = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[3]/a/i")).Displayed ? isValid : false; ;
                }
                catch (Exception e)
                {
                }
                Assert.That(isValid == true);
            }

            [TearDown]
            public void CloseBrowser()
            {
                _driver.Quit();
            }
        }

        [TestFixture]
        public class Chocolist_Register
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
            public void Navigation_Register_After_Signin()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"createAccount\"]"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Register") ? isValid : false;
                Assert.IsTrue(isValid);
            }

            [Test]
            public void Validation_Empty_Fields_Returns_Error()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"createAccount\"]"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Register") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"btnRegister\"]"));
                element.Click();
                try
                {
                    isValid = _driver.FindElement(By.XPath("//*[@id=\"MainContent_reqEmail\"]")).Displayed ? isValid : false; ;
                }
                catch (Exception e)
                {
                }
                try
                {
                    isValid = _driver.FindElement(By.XPath("//*[@id=\"MainContent_reqPassword\"]")).Displayed ? isValid : false; ;
                }
                catch (Exception e)
                {
                }
                Assert.IsTrue(isValid);
            }

            [Test]
            public void Validation_Bad_Email_Address_Returns_error()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"createAccount\"]"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Register") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"emailRegister\"]"));
                element.SendKeys("TESTING");
                element = _driver.FindElement(By.XPath("//*[@id=\"btnRegister\"]"));
                element.Click();
                element = _driver.FindElement(By.XPath("//*[@id=\"emailRegister\"]"));
                String message = "";
                try
                {
                    message = element.GetAttribute("validationMessage");
                }
                catch (Exception e)
                {
                    isValid = false;
                }
                isValid = message.Equals("Please enter an email address.") ? isValid : false;
                Assert.That(isValid == true);
            }

            [Test]
            public void Validation_Bad_Password_Returns_error()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"createAccount\"]"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Register") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"passwordRegister\"]"));
                element.SendKeys("TESTING");
                element = _driver.FindElement(By.XPath("//*[@id=\"btnRegister\"]"));
                element.Click();
                try
                {
                    isValid = _driver.FindElement(By.XPath("//*[@id=\"MainContent_regPassword\"]")).Displayed ? isValid : false;
                }
                catch (Exception e)
                {
                }
                Assert.That(isValid == true);
            }

            [Test]
            public void Validation_No_Captcha_Returns_error()
            {
                bool isValid = true;
                _driver.Navigate()
                    .GoToUrl("https://localhost:44343");
                IWebElement element = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/a/i"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Sign In") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"createAccount\"]"));
                element.Click();
                element = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[1]/h1"));
                isValid = element.Text.Equals("Register") ? isValid : false;
                element = _driver.FindElement(By.XPath("//*[@id=\"emailRegister\"]"));
                element.SendKeys("testing@testing.com");
                element = _driver.FindElement(By.XPath("//*[@id=\"passwordRegister\"]"));
                element.SendKeys("Testing1!");
                element = _driver.FindElement(By.XPath("//*[@id=\"btnRegister\"]"));
                element.Click();
                try
                {
                    isValid = _driver.FindElement(By.XPath("/html/body/form/div[4]/div/div/div[5]/div/span/ul/li")).Text.Equals("Captcha is required") ? isValid : false;
                }
                catch (Exception e)
                {
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
}
