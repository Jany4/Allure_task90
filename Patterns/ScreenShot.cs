
using OpenQA.Selenium;

using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace Patterns
{
    public class ScreenShot
    {
        public static byte[] Take(IWebDriver driver)
        {
            return ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
        }
    }
}
