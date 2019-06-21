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
    public class TcHeat
    {

        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("D:\\chromedriver_win32");
        }

        public void LoginAsAdmin()
        {
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
            driver.FindElement(By.XPath("//div[@id='box']/a/img")).Click();

            Thread.Sleep(8000);

        }

        #region Heating Test


        ///
        ///PowerRoom Heater
        ///

        [Test]
        public string TurnPowerRoomHeater(int hvac, bool onoff, double upper, double lower)  // hvac 1,2,3 .. compheater, 1=compressor, 2=heater, leadlag 1=lead,2 = lag
        {
            driver.Url = TestCPConst.SetURL+ "maindashboard.php";
            // Open HVAC dashboard                         
           // driver.FindElement(By.XPath("//div[@id='box']/a/img")).Click();
            Thread.Sleep(6000);

            bool result = false;
            string remarks = "";
            string currentlead = "";
            double CurrentTemp = 0;


            // Call Log In method
            //LoginAsAdmin();
            Thread.Sleep(8000);
            // click Frame important point
            driver.SwitchTo().Frame(0);

            // get Current Temperature value
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
            if (iwCT.Text.Length == 4)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
            }
            else if (iwCT.Text.Length == 5)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 3));
            }
            else if (iwCT.Text.Length == 6)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));
            }

            /* IWebElement iwRH = driver.FindElement(By.XPath("//span[@id='unit1_humidity']"));
             RelHumidity = Convert.ToDouble(text);

             IWebElement iwDP = driver.FindElement(By.XPath("//span[3]"));
             DiffPressure = Convert.ToDouble(text);*/

            // Get current lead and lag
            IWebElement iwHvacseq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvacseq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);
            double callib = 0;
            double difference = 0;

            if (onoff) // 1 means ON, 
            {  
                do
                {
                    // go to configuration page
                    driver.Url = TestCPConst.SetURL + "/config.php";
                    // click on Interface tab
                    driver.FindElement(By.XPath("//a[contains(text(),'Interfaces')]")).Click(); //*[@id="rwidth-duration"]/div/div[2]/div[2]/ul/li[4]/a
                    Thread.Sleep(8000);
                    // click power room
                    driver.FindElement(By.XPath("//a[contains(text(),'Power Room')]")).Click();  // power room tab

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
                    difference = CurrentTemp - lower;
                    difference = Math.Ceiling(difference);
                    /* Compare Current Temperature and AC Start - Upper Limit
                        we have to increment Current Temperature to cross Upper Limit, 
                        Callibiration value is important, 5units = 1 degree */
                    if (difference > 0)
                        callib = callib - (difference * 5) - TestCPConst.incdec;

                    iwCallib.Clear();
                    iwCallib.SendKeys(callib.ToString());                                       // new callibration value

                    // Update Values
                    IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-ind-temperature-submit"));
                    iwBtnUpdate.Click();
                    Thread.Sleep(2000);

                    Thread.Sleep(20000); // wait for 2 minutes to check configuration update

                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

                    // Move to MainForm & keep Refreshing page
                    driver.Url = TestCPConst.SetURL + "maindashboard.php";
                    Thread.Sleep(5000);
                    driver.Navigate().Refresh();
                    //driver.SwitchTo().Frame(0); 

                    Thread.Sleep(5000); // 15 secs -- 30 secs
                    driver.Navigate().Refresh();

                    Thread.Sleep(5000);
                    // get Current Temperature value
                    driver.SwitchTo().Frame(0);

                    iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
                    if (iwCT.Text.Length == 4)
                    {
                        CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
                    }
                    else if(iwCT.Text.Length == 5)
                    {
                        CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 3));
                    }
                    else if (iwCT.Text.Length == 6)
                    {
                        CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));
                    }
                    //CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
                    Thread.Sleep(3000);
                }
                while (CurrentTemp > lower);

                driver.Navigate().Refresh();
                Thread.Sleep(30000);    // 30 sec wait

            }  // end of if
            else    // turn Off
            {
                do
                {
                    // go to configuration page
                    driver.Url = TestCPConst.SetURL + "config.php";

                    // click on Interface tab
                    driver.FindElement(By.XPath("//a[contains(text(),'Interfaces')]")).Click(); //*[@id="rwidth-duration"]/div/div[2]/div[2]/ul/li[4]/a
                    Thread.Sleep(8000);
                    // click power room
                    driver.FindElement(By.XPath("//a[contains(text(),'Power Room')]")).Click();  // power room tab

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
                    //double oldcalib = 0;

                    difference = upper - CurrentTemp;
                    difference = Math.Ceiling(difference);
                    /* Compare Current Temperature and AC Start - Upper Limit
                        we have to increment Current Temperature to cross Upper Limit, 
                        Callibiration value is important, 5units = 1 degree */

                    if (difference > 0)                                                         // means +ve value, increase current temperature increase callibiration
                        callib = callib + (difference * 5) + TestCPConst.incdec; // callib = (callib - oldcalib) + (difference * 5) + 13;                     // adding 5+5+3 incase callibarated value results in exactly upper limit
                    //oldcalib = callib;                                                          // keeping old callib value, because we update original calib value with our formula
                    iwCallib.Clear();
                    iwCallib.SendKeys(callib.ToString());                                       // new callibration value

                    // Update Values
                    IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-ind-temperature-submit"));
                    iwBtnUpdate.Click();
                    Thread.Sleep(2000);

                    Thread.Sleep(25000); // wait for 2 minutes to check configuration update

                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

                    // Move to MainForm & keep Refreshing page
                    driver.Url = TestCPConst.SetURL + "maindashboard.php";
                    Thread.Sleep(5000);
                    driver.Navigate().Refresh();
                    //driver.SwitchTo().Frame(0);           

                    Thread.Sleep(5000); // 15 secs -- 30 secs
                    driver.Navigate().Refresh();
                    Thread.Sleep(5000);
                    // get Current Temperature value again 
                    driver.SwitchTo().Frame(0);
                    iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
                    if (iwCT.Text.Length == 4)
                    {
                        CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
                    }
                    else if (iwCT.Text.Length == 5)
                    {
                        CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 3));
                    }
                    else if (iwCT.Text.Length == 6)
                    {
                        CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));
                    }
                    //CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
                    Thread.Sleep(3000);

                }
                while (CurrentTemp < upper);

                driver.Navigate().Refresh();
                Thread.Sleep(30000);    //30 sec wait
            }

            driver.Navigate().Refresh();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(8000); // 8 sec wait

            iwHvacseq = driver.FindElement(By.Id("hvac" + hvac.ToString() + "_sequence"));                         // reading hvac sequence
            IWebElement iwAC = driver.FindElement(By.XPath("//*[@id='hvac" + hvac.ToString() + "_heater']"));  // reading hvac compressor status  //*[@id="hvac1_compressor"]

            if (onoff)
            {
                // Check Results
                if (iwAC.Text == "ON" && iwHvacseq.Text == "Lead")  // check HVAC lead lag button, how to read its value.. otherwise this test case is working fine
                {
                    result = true;
                    remarks = "Success: HVAC " + hvac.ToString() + "Heater Turned On Successfully and Lead remain same" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
                }
                else if (iwAC.Text == "ON" && iwHvacseq.Text == "Lag")
                {
                    result = false;
                    remarks = "Failure: HVAC " + hvac.ToString() + "Heater Turned On but Lead Changed" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
                }
                else
                {
                    remarks = "Failure: HVAC " + hvac.ToString() + "Heater Turned On Failed" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
                }

            }
            else
            {   
                // Check Results
                if (iwAC.Text == "OFF" && iwHvacseq.Text == "Lag")  // check HVAC lead lag button, how to read its value.. otherwise this test case is working fine
                {
                    result = true;
                    remarks = "Success: HVAC " + hvac.ToString() + "Heater Turned Off Successfully and Lead changed" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
                }
                else if (iwAC.Text == "OFF" && iwHvacseq.Text == "Lead")
                {
                    result = false;
                    remarks = "Failure: HVAC " + hvac.ToString() + "Heater Turned Off but Lead reamined same" + " ==== CT=" + CurrentTemp.ToString() + " Upper:" + upper.ToString() + " Lower:" + lower;
                }
                else
                {
                    remarks = "Failure: HVAC " + hvac.ToString() + "Heater Turn Off Failed" + " ==== CT=" + CurrentTemp.ToString() + " Upper:" + upper.ToString() + " Lower:" + lower;
                }


  
            }
            return remarks;

        }




        /// <summary>
        /// Turn on Heater 1 
        /// </summary>
        public string HTTurnOnHeater1Only()
        {

            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            LoginAsAdmin();

            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));

            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);

            driver.Url = TestCPConst.SetURL + "/systemcontrol.php";
            Thread.Sleep(15000);


            IWebElement iwStartHtSP = driver.FindElement(By.Id("fcb1_heater_start_temp")); // heater start   
            IWebElement iwStopHtSP = driver.FindElement(By.Id("fcb1_heater_stop_temp"));   // heater stop

            double dHtstartSP = Convert.ToDouble(iwStartHtSP.GetAttribute("value"));
            double dHtstopSP = Convert.ToDouble(iwStopHtSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp > dHtstartSP)
                dHtstartSP = dHtstartSP + (CurrentTemp - dHtstartSP + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC

            iwStartHtSP.Clear();
            iwStartHtSP.SendKeys(dHtstartSP.ToString());

            iwStopHtSP.Clear();
            dHtstopSP = dHtstartSP + TestCPConst.LuD; // LuD = LagUnitDifferential
            iwStopHtSP.SendKeys(dHtstopSP.ToString());

            // Update Values
            IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-systemcontrol-submit-sitelogic"));
            iwBtnUpdate.Click();
            Thread.Sleep(2000);

            Thread.Sleep(30000); // wait for 3 minutes to check configuration update

            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

            // Move to MainForm & keep Refreshing page
            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            //driver.SwitchTo().Frame(0);           


            driver.Navigate().Refresh();
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(30000); // 30 secs


            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHt1 = driver.FindElement(By.XPath("//*[@id='hvac1_heater']"));
            // Check Results
            if (iwHt1.Text == "ON")
            {
                result = true;
                remarks = "Heater 1 Turned On Successfully";
            }
            else
            {
                remarks = "Heater 1 Turned On Failed";
            }

            return remarks;
        }

        /// <summary>
        /// Turn off Heater 1
        /// </summary>
        public string HTTurnOffHeater1Only()
        {
            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            LoginAsAdmin();

            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));

            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);

            driver.Url = TestCPConst.SetURL + "/systemcontrol.php";
            Thread.Sleep(15000);


            IWebElement iwStartHtSP = driver.FindElement(By.Id("fcb1_heater_start_temp")); // heater start   
            IWebElement iwStopHtSP = driver.FindElement(By.Id("fcb1_heater_stop_temp"));   // heater stop

            double dHtstartSP = Convert.ToDouble(iwStartHtSP.GetAttribute("value"));
            double dHtstopSP = Convert.ToDouble(iwStopHtSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp < dHtstopSP)
                dHtstopSP = dHtstopSP - (dHtstopSP - CurrentTemp + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC

            iwStopHtSP.Clear();
            iwStopHtSP.SendKeys(dHtstopSP.ToString());

            iwStartHtSP.Clear();
            dHtstartSP = dHtstopSP - TestCPConst.LuD; // LuD = LagUnitDifferential
            iwStartHtSP.SendKeys(dHtstartSP.ToString());

            // Update Values
            IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-systemcontrol-submit-sitelogic"));
            iwBtnUpdate.Click();
            Thread.Sleep(2000);

            Thread.Sleep(30000); // wait for 3 minutes to check configuration update

            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

            // Move to MainForm & keep Refreshing page
            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            //driver.SwitchTo().Frame(0);           


            driver.Navigate().Refresh();
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(30000); // 30 secs


            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHt1 = driver.FindElement(By.XPath("//*[@id='hvac1_heater']"));
            // Check Results
            if (iwHt1.Text == "OFF")
            {
                result = true;
                remarks = "Heater 1 Turned Off Successfully";
            }
            else
            {
                remarks = "Heater 1 Turned Off Failed";
            }

            return remarks;
        }


        /// <summary>
        /// Turn ON Heater 2
        /// </summary>
        public string HTTurnOnHeater2Only()
        {
            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            LoginAsAdmin();

            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));

            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);

            driver.Url = TestCPConst.SetURL + "/systemcontrol.php";
            Thread.Sleep(15000);


            IWebElement iwStartHtSP = driver.FindElement(By.Id("fcb1_heater_start_temp")); // heater start   
            IWebElement iwStopHtSP = driver.FindElement(By.Id("fcb1_heater_stop_temp"));   // heater stop

            double dHtstartSP = Convert.ToDouble(iwStartHtSP.GetAttribute("value"));
            double dHtstopSP = Convert.ToDouble(iwStopHtSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp > dHtstartSP)
                dHtstartSP = dHtstartSP + (CurrentTemp - dHtstartSP + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC

            iwStartHtSP.Clear();
            iwStartHtSP.SendKeys(dHtstartSP.ToString());

            iwStopHtSP.Clear();
            dHtstopSP = dHtstartSP + TestCPConst.LuD; // LuD = LagUnitDifferential
            iwStopHtSP.SendKeys(dHtstopSP.ToString());

            // Update Values
            IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-systemcontrol-submit-sitelogic"));
            iwBtnUpdate.Click();
            Thread.Sleep(2000);

            Thread.Sleep(30000); // wait for 3 minutes to check configuration update

            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

            // Move to MainForm & keep Refreshing page
            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            //driver.SwitchTo().Frame(0);           


            driver.Navigate().Refresh();
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(30000); // 30 secs


            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));
            IWebElement iwHt2 = driver.FindElement(By.XPath("//*[@id='hvac2_heater']"));
            // Check Results
            if (iwHt2.Text == "ON")
            {
                result = true;
                remarks = "Heater 2 Turned On Successfully";
            }
            else
            {
                remarks = "Heater 2 Turned On Failed";
            }

            return remarks;
        }

        /// <summary>
        /// Turn Off Heater 2
        /// </summary>
        public string HTTurnOffHeater2Only()
        {
            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            LoginAsAdmin();

            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));

            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);

            driver.Url = TestCPConst.SetURL + "/systemcontrol.php";
            Thread.Sleep(15000);


            IWebElement iwStartHtSP = driver.FindElement(By.Id("fcb1_heater_start_temp")); // heater start   
            IWebElement iwStopHtSP = driver.FindElement(By.Id("fcb1_heater_stop_temp"));   // heater stop

            double dHtstartSP = Convert.ToDouble(iwStartHtSP.GetAttribute("value"));
            double dHtstopSP = Convert.ToDouble(iwStopHtSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp < dHtstopSP)
                dHtstopSP = dHtstopSP - (dHtstopSP - CurrentTemp + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC

            iwStopHtSP.Clear();
            iwStopHtSP.SendKeys(dHtstopSP.ToString());

            iwStartHtSP.Clear();
            dHtstartSP = dHtstopSP - TestCPConst.LuD; // LuD = LagUnitDifferential
            iwStartHtSP.SendKeys(dHtstartSP.ToString());

            // Update Values
            IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-systemcontrol-submit-sitelogic"));
            iwBtnUpdate.Click();
            Thread.Sleep(2000);

            Thread.Sleep(30000); // wait for 3 minutes to check configuration update

            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

            // Move to MainForm & keep Refreshing page
            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            //driver.SwitchTo().Frame(0);           


            driver.Navigate().Refresh();
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(30000); // 30 secs


            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));
            IWebElement iwHt2 = driver.FindElement(By.XPath("//*[@id='hvac2_heater']"));
            // Check Results
            if (iwHt2.Text == "OFF")
            {
                result = true;
                remarks = "Heater 2 Turned Off Successfully";
            }
            else
            {
                remarks = "Heater 2 Turned Off Failed";
            }

            return remarks;
        }


        #endregion

        [TearDown]
        public void closeBrowser()
        {
            //  driver.Close();
        }


    }
}
