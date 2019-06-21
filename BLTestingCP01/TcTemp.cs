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
    // Temperature class
    public class TcTemp
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
            driver.Url = TestCPConst.SetURL + "/login.php";
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
            //driver.FindElement(By.XPath("//div[@id='box']/a/img")).Click();

            //Thread.Sleep(8000);

        }

        [Test]
        public string IncreaseTemperature(List<TcHVAC> hvacs, double maxmintemp)
        {
            try
            {
                bool result = false;
                string remarks = "";
                string currentlead = "";
                double CurrentTemp = 0;
                //Get Current Temperature
                CurrentTemp = GetCurrentPhysicalQuantity(1); // 1 means Temperature
                hvacs = GetValuesofHVACs(hvacs);            // Get HVACs statuses
                                                            // Go to Configuration page
                Thread.Sleep(4000);
                while (CurrentTemp < maxmintemp)
                {
                    ChangeCallibiration(1, true, CurrentTemp, maxmintemp);    // 1 = Temperature, true = Increase                   // increase/ decrease callibn value depending on requirement
                    CurrentTemp = GetCurrentPhysicalQuantity(1); // 1 means Temperature
                    hvacs = GetValuesofHVACs(hvacs);            // Get HVACs statuses
                }

                return "Test Complete";
            }
            catch (Exception)
            {

                throw;
            }


        }

        public string DecreaseTemperature(List<TcHVAC> hvacs, double maxmintemp)
        {
            try
            {
                bool result = false;
                string remarks = "";
                string currentlead = "";
                double CurrentTemp = 0;
                //Get Current Temperature
                CurrentTemp = GetCurrentPhysicalQuantity(1); // 1 means Temperature
                hvacs = GetValuesofHVACs(hvacs);            // Get HVACs statuses
                                                            // Go to Configuration page
                Thread.Sleep(4000);
                while (CurrentTemp > maxmintemp)
                {
                    ChangeCallibiration(1, false, CurrentTemp, maxmintemp);    // 1 = Temperature, false = decrease                   // increase/ decrease callibn value depending on requirement
                    CurrentTemp = GetCurrentPhysicalQuantity(1); // 1 means Temperature
                    hvacs = GetValuesofHVACs(hvacs);            // Get HVACs statuses
                }

                return "Test Complete";
            }
            catch (Exception)
            {

                throw;
            }


        }

        private double GetCurrentPhysicalQuantity(int value)  // Temperature, Humidty, Differenetial Pressure
        {
            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            Thread.Sleep(10000); // 15 secs -- 30 secs
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            // get Current Temperature value again 
            driver.SwitchTo().Frame(0);
            double CurrentVal = 0;
            switch (value)
            {
                case 1:
                    {
                        // get Current Temperature value
                        IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
                        if (iwCT.Text.Length == 4)
                        {
                            CurrentVal = Convert.ToDouble(iwCT.Text.Substring(0, 2));
                        }
                        else if (iwCT.Text.Length == 5)
                        {
                            CurrentVal = Convert.ToDouble(iwCT.Text.Substring(0, 3));
                        }
                        else if (iwCT.Text.Length == 6)
                        {
                            CurrentVal = Convert.ToDouble(iwCT.Text.Substring(0, 4));
                        }
                    }
                    break;
                case 2:
                    {
                        // get Current Himidity level
                        IWebElement iwRH = driver.FindElement(By.XPath("//span[@id='unit1_humidity']")); //unit1_humidity 
                        if (iwRH.Text.Length == 3)
                        {
                            CurrentVal = Convert.ToDouble(iwRH.Text.Substring(0, 2));
                        }
                    }
                    break;
                case 3:
                    {
                        // get Current differential pressure
                        IWebElement iwDP = driver.FindElement(By.Id("unit1_dp"));
                        if (iwDP.Text.Length == 3)
                        {
                            CurrentVal = Convert.ToDouble(iwDP.Text.Substring(0, 1));
                        }
                        else if (iwDP.Text.Length == 4)
                        {
                            CurrentVal = Convert.ToDouble(iwDP.Text.Substring(0, 2));
                        }
                    }
                    break;
            }
            
     

            return CurrentVal;
        }

        private List<TcHVAC> GetValuesofHVACs(List<TcHVAC> hvacs)
        {
            // Get before temperature change Settings of Room
            IWebElement iwHvacseq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwAC = driver.FindElement(By.XPath("//*[@id='hvac1_compressor']"));
            //IWebElement iwHeat = 
            int i = 1;
            foreach (TcHVAC hvac in hvacs)
            {
                // get lead/lag                
                iwHvacseq = driver.FindElement(By.Id("hvac" + i + "_sequence"));
                hvac.LEADLAG = (iwHvacseq.Text == "Lead") ? true : false;

                // get fan value
                iwAC = driver.FindElement(By.XPath("//*[@id='hvac" + i.ToString() + "_fan']"));
                hvac.FAN = (iwAC.Text == "ON") ? true : false;

                // get compressor value
                iwAC = driver.FindElement(By.XPath("//*[@id='hvac" + i.ToString() + "_compressor']"));
                hvac.COMPRESSOR = (iwAC.Text == "ON") ? true : false;

                // get heater value
                iwAC = driver.FindElement(By.XPath("//*[@id='hvac" + i.ToString() + "_heater']"));  // reading hvac compressor status
                hvac.HEATER = (iwAC.Text == "ON") ? true : false;
            }
            return hvacs;
        }

        private void ChangeCallibiration(int value, bool incdec, double CurrentTemp, double maxmintemp)   // temperature, Humidity, DiffPressure
        {
            double callib = 0;
            double difference = 0;
            // go to configuration page
            driver.Url = TestCPConst.SetURL + "/config.php";

            // click on Interface tab
            driver.FindElement(By.XPath("//a[contains(text(),'Interfaces')]")).Click(); //*[@id="rwidth-duration"]/div/div[2]/div[2]/ul/li[4]/a
            Thread.Sleep(8000);
            // click power room
            driver.FindElement(By.XPath("//a[contains(text(),'Power Room')]")).Click();  // power room tab

            switch (value)
            {
                case 1:   // temperature
                    {
                        // get value dropdown
                        var ddown = driver.FindElement(By.Id("select_interface"));
                        // create select element
                        var selectelemt = new SelectElement(ddown);
                        selectelemt.SelectByValue("select_temperature_0"); // indoor temperature value    //  select_humidity_0     // value="select_diff_press_0"
                        //get temperature type -- set Celsius
                        var tempdown = driver.FindElement(By.Id("temperature_interfaces"));
                        var selecttemp = new SelectElement(tempdown);
                        selecttemp.SelectByValue("0");

                        IWebElement iwCallib = driver.FindElement(By.Id("fcb1-ind-temperature-calib"));
                        callib = Convert.ToDouble(iwCallib.GetAttribute("value"));

                        if (incdec)  // increase means to turn ON
                        {        
                            difference = maxmintemp - CurrentTemp;
                            difference = Math.Ceiling(difference);        /* Compare Current Temperature and AC Start - Upper Limit, we have to increment Current Temperature to cross Upper Limit,  Callibiration value is important, 5units = 1 degree */
                            if (difference > 0)                          // means +ve value, increase current temperature increase callibiration
                                callib = callib + TestCPConst.change; // callib = calib + 20;                
                                                                                         //oldcalib = callib;                                                          // keeping old callib value, because we update original calib value with our formula
                        }
                        else        // decrease means to turn OFF
                        {
                            difference = CurrentTemp - maxmintemp;
                            difference = Math.Ceiling(difference);
                            if (difference > 0)
                                callib = callib - TestCPConst.change;
                        }
                        iwCallib.Clear();
                        iwCallib.SendKeys(callib.ToString());        // new callibration value
                        // Update Values
                        IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-ind-temperature-submit"));
                        iwBtnUpdate.Click();
                        Thread.Sleep(2000);
                        Thread.Sleep(25000); // wait for 2 minutes to check configuration update

                    }
                    break;

                case 2:  // humidity
                    {


                        if(incdec)
                        {
                           string s =  "check this later";
                        }
                        else
                        {
                            string s = "check this later";
                        }

                    }
                    break;
                case 3:  // diff pressure
                    {


                        if(incdec)
                        {
                            string s = "check this later";
                        }
                        else
                        {
                            string s = "check this later";
                        }
                    }
                    break;

                    
            }

        }

        [TearDown]
        public void closeBrowser()
        {
            //driver.Close();
            driver.Quit();
        }

    }
}
