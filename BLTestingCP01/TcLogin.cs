using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;

namespace BLTestingCP01
{
    class TcLogin
    {
        
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("D:\\chromedriver_win32");
        }

        [Test]
        public void LoginAsAdmin()
        {
            driver = new ChromeDriver("D:\\chromedriver_win32");
            driver.Url = TestCPConst.SetURL +"/login.php";           
            Thread.Sleep(7000);

            // Find element using ID
            IWebElement uname = driver.FindElement(By.Id("username"));
            IWebElement pwd = driver.FindElement(By.Id("password"));
            IWebElement btnlgin = driver.FindElement(By.Id("login"));

            uname.SendKeys("admin");
            pwd.SendKeys("password");

            btnlgin.Click();
            Thread.Sleep(5000);

            // Oen HVAC dashboard                         
            driver.FindElement(By.XPath("//div[@id='box']/a/img")).Click();

            Thread.Sleep(8000);

        }



        [TearDown]
        public void closeBrowser()
        {
           driver.Close();
        }

        
    }
}
