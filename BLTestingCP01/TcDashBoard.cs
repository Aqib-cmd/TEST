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
    public class TcDashBoard
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
            CurrentTemp = Convert.ToDouble( iwCT.Text.Substring(0,4));

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

            driver.Url =TestCPConst.SetURL+"/systemcontrol.php";
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
                remarks = "HVAC1 Turn On Failed";
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
            if(iwCT.Text.Length == 4)
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
            dACstartSP= dACstopSP + TestCPConst.LuD; // LuD = LagUnitDifferential + stop point
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
                remarks = "HVAC 1 Turn Off Test Failed";
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
                remarks = "HVAC 2 AC Turned On Successfully";
            }
            else
            {
                remarks = "HVAC2 AC Turn On Test Failed";
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
                remarks = "HVAC2 AC Turned off Successfully";
            }
            else
            {
                remarks = "HVAC2 AC Turn Off Test Failed";
            }

            return remarks;
        }


        #endregion


        #region Heating Test

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
                remarks = "Heater1 Turned On Successfully";
            }
            else
            {
                remarks = "Heater 1 Turn On Failed";
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
                remarks = "Heater 1 Turn Off Failed";
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
                remarks = "Heater 2 Turn On Failed";
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
                remarks = "Heater 2 Turn Off Failed";
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


/* 
 *             // Find elements using X-PAth
            text = driver.FindElement(By.XPath("//span[@id='unit1_temperature']")).Text;
            CurrentTemp = Convert.ToDouble(text);

            text = driver.FindElement(By.XPath("//span[@id='unit1_temperature']")).Text;
            RelHumidity = Convert.ToDouble(text);

            text = driver.FindElement(By.XPath("//span[@id='unit1_temperature']")).Text;
            DiffPressure = Convert.ToDouble(text);

            lead = driver.FindElement(By.XPath("//span[@id='unit1_temperature']")).Text;
                   
            lag = driver.FindElement(By.XPath()).Text;
*/