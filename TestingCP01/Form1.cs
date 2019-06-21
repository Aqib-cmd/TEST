using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLTestingCP01;
using System.Xml;


namespace TestingCP01
{
    public partial class Form1 : Form
    {
        TcDashBoard obj;
        string filename = "D:\\XmlLogs.xml";
        string mainresult = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           TcCool obj = new TcCool();
            obj.startBrowser();
           rtbResult.Text = obj.CTTurnOnAC1Only();

        }

        private void btnOffAC1_Click(object sender, EventArgs e)
        {
            TcCool obj = new TcCool();
            obj.startBrowser();
            rtbResult.Text += "\n" + obj.CTTurnOffAC1Only(); 
        }

        private void btnAC2On_Click(object sender, EventArgs e)
        {
            TcCool obj = new TcCool();
            obj.startBrowser();
            rtbResult.Text = obj.CTTurnONAC2Only(); 
        }

        private void btnOffAC2_Click(object sender, EventArgs e)
        {
            TcCool obj = new TcCool();
            obj.startBrowser();
            rtbResult.Text += "\n" + obj.CTTurnOffAC2Only();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            obj.closeBrowser();
        }

        private void btnOnHeat1_Click(object sender, EventArgs e)
        {
            TcHeat obj = new TcHeat();
            obj.startBrowser();
            rtbResult.Text += "\n" + obj.HTTurnOnHeater1Only();
        }

        private void btnOffHeat1_Click(object sender, EventArgs e)
        {
            TcHeat obj = new TcHeat();
            obj.startBrowser();
            rtbResult.Text += "\n" + obj.HTTurnOffHeater1Only();

        }

        private void btnOnHeat2_Click(object sender, EventArgs e)
        {
            TcHeat obj = new TcHeat();
            obj.startBrowser();
            rtbResult.Text += "\n" + obj.HTTurnOnHeater2Only(); 
        }

        private void btnOffHeat2_Click(object sender, EventArgs e)
        {
            TcHeat obj = new TcHeat();
            obj.startBrowser();
            rtbResult.Text += "\n" + obj.HTTurnOffHeater2Only();
        }

