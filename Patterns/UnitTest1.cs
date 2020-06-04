using NUnit.Framework;
using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using SeleniumExtras.PageObjects;
using Allure.NUnit.Attributes;
using Allure.Commons;

namespace Patterns
{
    

    public class Tests : AllureReport
    {
        private static IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        
        [TearDown]
        public void CleanUp()
        {
            string result = TestContext.CurrentContext.Result.Outcome.Status.ToString();
            
            if (TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Failed")
            {
                AllureLifecycle.Instance.AddAttachment("Fail screenshot", AllureLifecycle.AttachFormat.ImagePng, ScreenShot.Take(_driver));
                //AllureLifecycle.Instance.AddAttachment("Fail Screenshot", AllureLifecycle.AttachFormat.ImagePng, ((ITakesScreenshot)_driver).GetScreenshot().AsByteArray);
            }
                
            _driver.Close();
        }

        [AllureTest("Testing Login functionality")]
        [AllureLink("https//:test.com/issue1", false)]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureSubSuite("Login")]
        [AllureOwner("Vorobyov")]
        [TestCase("seleniumtests2", "123456789zxcvbn1")]//wrong test data to get error in the report
        [Test]
        public void LogInTest(string logIn, string password)
        {
            TutByLoginPage tutByLoginPage = new TutByLoginPage(_driver);
            Assert.IsTrue(tutByLoginPage.IsLoaded);

            Assert.IsTrue(tutByLoginPage.LogIn(logIn, password).IsLoaded);
        }

        [AllureTest("Testing Logout functionality")]
        [AllureLink("issue2")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Normal)]
        [AllureSubSuite("Logout")]
        [AllureOwner("Vorobyov")]
        [TestCase("seleniumtests2", "123456789zxcvbn")]
        [Test]
        public void LogOutTest(string logIn, string password)
        {
            TutByLoginPage tutByLoginPage = new TutByLoginPage(_driver);
            Assert.IsTrue(tutByLoginPage.IsLoaded);

            TutByEmailPage tutByEmailPage = tutByLoginPage.LogIn(logIn, password);
            Assert.IsTrue(tutByEmailPage.IsLoaded);
            Assert.IsTrue(tutByEmailPage.LogOut().IsLoaded);

        }
    }
}