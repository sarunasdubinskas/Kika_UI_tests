using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject1
{
    public class DriverFunctions
    {
        public static void DoClickWhile(IWebDriver driver, string selector)
        {
            int iterationCount = 2;
            do
            {
                try
                {
                    driver.FindElement(By.CssSelector(selector)).Click();
                    iterationCount = 0;
                }
                catch
                {
                    iterationCount--;
                }
            } while (iterationCount > 0);
        }

        public static int CountOfElements(IWebDriver driver, string selector)
        {
            int count = -1;
            int iterationCount = 2;
            do
            {
                try
                {
                    count = driver.FindElements(By.CssSelector(selector)).Count;
                    iterationCount = 0;
                }
                catch
                {
                    iterationCount--;
                }
            } while (iterationCount > 0);
            return count;
        }
        public static void SetWebElementByCssSelector(IWebDriver driver, string selector, ref IWebElement webElement)
        {
            webElement = null;
            int iterationCount = 2;
            do
            {
                try
                {
                    webElement = driver.FindElement(By.CssSelector(selector));
                    iterationCount = 0;
                }
                catch
                {
                    iterationCount--;
                }
            } while (iterationCount > 0);
        }
    }
}