        private void btnWriteXML_Click(object sender, EventArgs e)
        {
            try
            {
                
                XmlDocument xmlDoc = new XmlDocument();

                XmlTextWriter xWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
                xWriter.Formatting = Formatting.Indented;

                //setting XmlWriter formating to be indented
                xWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                //writing version and encoding type of XML in file.
                xWriter.WriteStartElement("Test");
                //writing first element 
                xWriter.Close();
                //closing writer



                xmlDoc.Load(filename);
                //loading XML file

                XmlNode root = xmlDoc.DocumentElement;
                //creating child nodes.
                XmlElement childNode1 = xmlDoc.CreateElement("Test1");
                XmlElement childNode2 = xmlDoc.CreateElement("Test2");
                XmlElement childNode3 = xmlDoc.CreateElement("Test3");

                //adding child node to root.
                root.AppendChild(childNode1);
                childNode1.InnerText = "HVAC1 turned On";
                //assigning innertext of childnode to text of combobox.
                root.AppendChild(childNode2);
                childNode2.InnerText = "HVAC1 turned Off";
                root.AppendChild(childNode3);
                childNode3.InnerText = "HVAC1 not responding";

                xmlDoc.Save(filename);
                //saving xml file

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
   
            //rtbResult.Text = s;
            //rtbResult.Text += "\n" + x.ToString();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {

               if (chbCool.Checked && !chbHeat.Checked)
               {
                    TcHVAC hvac1 = new TcHVAC();
                    hvac1.NUM = 1;
                    hvac1.ROOM = "Power";

                    TcHVAC hvac2 = new TcHVAC();
                    hvac2.NUM = 2;
                    hvac2.ROOM = "Power";

                    List<TcHVAC> hvacs = new List<TcHVAC>();
                    hvacs.Add(hvac1);
                    hvacs.Add(hvac2);

                    //TcTemp obj = new TcTemp();
                    //obj.startBrowser();
                    //obj.LoginAsAdmin();
                    // obj.IncreaseTemperature(hvacs);


                    
                }
                else if (chbHeat.Checked)
                  {
                    // cooling + heating
                    TcHeat obj = new TcHeat();
                    obj.startBrowser();
                    obj.LoginAsAdmin();
                    mainresult = obj.TurnPowerRoomHeater(1, true, Convert.ToDouble(tbHeaterOff.Text), Convert.ToDouble(tbHeaterstart.Text));
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    //obj.closeBrowser();
                    System.Threading.Thread.Sleep(10000);
                    mainresult = obj.TurnPowerRoomHeater(1,false, Convert.ToDouble(tbHeaterOff.Text), Convert.ToDouble(tbHeaterstart.Text));
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());

                    System.Threading.Thread.Sleep(10000);
                    mainresult = obj.TurnPowerRoomHeater(2, true, Convert.ToDouble(tbHeaterOff.Text), Convert.ToDouble(tbHeaterstart.Text));
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    //obj.closeBrowser();
                    System.Threading.Thread.Sleep(10000);

                    mainresult = obj.TurnPowerRoomHeater(2, false, Convert.ToDouble(tbHeaterOff.Text), Convert.ToDouble(tbHeaterstart.Text));
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    obj.closeBrowser();



                }
                  else if (chbHumidity.Checked)  // chbCool.Checked && chbHeat.Checked && 
                {
                    // only cooling and heating + humidity
                    TcHumid obj = new TcHumid();
                    obj.startBrowser();
                    obj.LoginAsAdmin();
                    //mainresult = obj.TurnHVACDehumidification(1, true);
                    //rtbResult.Text += "\n" + mainresult;
                    //writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    //System.Threading.Thread.Sleep(30000);
                    //mainresult = obj.TurnHVACDehumidification(1, false);
                    //rtbResult.Text += "\n" + mainresult;
                    //writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    //System.Threading.Thread.Sleep(30000);
                    mainresult = obj.TurnHVACDehumidification(2, true);
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    System.Threading.Thread.Sleep(30000);
                    mainresult = obj.TurnHVACDehumidification(2, false);
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());



                }
                else if (chbDiff.Checked) //chbCool.Checked && chbHeat.Checked && chbHumidity.Checked && 
                {
                    // cooling + heating + humidity + diffpressure
                    TcDfPressure obj = new TcDfPressure();
                    obj.startBrowser();
                    obj.LoginAsAdmin();
                    mainresult = obj.TurnOnDamperDiffPressure(1, true);
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    obj.closeBrowser();
                }
                  else
                  {
                      MessageBox.Show("Select Test");
                  }
                  
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
 
        }

        private void readdatafromXML()
        {
            // Load file in textreader
            XmlTextReader reader = new XmlTextReader(filename);
            //string s = "";
            //int x = 0;
            // Read the file
            if (System.IO.File.Exists(filename))
            {

                while (reader.Read())
                {
                    XmlNodeType nodetype = reader.NodeType;

                    //s+= reader.ReadInnerXml();
                    if (nodetype == XmlNodeType.Element)
                    {
                        rtbResult.Text += reader.Value.ToString() + "\t" + reader.ReadInnerXml() + "\n";
                        //x++;
                    }


                }  // end of while loop

            }
        }

        private void writedatainXML(string res)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            //loading XML file

            XmlNode root = xmlDoc.DocumentElement;
            //creating child nodes.
            XmlElement childNode4 = xmlDoc.CreateElement("CoolingTest");
            //XmlElement childNode2 = xmlDoc.CreateElement("Test2");
            //XmlElement childNode3 = xmlDoc.CreateElement("Test3");

            //adding child node to root.
            root.AppendChild(childNode4);
            childNode4.InnerText =res;


            xmlDoc.Save(filename);
            //saving xml file
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                TcMisc obj = new TcMisc();
                double[] darray = new double[6];
                obj.startBrowser();
                darray = obj.loadSetPoints();

                tbACstart.Text = darray[0].ToString();
                tbACstop.Text = darray[1].ToString();
                // heater
                tbHeaterstart.Text = darray[2].ToString();
                tbHeaterOff.Text = darray[3].ToString();
                // damper
                tbDamperOn.Text = darray[4].ToString();
                tbDamperOff.Text = darray[5].ToString();

                obj.closeBrowser();
                MessageBox.Show("SetPoints Load");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
    }
}

/*
 * 
 *                  /* TcICool obj = new TcICool();
                    obj.startBrowser();
                    obj.LoginAsAdmin();
                    mainresult = obj.TurnonPowerRoomCompressors(2,true);   // turn ON HVAC 1, HVAC 2 ACs
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    System.Threading.Thread.Sleep(10000);
                    mainresult = obj.TurnonPowerRoomCompressors(2, false);   // turn ON HVAC 1, HVAC 2 ACs
                    obj.closeBrowser();
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());

// only cooling test
TcICool obj = new TcICool();
obj.startBrowser();
                    obj.LoginAsAdmin();
                    mainresult = obj.TurnPowerRoomCompressor(1,true);   // turn ON HVAC 1 AC
                    rtbResult.Text += "\n"+ mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    //obj.closeBrowser();
                    System.Threading.Thread.Sleep(10000);

                    //obj.startBrowser();         
                    mainresult = obj.TurnPowerRoomCompressor(1,false);   // turn OFF HVAC 1 AC
                    rtbResult.Text += "\n"+ mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    //obj.closeBrowser();

                    ////////////////
                    System.Threading.Thread.Sleep(10000);
                    ///////////////////
                    //obj.startBrowser();
                    mainresult = obj.TurnPowerRoomCompressor(2, true);   // turn ON HVAC 2 AC
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());

                   // obj.closeBrowser();
                 

                   // obj.startBrowser();
                    mainresult = obj.TurnPowerRoomCompressor(2, false);   // turn OFF HVAC 2 AC
                    rtbResult.Text += "\n" + mainresult;
                    writedatainXML(mainresult + "--" + DateTime.Now.ToString());
                    obj.closeBrowser();
                    */