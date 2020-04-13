using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;

namespace NUnitTestProject1
{
    public class Tests
    {
        private const string homeUrl = "https://www.kika.lt/";
        private const string loginName = "s.dubinskas@gmail.com";
        private const string password = "test123";
        private const string searchParam = "maistas";
        private const string mainTitle = "KIKA – žinomiausias gyvūnų prekių parduotuvių tinklas Lietuvoje";

        private int tempInt = 0;
        private IWebElement webElement;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [Category("Smoke")]
        public void LoginTest()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--start-maximized");
            using IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl(homeUrl);

            Assert.AreEqual(mainTitle, driver.Title);

            DriverFunctions.DoClickWhile(driver, $"#editable_popup[style*='display: block'] .close");
            DriverFunctions.DoClickWhile(driver, $".need2login");

            DriverFunctions.SetWebElementByCssSelector(driver, $".modal .form-control[name='email']", ref webElement);
            Assert.NotNull(webElement);
            webElement.SendKeys(loginName);

            Thread.Sleep(100);
            DriverFunctions.SetWebElementByCssSelector(driver, $".modal .form-control[name='password']", ref webElement);
            Assert.NotNull(webElement);
            webElement.SendKeys(password);

            DriverFunctions.DoClickWhile(driver, $".modal .btn.btn-primary");
            DriverFunctions.DoClickWhile(driver, $"#editable_popup[style*='display: block'] .close");
            DriverFunctions.DoClickWhile(driver, $"#profile_menu .title");
            DriverFunctions.DoClickWhile(driver, $"[href='?logout']");

            Assert.Pass();
        }

        [Test]
        public void ProductSearchAddAndRemove()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--start-maximized");
            using IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl(homeUrl);

            DriverFunctions.DoClickWhile(driver, $"#editable_popup[style*='display: block'] .close");

            DriverFunctions.DoClickWhile(driver, $".need2login");

            DriverFunctions.SetWebElementByCssSelector(driver, $".modal .form-control[name='email']", ref webElement);
            Assert.NotNull(webElement);
            webElement.SendKeys(loginName);

            Thread.Sleep(100);
            DriverFunctions.SetWebElementByCssSelector(driver, $".modal .form-control[name='password']", ref webElement);
            Assert.NotNull(webElement);
            webElement.SendKeys(password);

            DriverFunctions.DoClickWhile(driver, $".modal .btn.btn-primary");
            DriverFunctions.DoClickWhile(driver, $"#editable_popup[style*='display: block'] .close");

            DriverFunctions.DoClickWhile(driver, $"#quick_search_show");
            DriverFunctions.SetWebElementByCssSelector(driver, $".form-control[name='search']", ref webElement);
            Assert.NotNull(webElement);
            webElement.SendKeys(searchParam);
            webElement.SendKeys(Keys.Return);

            DriverFunctions.DoClickWhile(driver, $"#logo a");
            DriverFunctions.DoClickWhile(driver, $"#editable_popup[style*='display: block'] .close");

            DriverFunctions.DoClickWhile(driver, $"#mega_menu .dog a");
            DriverFunctions.DoClickWhile(driver, $".product_listing .product_element .btn.btn-primary");

            DriverFunctions.DoClickWhile(driver, $"#cart_info a");

            tempInt = DriverFunctions.CountOfElements(driver, "#cart_items .item");
            Assert.Greater(tempInt, 0, "Incorrect basket items count. Expected at least one item");

            DriverFunctions.DoClickWhile(driver, $"#cart_items .remove a");

            DriverFunctions.DoClickWhile(driver, $"#profile_menu .title");
            DriverFunctions.DoClickWhile(driver, $"[href='?logout']");

            Thread.Sleep(5000);

            Assert.Pass();
        }

        [TearDown]
        public void End()
        {

        }
    }
}