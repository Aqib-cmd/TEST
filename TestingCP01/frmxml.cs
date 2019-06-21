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
using System.IO;
using System.Xml.Serialization;

namespace TestingCP01
{
    public partial class frmxml : Form
    {
        List<TcHVAC> hvaclist = new List<TcHVAC>();
        TcHVAC hvac1 = new TcHVAC();
         XmlElement Test;
         XmlElement HVAC;
         XmlElement Tests;


        XmlDocument xmlDoc = new XmlDocument();
        //creating child nodes.
         XmlNode xmlNode;
        public frmxml()
        {
            InitializeComponent();

            hvac1.NUM = 1;
            hvac1.ROOM = "power";
            hvac1.COMPRESSOR = true;
            hvac1.HEATER = false;
            hvac1.FAN = true;
            hvaclist.Add(hvac1);

            TcHVAC hvac2 = new TcHVAC();
            hvac2.NUM = 2;
            hvac2.ROOM = "Room";
            hvac2.COMPRESSOR = false;
            hvac2.HEATER = false;
            hvac2.FAN = false;
            hvaclist.Add(hvac2);

            TcHVAC hvac3 = new TcHVAC();
            hvac3.NUM = 3;
            hvac3.ROOM = "power";
            hvac3.COMPRESSOR = true;
            hvac3.HEATER = true;
            hvac3.FAN = true;
            hvaclist.Add(hvac3);
        }

        private void Create_Click(object sender, EventArgs e)
        {
               //CREATE XMLFile
                methodCreateXMLbyUser();
           
        }
        private void Read_Click(object sender, EventArgs e)
        {
           
            try
            {
                //Write
                //methodWriteData();
                methodWriteDataFile();
                //Read
                //methodReadData();
                //methodReadDataFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                string filetext = File.ReadAllText(filename);
                richtb.Text = filetext;
            }
        }

        

        //USER METHODS
        /*private void methodCreateXML()
         {
             //STATIC
             //XmlWriter writer = XmlWriter.Create("E:\\XML Project\\books.xml");
             //writer.WriteStartDocument();
             //writer.WriteStartElement("book");
             //writer.WriteStartElement("books");
             //writer.WriteStartElement("HVAC");
             //writer.WriteElementString("title", "Graphics Programming using GDI+");
             //writer.WriteElementString("author", "Mahesh Chand");
             //writer.WriteElementString("publisher", "Addison-Wesley");
             //writer.WriteElementString("price", "64.95");
             //writer.WriteEndElement();
             //writer.WriteEndDocument();
             //writer.Flush(); 

     StringWriter stringwriter = new StringWriter();
     XmlTextWriter xmlwriter = new XmlTextWriter(stringwriter);
     xmlwriter.Formatting = Formatting.Indented;
     xmlwriter.WriteStartDocument();
     xmlwriter.WriteStartElement("root");
     xmlwriter.WriteStartElement("information");
     xmlwriter.WriteEndElement();
     xmlwriter.WriteEndDocument();
     XmlDocument docSave = new XmlDocument();
     docSave.LoadXml(stringwriter.ToString());
     docSave.Save(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "xml") + Guid.NewGuid().ToString() + ".xml");

     MessageBox.Show("File Created !"); 
         } */
        private void methodCreateXMLbyUser()
        {
            // Load file in textreader
            //XmlTextReader XMLreader = null;
           // XMLreader = new XmlTextReader(hvaclist.ToString());

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*";      

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Saving Setting
                Stream s = File.Open(saveFileDialog.FileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(s);
                //sw.Write(hvaclist.ToString());
               sw.Write(richtb.Text);
                MessageBox.Show("File Created !");
                s.Close();
            
            }
        }
        private void tags(string _num, string _room, string _comp, string _heater, string _fan)
        {
            HVAC = xmlDoc.CreateElement("HVAC");

            //Elements TAGS Sequence
            xmlNode = xmlDoc.DocumentElement;
            xmlNode.AppendChild(Tests);
            Tests.AppendChild(Test);
            Test.AppendChild(HVAC);
            //<TAGS>
            XmlElement NUM = xmlDoc.CreateElement("NUM");
            XmlElement ROOM = xmlDoc.CreateElement("ROOM");
            XmlElement COMPRESSOR = xmlDoc.CreateElement("COMPRESSOR");
            XmlElement HEATER = xmlDoc.CreateElement("HEATER");
            XmlElement FAN = xmlDoc.CreateElement("FAN");

            // *** PARAMETERS ***
            //NUM
            HVAC.AppendChild(NUM);
            NUM.InnerText = _num.ToString();

            //ROOM
            HVAC.AppendChild(ROOM);
            ROOM.InnerText = hvac1.ROOM.ToString();
            //COMPRESSOR
            HVAC.AppendChild(COMPRESSOR);
            COMPRESSOR.InnerText = _comp.ToString();
            //HEATER
            HVAC.AppendChild(HEATER);
            HEATER.InnerText = _heater.ToString();
            //FAN
            HVAC.AppendChild(FAN);
            FAN.InnerText = _fan.ToString();
        }

