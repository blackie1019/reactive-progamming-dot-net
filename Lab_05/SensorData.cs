using System;

namespace Lab_05
{
    public class SensorData
    {
        public string DeviceName { set; get; }
        public int Temp { set; get; }
        public int Humid { set; get; }
        public int Light { set; get; }
        public int Gas { set; get; }
        public DateTime Created { set; get; }
    }
}
