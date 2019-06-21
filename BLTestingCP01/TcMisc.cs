using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Threading;


namespace BLTestingCP01
{
    
    public class TcMisc
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("D:\\chromedriver_win32");
        }

        #region SetPoint
        /// <summary>
        /// Cooling Test Turn On Power Room AC1
        /// </summary>
        [Test]
        public double[] loadSetPoints()
        {
            double[] darray = new double[6];
            // go to setpoints page
            driver.Url = TestCPConst.SetURL + "systemcontrol.php";
            Thread.Sleep(6000); // 6 sec delay
                                //driver.SwitchTo().Frame(0);

            //ac
            IWebElement elmt = driver.FindElement(By.Id("fcb1_hvac_start_temp"));
            darray[0] = Convert.ToDouble(elmt.GetAttribute("value"));
            elmt = driver.FindElement(By.Id("fcb1_hvac_stop_temp"));
            darray[1] = Convert.ToDouble(elmt.GetAttribute("value"));
            // heater
            elmt = driver.FindElement(By.Id("fcb1_heater_start_temp"));
            darray[2] = Convert.ToDouble(elmt.GetAttribute("value"));
            elmt = driver.FindElement(By.Id("fcb1_heater_stop_temp"));
            darray[3] = Convert.ToDouble(elmt.GetAttribute("value"));
            // damper
            elmt = driver.FindElement(By.Id("fcb1_damper_start_temp"));
            darray[4] = Convert.ToDouble(elmt.GetAttribute("value")); ;
            elmt = driver.FindElement(By.Id("fcb1_damper_stop_temp"));
            darray[5] = Convert.ToDouble(elmt.GetAttribute("value"));
            return darray;
        }


        [Test]
        public string ReadLeadLagValue()
        {
            driver.Url = TestCPConst.SetURL + "/maindashboard.php";
            Thread.Sleep(7000);
            string currentlead = "";
            // click Frame important point
            driver.SwitchTo().Frame(0);
            // Get current lead and lag
            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            IWebElement iwAC1 = driver.FindElement(By.XPath("//*[@id='hvac2_compressor']"));

            return iwAC1.Text;
        }
        #endregion




        [TearDown]
        public void closeBrowser()
        {
            driver.Quit();
        }



    }
}
