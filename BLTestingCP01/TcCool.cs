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
    public class TcCool
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

        #region Cooling Test
        /// <summary>
        /// Cooling Test Turn On Power Room AC1
        /// </summary>
        [Test]
        public string CTTurnOnAC1Only()
        {
            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            // Call Log In 
            //TcLogin login = new TcLogin();
            LoginAsAdmin();

            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));

            // CurrentTemp = Convert.ToDouble(text);

            /* IWebElement iwRH = driver.FindElement(By.XPath("//span[@id='unit1_humidity']"));
             RelHumidity = Convert.ToDouble(text);

             IWebElement iwDP = driver.FindElement(By.XPath("//span[3]"));
             DiffPressure = Convert.ToDouble(text);*/

            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);

            driver.Url = TestCPConst.SetURL + "/systemcontrol.php";
            Thread.Sleep(15000);


            IWebElement iwStartACSP = driver.FindElement(By.Id("fcb1_hvac_start_temp"));
            IWebElement iwStopACSP = driver.FindElement(By.Id("fcb1_hvac_stop_temp"));

            double dACstartSP = Convert.ToDouble(iwStartACSP.GetAttribute("value"));
            double dACstopSP = Convert.ToDouble(iwStopACSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp < dACstartSP)
                dACstartSP = dACstartSP - (dACstartSP - CurrentTemp + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC

            iwStartACSP.Clear();
            iwStartACSP.SendKeys(dACstartSP.ToString());

            iwStopACSP.Clear();
            dACstopSP = dACstartSP - TestCPConst.LuD; // LuD = LagUnitDifferential
            iwStopACSP.SendKeys(dACstopSP.ToString());

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
            IWebElement iwAC1 = driver.FindElement(By.XPath("//*[@id='hvac1_compressor']"));
            // Check Results
            if (iwAC1.Text == "ON")
            {
                result = true;
                remarks = "HVAC1 Turned On Successfully";
            }
            else
            {
                remarks = "HVAC 2 Turned On Failed";
            }

            return remarks;

        }
        /// <summary>
        /// Cooling Test Turn Off Power Room AC1
        /// </summary>
        [Test]
        public string CTTurnOffAC1Only()
        {
            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            // Call Log In 
            //TcLogin login = new TcLogin();
            LoginAsAdmin();

            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            if (iwCT.Text.Length == 4)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
            }
            else if (iwCT.Text.Length == 6)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));
            }


            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            // Check Lead Lag
            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(15000);

            // Move to System Configuration Form
            driver.Url = TestCPConst.SetURL + "systemcontrol.php";
            Thread.Sleep(4000);

            IWebElement iwStartACSP = driver.FindElement(By.Id("fcb1_hvac_start_temp"));
            IWebElement iwStopACSP = driver.FindElement(By.Id("fcb1_hvac_stop_temp"));

            double dACstartSP = Convert.ToDouble(iwStartACSP.GetAttribute("value"));
            double dACstopSP = Convert.ToDouble(iwStopACSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp > dACstopSP) // just to check Current Temp is Greater then Setpoints, we want To turn off AC, so we will increase Stop AC Limit
                dACstopSP = dACstopSP + (CurrentTemp - dACstopSP + 2); //   Differnce b/w current temo and AC Stop Point, will be added with ACStopPoint to raise ACStopPoint
                                                                       // dACstartSP = dACstartSP - (dACstartSP - CurrentTemp + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC

            iwStartACSP.Clear();
            dACstartSP = dACstopSP + TestCPConst.LuD; // LuD = LagUnitDifferential + stop point
            iwStartACSP.SendKeys(dACstartSP.ToString());

            iwStopACSP.Clear();
            iwStopACSP.SendKeys(dACstopSP.ToString());

            // Update Values
            //driver.SwitchTo().Frame(0);
            IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-systemcontrol-submit-sitelogic"));
            iwBtnUpdate.Click();
            Thread.Sleep(20000); // refresh after 20 secs
            driver.Navigate().Refresh();

            Thread.Sleep(30000); // wait for 3 minutes to check configuration update

            // Move to MainForm & keep Refreshing page
            driver.Url = TestCPConst.SetURL + "/maindashboard.php";
            driver.SwitchTo().Frame(0);


            driver.Navigate().Refresh();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(30000); // 30 secs


            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwAC1 = driver.FindElement(By.XPath("//*[@id='hvac1_compressor']"));
            // Check Results
            if (iwAC1.Text == "OFF")
            {
                result = true;
                remarks = "HVAC 1 Turned Off Successfully";
            }
            else
            {
                remarks = "HVAC 1 Turned Off Test Failed";
            }

            return remarks;


        }

        /// <summary>
        /// Cooling Test Turn On Power Room AC 2
        /// </summary>
        [Test]
        public string CTTurnONAC2Only()
        {
            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            // Call Log In 
            //TcLogin login = new TcLogin();
            LoginAsAdmin();

            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));

            // CurrentTemp = Convert.ToDouble(text);

            /* IWebElement iwRH = driver.FindElement(By.XPath("//span[@id='unit1_humidity']"));
             RelHumidity = Convert.ToDouble(text);

             IWebElement iwDP = driver.FindElement(By.XPath("//span[3]"));
             DiffPressure = Convert.ToDouble(text);*/

            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);

            driver.Url = TestCPConst.SetURL + "/systemcontrol.php";
            Thread.Sleep(15000);


            IWebElement iwStartACSP = driver.FindElement(By.Id("fcb1_hvac_start_temp"));
            IWebElement iwStopACSP = driver.FindElement(By.Id("fcb1_hvac_stop_temp"));

            double dACstartSP = Convert.ToDouble(iwStartACSP.GetAttribute("value"));
            double dACstopSP = Convert.ToDouble(iwStopACSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp < dACstartSP)
                dACstartSP = dACstartSP - (dACstartSP - CurrentTemp + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC

            iwStartACSP.Clear();
            iwStartACSP.SendKeys(dACstartSP.ToString());

            iwStopACSP.Clear();
            dACstopSP = dACstartSP - TestCPConst.LuD; // LuD = LagUnitDifferential
            iwStopACSP.SendKeys(dACstopSP.ToString());

            // Update Values
            IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-systemcontrol-submit-sitelogic"));
            iwBtnUpdate.Click();
            Thread.Sleep(2000);

            Thread.Sleep(30000); // wait for 3 minutes to check configuration update

            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
            // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

            // Move to MainForm & keep Refreshing page
            driver.Url = TestCPConst.SetURL + "/maindashboard.php";
            driver.SwitchTo().Frame(0);


            driver.Navigate().Refresh();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(30000); // 30 secs


            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));
            IWebElement iwAC2 = driver.FindElement(By.XPath("//*[@id='hvac2_compressor']"));
            // Check Results
            if (iwAC2.Text == "ON")
            {
                result = true;
                remarks = "HVAC 2 Turned On Successfully";
            }
            else
            {
                remarks = "HVAC 2 Turned On Test Failed";
            }

            return remarks;

        }


        /// <summary>
        /// Cooling Test Turn Off Power Room AC 2
        /// </summary>
        [Test]
        public string CTTurnOffAC2Only()
        {
            bool result = false;
            string remarks = "";
            string text = "";
            double CurrentTemp = 0;
            double RelHumidity = 0;
            double DiffPressure = 0;
            string currentlead = "";
            string currentlag = "";

            // Call Log In 
            //TcLogin login = new TcLogin();
            LoginAsAdmin();

            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            driver.SwitchTo().Frame(0);
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature"));
            if (iwCT.Text.Length == 4)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
            }
            else if (iwCT.Text.Length == 6)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));
            }


            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            // Check Lead Lag
            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(15000);

            // Move to System Configuration Form
            driver.Url = TestCPConst.SetURL + "systemcontrol.php";
            Thread.Sleep(4000);

            IWebElement iwStartACSP = driver.FindElement(By.Id("fcb1_hvac_start_temp"));
            IWebElement iwStopACSP = driver.FindElement(By.Id("fcb1_hvac_stop_temp"));

            double dACstartSP = Convert.ToDouble(iwStartACSP.GetAttribute("value"));
            double dACstopSP = Convert.ToDouble(iwStopACSP.GetAttribute("value"));

            // Compute Set Points
            if (CurrentTemp > dACstopSP) // just to check Current Temp is Greater then Setpoints, we want To turn off AC, so we will increase Stop AC Limit
                dACstopSP = dACstopSP + (CurrentTemp - dACstopSP + 2); //   Differnce b/w current temo and AC Stop Point, will be added with ACStopPoint to raise ACStopPoint
                                                                       // dACstartSP = dACstartSP - (dACstartSP - CurrentTemp + 2);  // Difference b/w Current Temp and AC StartPoint, will be subtracted from ACStartpoint to bring Temp down to start aC



            iwStartACSP.Clear();
            dACstartSP = dACstopSP + TestCPConst.LuD; // LuD = LagUnitDifferential + stop point
            iwStartACSP.SendKeys(dACstartSP.ToString());

            iwStopACSP.Clear();
            iwStopACSP.SendKeys(dACstopSP.ToString());

            // Update Values
            //driver.SwitchTo().Frame(0);
            IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-systemcontrol-submit-sitelogic"));
            iwBtnUpdate.Click();
            Thread.Sleep(20000); // refresh after 20 secs
            driver.Navigate().Refresh();

            Thread.Sleep(30000); // wait for 3 minutes to check configuration update

            // Move to MainForm & keep Refreshing page
            driver.Url = TestCPConst.SetURL + "maindashboard.php";
            //driver.SwitchTo().Frame(0);


            driver.Navigate().Refresh();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(30000); // 30 secs


            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));
            IWebElement iwAC2 = driver.FindElement(By.XPath("//*[@id='hvac2_compressor']"));
            // Check Results
            if (iwAC2.Text == "OFF")
            {
                result = true;
                remarks = "HVAC 2 AC Turned off Successfully";
            }
            else
            {
                remarks = "HVAC 2 AC Turned Off Test Failed";
            }

            return remarks;
        }


        #endregion

        [TearDown]
        public void closeBrowser()
        {
            //  driver.Close();
        }



        [Test]
        public string TurnOnPowerRoomAC1(int hvac, int compheater, int leaglag)  // hvac 1,2,3 .. compheater, 1=compressor, 2=heater, leadlag 1=lead,2 = lag
        {
            bool result = false;
            string remarks = "";
            string currentlead = "";
            double CurrentTemp = 0;


            // Call Log In method
            LoginAsAdmin();
            Thread.Sleep(8000);
            // click Frame important point
            driver.SwitchTo().Frame(0);

            // get Current Temperature value
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
            if (iwCT.Text.Length == 4)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
            }
            else if (iwCT.Text.Length == 6)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));
            }

            // CurrentTemp = Convert.ToDouble(text);

            /* IWebElement iwRH = driver.FindElement(By.XPath("//span[@id='unit1_humidity']"));
             RelHumidity = Convert.ToDouble(text);

             IWebElement iwDP = driver.FindElement(By.XPath("//span[3]"));
             DiffPressure = Convert.ToDouble(text);*/

            // Get current lead and lag
            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);
            double lower = 0;
            double upper = 0;
            double callib = 0;
            double difference = 0;
            // go to configuration page
            driver.Url = TestCPConst.SetURL + "/config.php";

            // click on Interface tab
            driver.FindElement(By.XPath("//a[contains(text(),'Interfaces')]")).Click(); //*[@id="rwidth-duration"]/div/div[2]/div[2]/ul/li[4]/a
            Thread.Sleep(8000);

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


            IWebElement iwLower = driver.FindElement(By.Id("fcb1-ind-temperature-lower"));
            IWebElement iwUpper = driver.FindElement(By.Id("fcb1-ind-temperature-upper"));
            IWebElement iwCallib = driver.FindElement(By.Id("fcb1-ind-temperature-calib"));

            lower = Convert.ToDouble(iwLower.GetAttribute("value"));
            upper = Convert.ToDouble(iwUpper.GetAttribute("value"));
            callib = Convert.ToDouble(iwCallib.GetAttribute("value"));
            double oldcalib = 0;
            do
            {
                difference = upper - CurrentTemp;
                difference = Math.Ceiling(difference);
                /* Compare Current Temperature and AC Start - Upper Limit
                    we have to increment Current Temperature to cross Upper Limit, 
                    Callibiration value is important, 5units = 1 degree */

                if (difference > 0)                                                         // means +ve value, increase current temperature increase callibiration
                    callib = (callib - oldcalib) + (difference * 5) + 13;                     // adding 5+5+3 incase callibarated value results in exactly upper limit
                oldcalib = callib;                                                          // keeping old callib value, because we update original calib value with our formula
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
                //driver.SwitchTo().Frame(0);           

                Thread.Sleep(15000); // 15 secs -- 30 secs
                driver.Navigate().Refresh();

                // get Current Temperature value
                driver.SwitchTo().Frame(0);
                iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
                if (iwCT.Text.Length == 4)
                {
                    CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
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
            Thread.Sleep(30000);    // 50 sec wait
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            Thread.Sleep(8000); // 8 sec wait
            iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwAC1 = driver.FindElement(By.XPath("//*[@id='hvac1_compressor']"));   //*[@id="hvac1_compressor"]

            // Check Results
            if (iwAC1.Text == "ON" && iwHvac1seq.Text == "Lead")  // check HVAC lead lag button, how to read its value.. otherwise this test case is working fine
            {
                result = true;
                remarks = "Success: HVAC1 Turned On Successfully and Lead remain same" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
            }
            else if (iwAC1.Text == "ON" && iwHvac1seq.Text == "Lag")
            {
                result = false;
                remarks = "Failure: HVAC Turned On but Lead Changed" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
            }
            else
            {
                remarks = "Failure: HVAC1 Turned On Failed" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
            }

            return remarks;

        }



        [Test]
        public string TurnOffPowerRoomAC1()
        {
            bool result = false;
            string remarks = "";
            string currentlead = "";
            double CurrentTemp = 0;


            // Call Log In method
            LoginAsAdmin();
            Thread.Sleep(8000);
            // click Frame important point
            driver.SwitchTo().Frame(0);

            // get Current Temperature value
            IWebElement iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
            if (iwCT.Text.Length == 4)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
            }
            else if (iwCT.Text.Length == 6)
            {
                CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 4));
            }

            // CurrentTemp = Convert.ToDouble(text);

            /* IWebElement iwRH = driver.FindElement(By.XPath("//span[@id='unit1_humidity']"));
             RelHumidity = Convert.ToDouble(text);

             IWebElement iwDP = driver.FindElement(By.XPath("//span[3]"));
             DiffPressure = Convert.ToDouble(text);*/

            // Get current lead and lag
            IWebElement iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvac1seq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);
            double lower = 0;
            double upper = 0;
            double callib = 0;
            double difference = 0;
            // go to configuration page
            driver.Url = TestCPConst.SetURL + "/config.php";

            // click on Interface tab
            driver.FindElement(By.XPath("//a[contains(text(),'Interfaces')]")).Click(); //*[@id="rwidth-duration"]/div/div[2]/div[2]/ul/li[4]/a
            Thread.Sleep(8000);

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


            IWebElement iwLower = driver.FindElement(By.Id("fcb1-ind-temperature-lower"));
            IWebElement iwUpper = driver.FindElement(By.Id("fcb1-ind-temperature-upper"));
            IWebElement iwCallib = driver.FindElement(By.Id("fcb1-ind-temperature-calib"));

            lower = Convert.ToDouble(iwLower.GetAttribute("value"));
            upper = Convert.ToDouble(iwUpper.GetAttribute("value"));
            callib = Convert.ToDouble(iwCallib.GetAttribute("value"));
            double oldcalib = 0;
            do
            {
                difference = CurrentTemp - lower;
                difference = Math.Ceiling(difference);
                /* Compare Current Temperature and AC Start - Upper Limit
                    we have to increment Current Temperature to cross Upper Limit, 
                    Callibiration value is important, 5units = 1 degree */

                if (difference > 0 && oldcalib != 0)
                {
                    // means +ve value, decrease current temperature decrease callibiration
                    callib = (oldcalib - callib) - (difference * 5) - 13;                     // subtracting 5+5+3 incase callibarated value results in exactly upper limit

                }
                else
                {
                    oldcalib = callib;
                    callib = callib - (difference * 5) - 13;
                }

                // keeping old callib value, because we update original calib value with our formula
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
                //driver.SwitchTo().Frame(0);           

                Thread.Sleep(15000); // 15 secs -- 30 secs
                driver.Navigate().Refresh();

                // get Current Temperature value
                driver.SwitchTo().Frame(0);

                iwCT = driver.FindElement(By.Id("unit1_temperature")); //unit1_temperature
                if (iwCT.Text.Length == 4)
                {
                    CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
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
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame(0);
            Thread.Sleep(8000); // 8 sec wait
            iwHvac1seq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwAC1 = driver.FindElement(By.XPath("//*[@id='hvac1_compressor']"));   //*[@id="hvac1_compressor"]


            // Check Results
            if (iwAC1.Text == "OFF" && iwHvac1seq.Text == "Lag")  // check HVAC lead lag button, how to read its value.. otherwise this test case is working fine
            {
                result = true;
                remarks = "Success: HVAC1 Turned Off Successfully and Lead changed" + " ==== CT=" + CurrentTemp.ToString() + "Upper:" + upper.ToString() + "Lower:" + lower;
            }
            else if (iwAC1.Text == "OFF" && iwHvac1seq.Text == "Lead")
            {
                result = false;
                remarks = "Failure: HVAC1 Turned Off but Lead reamined same" + " ==== CT=" + CurrentTemp.ToString() + " Upper:" + upper.ToString() + " Lower:" + lower;
            }
            else
            {
                remarks = "Failure: HVAC1 Turn Off Failed" + " ==== CT=" + CurrentTemp.ToString() + " Upper:" + upper.ToString() + " Lower:" + lower;
            }

            return remarks;

        }



    }
}
