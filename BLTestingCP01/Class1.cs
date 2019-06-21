using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;


namespace BLTestingCP01
{
    public class Class1
    {

        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("D:\\chromedriver_win32");
        }

        [Test]
        public void test()
        {
            driver.Url = "http://192.168.100.51/plc/login.php";
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }



    }
}
