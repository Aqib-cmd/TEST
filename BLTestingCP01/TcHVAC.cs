using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTestingCP01
{
    public class TcHVAC
    {
        public string ROOM { get; set; }
        public int NUM { get; set; }
        public bool FAN { get; set; }
        public bool COMPRESSOR { get; set; }
        public bool HEATER { get; set; }
        public string DAMPER { get; set; }
        public double TEMPERATURE1 { get; set; }
        public double TEMPERATURE2 { get; set; }
        public double HUMIDITY1 { get; set; }
        public double HUMIDITY2 { get; set; }
        public double DIFFPRESSURE1 { get; set; }
        public double DIFFPRESSURE2 { get; set; }
        public double UPPER { get; set; }
        public double LOWER { get; set; }
        public bool LEADLAG { get; set; }  //  0 means lag, 1 means lead


    }
}