        //READ From List
        //--------------START-----------------//
        private void methodReadData()
        {
            // Load file in textreader
            XmlTextReader XMLreader = null;
            XMLreader = new XmlTextReader(hvaclist.ToString());

            if (System.IO.File.Exists(hvaclist.ToString()))
            {
                while (XMLreader.Read())
                {
                    XmlNodeType inArrayList_nodetype = XMLreader.NodeType;
                    if (inArrayList_nodetype == XmlNodeType.Element)
                    {
                        richtb.Text += XMLreader.Value.ToString() + "\t " + XMLreader.ReadInnerXml() + "\n ";
                    }
                }
            }
            //XMLreader.Close();
        }
        private void methodWriteData()
        {
            //creating XmlTextWriter, and passing file name and encoding type as argument
            XmlTextWriter xmlWriter = new XmlTextWriter(hvaclist.ToString(), System.Text.Encoding.UTF8);
            //setting XmlWriter formating to be indented
            xmlWriter.Formatting = Formatting.Indented;
            //writing version and encoding type of XML in file.
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            //writing first element 
            xmlWriter.WriteStartElement("Tests");
            //closing writer
            xmlWriter.Close();

            //loading XML file
            xmlDoc.Load(hvaclist.ToString());
           
            //creating Elements
            Tests = xmlDoc.CreateElement("Tests");
            Test = xmlDoc.CreateElement("Test");
           

            foreach (TcHVAC hv in hvaclist)
            {
                tags(hv.NUM.ToString(), hvac1.ROOM.ToString(), hvac1.COMPRESSOR.ToString(), hvac1.HEATER.ToString(), hvac1.FAN.ToString());
            }
            xmlDoc.Save(hvaclist.ToString());
        }

        //--------------END-----------------//
        
      
        //READ From FILE
        //--------------START-----------------//
        private void methodReadDataFile()
        {
            // Load file in textreader
            XmlTextReader XMLreader = null;
            XMLreader = new XmlTextReader(hvaclist.ToString());

            if (System.IO.File.Exists(hvaclist.ToString()))
            {
                while (XMLreader.Read())
                {
                    XmlNodeType inArrayList_nodetype = XMLreader.NodeType;
                    if (inArrayList_nodetype == XmlNodeType.Element)
                    {
                        richtb.Text += XMLreader.Value.ToString() + "\t " + XMLreader.ReadInnerXml() + "\n ";
                    }
                }
            }
            //XMLreader.Close();
        }
        private void methodWriteDataFile()
        {
            //creating XmlTestWriter, and passing file name and encoding type as argument
            XmlTextWriter xmlWriter = new XmlTextWriter(hvaclist.ToString(), System.Text.Encoding.UTF8);
            //setting XmlWriter formating to be indented
            xmlWriter.Formatting = Formatting.Indented;
            //writing version and encoding type of XML in file.
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            //writing first element 
            xmlWriter.WriteStartElement("Tests");
            //closing writer
            xmlWriter.Close();

            //loading XML file
            xmlDoc.Load(hvaclist.ToString());

            //creating Elements
            Tests = xmlDoc.CreateElement("Tests");
            Test = xmlDoc.CreateElement("Test");
            

            foreach (TcHVAC hv in hvaclist)
            {
                tags(hv.NUM.ToString(), hvac1.ROOM.ToString(), hvac1.COMPRESSOR.ToString(), hvac1.HEATER.ToString(), hvac1.FAN.ToString());
            }
            xmlDoc.Save(hvaclist.ToString());

            //METHOD 01
            SaveFileDialog saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                File.WriteAllText(saveDialog.FileName, hvaclist.ToString());

             //METHOD 02
                //XmlSerializer xs = new XmlSerializer(typeof(List<TcHVAC>));
            // FileStream fs = new FileStream("E:\\XML Project\\BBB.xml", FileMode.Open, FileAccess.Write);
           // xs.Serialize(fs, hvaclist);
            //fs.Close();
            }
            MessageBox.Show(" Data Inserted Successfully ");
            
        }

        //--------------END-----------------//
       
    }
}
