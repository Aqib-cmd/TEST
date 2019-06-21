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
    public class TcDfPressure
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

            // Open HVAC dashboard                         
            //driver.FindElement(By.XPath("//div[@id='box']/a/img")).Click();

            Thread.Sleep(3000);

        }


        [Test]
        public string TurnOnDamperDiffPressure(int hvac, bool onoff)  //*[@id="unit1_dp"]
        {
            driver.Url = TestCPConst.SetURL + "navigation.php";
            // Open HVAC dashboard                         
            driver.FindElement(By.XPath("//div[@id='box']/a/img")).Click();
            Thread.Sleep(6000);

            bool result = false;
            string remarks = "";
            string currentlead = "";
            double CurrentDP = 0;

            Thread.Sleep(8000);
            // click Frame important point
            driver.SwitchTo().Frame(0);

            // get Current differential pressure
            IWebElement iwDP = driver.FindElement(By.Id("unit1_dp")); 
            if (iwDP.Text.Length == 3)
            {
                CurrentDP = Convert.ToDouble(iwDP.Text.Substring(0, 1));
            }
            else if (iwDP.Text.Length == 4)
            {
                CurrentDP = Convert.ToDouble(iwDP.Text.Substring(0, 2));
            }
            else
                return "DiffPress value issue";

            // Get current lead and lag
            IWebElement iwHvacseq = driver.FindElement(By.Id("hvac1_sequence"));
            IWebElement iwHvac2seq = driver.FindElement(By.Id("hvac2_sequence"));

            if (iwHvacseq.Text == "Lead")
                currentlead = "HVAC1";
            else
                currentlead = "HVAC2";

            Thread.Sleep(4000);
            double lower = 0;
            double upper = 0;
            double callib = 0;
            double difference = 0;

            if (onoff) // 1 means ON
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
                    selectelemt.SelectByValue("select_diff_press_0"); // indoor temperature value    //  select_humidity_0     // value="select_diff_press_0"

                    //get temperature type -- set Celsius
                    //var tempdown = driver.FindElement(By.Id("temperature_interfaces"));
                    //var selecttemp = new SelectElement(tempdown);
                    //selecttemp.SelectByValue("0");


                    IWebElement iwLower = driver.FindElement(By.Id("fcb1-diff-press-lower"));
                    IWebElement iwUpper = driver.FindElement(By.Id("fcb1-diff-press-upper"));
                    IWebElement iwCallib = driver.FindElement(By.Id("fcb1-diff-press-calib"));

                    lower = Convert.ToDouble(iwLower.GetAttribute("value"));
                    upper = Convert.ToDouble(iwUpper.GetAttribute("value"));
                    callib = Convert.ToDouble(iwCallib.GetAttribute("value"));
                    //double oldcalib = 0;



                    difference = upper - CurrentDP;
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
                    IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-diff-press-submit"));
                    iwBtnUpdate.Click();
                    Thread.Sleep(2000);

                    Thread.Sleep(25000); // wait for 2 minutes to check configuration update

                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

                    // Move to MainForm & keep Refreshing page
                    driver.Url = TestCPConst.SetURL + "maindashboard.php";
                    //driver.SwitchTo().Frame(0);           

                    Thread.Sleep(10000); // 15 secs -- 30 secs
                    driver.Navigate().Refresh();
                    Thread.Sleep(5000);
                    // get Current Temperature value again 
                    driver.SwitchTo().Frame(0);

                    // get Current differential pressure
                    iwDP = driver.FindElement(By.Id("unit1_dp"));
                    if (iwDP.Text.Length == 3)
                    {
                        CurrentDP = Convert.ToDouble(iwDP.Text.Substring(0, 1));
                    }
                    else if (iwDP.Text.Length == 4)
                    {
                        CurrentDP = Convert.ToDouble(iwDP.Text.Substring(0, 2));
                    }
                    else
                        return "DiffPress value issue";
                    //CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
                    Thread.Sleep(3000);

                }
                while (CurrentDP < upper);

                driver.Navigate().Refresh();
                Thread.Sleep(30000);    // 50 sec wait
            }  // end of if
            else    // turn off
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
                    selectelemt.SelectByValue("select_diff_press_0"); // indoor temperature value    //  select_humidity_0     // value="select_diff_press_0"


                    IWebElement iwLower = driver.FindElement(By.Id("fcb1-diff-press-lower"));
                    IWebElement iwUpper = driver.FindElement(By.Id("fcb1-diff-press-upper"));
                    IWebElement iwCallib = driver.FindElement(By.Id("fcb1-diff-press-calib"));

                    lower = Convert.ToDouble(iwLower.GetAttribute("value"));
                    upper = Convert.ToDouble(iwUpper.GetAttribute("value"));
                    callib = Convert.ToDouble(iwCallib.GetAttribute("value"));

                    difference = CurrentDP - lower;
                    difference = Math.Ceiling(difference);
                    /* Compare Current DiffPressure and AC Start - Upper Limit
                        we have to increment Current Temperature to cross Upper Limit, 
                        Callibiration value is important, 5units = 1 degree */

                    if (difference > 0)
                        callib = callib - (difference * 5) - TestCPConst.incdec;


                    iwCallib.Clear();
                    iwCallib.SendKeys(callib.ToString());                                       // new callibration value

                    // Update Values
                    IWebElement iwBtnUpdate = driver.FindElement(By.Id("fcb1-diff-press-submit"));
                    iwBtnUpdate.Click();
                    Thread.Sleep(2000);

                    Thread.Sleep(20000); // wait for 2 minutes to check configuration update

                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[2]")).Click();
                    // driver.FindElement(By.XPath("//div[@id='box']/a/img)[3]")).Click();

                    // Move to MainForm & keep Refreshing page
                    driver.Url = TestCPConst.SetURL + "maindashboard.php";
                    //driver.SwitchTo().Frame(0); 

                    Thread.Sleep(10000); // 15 secs -- 30 secs
                    driver.Navigate().Refresh();

                    Thread.Sleep(5000);
                    // get Current Temperature value
                    driver.SwitchTo().Frame(0);

                    // get Current differential pressure
                    iwDP = driver.FindElement(By.Id("unit1_dp"));
                    if (iwDP.Text.Length == 3)
                    {
                        CurrentDP = Convert.ToDouble(iwDP.Text.Substring(0, 1));
                    }
                    else if (iwDP.Text.Length == 4)
                    {
                        CurrentDP = Convert.ToDouble(iwDP.Text.Substring(0, 2));
                    }
                    else
                        return "DiffPress value issue";
                    //CurrentTemp = Convert.ToDouble(iwCT.Text.Substring(0, 2));
                    Thread.Sleep(3000);
                }
                while (CurrentDP > lower);

                driver.Navigate().Refresh();
                Thread.Sleep(30000);    // 30 sec wait
            } // else

            driver.Navigate().Refresh();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(8000); // 8 sec wait

            iwHvacseq = driver.FindElement(By.Id("hvac" + hvac.ToString() + "_sequence")); // reading hvac sequence
            string iwHvacseQ = iwHvacseq.Text;
            // this code area is different then cooling,heating tests etc, because we check Damper in Monitor page
            driver.Url = TestCPConst.SetURL + "monitor.php";
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            IWebElement iwDamper = driver.FindElement(By.Id("f1_damper")); 
            int i = 0;

            if (hvac == 1 && iwHvacseQ == "Lead")
            {
                iwDamper = driver.FindElement(By.Id("f1_damper"));
                i = 1;
            }
            else if (hvac == 1 && iwHvacseQ == "Lag")
            {
                iwDamper = driver.FindElement(By.Id("f2_damper"));
                i = 2;
            }
            else if (hvac == 2 && iwHvacseQ == "Lead")
            {
                iwDamper = driver.FindElement(By.Id("f2_damper"));
                i = 2;
            }
            else if (hvac == 2 && iwHvacseQ == "Lag")
            {
                iwDamper = driver.FindElement(By.Id("f1_damper"));
                i = 1;
            }


            driver.Navigate().Refresh();
            Thread.Sleep(25000);

            driver.Navigate().Refresh();
            Thread.Sleep(25000);

            if (onoff)
            {
                // Check Results
                if (iwDamper.Text.Contains("Open"))  //?? this iwDamper is causing slate Exception.. Check this ?
                {
                    remarks = "Damper" + i.ToString() + "Opened Successfully";
                }
                else
                {
                    remarks = "Damper" + i.ToString() + "Opening Failed";
                }
            }   
            else
            {
                // Check Results
                if (iwDamper.Text.Contains("Clos"))
                {
                    remarks = "Damper" + i.ToString() + "Closed Successfully";
                }
                else
                {
                    remarks = "Damper" + i.ToString() + "Closing Failed";
                }
            }
            return remarks;

        }

        [TearDown]
        public void closeBrowser()
        {
            //driver.Close();
            driver.Quit();
        }

    }
}
