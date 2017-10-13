using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Lab_05
{
    public class Program
    {
        private static MqttClient client;

        //replace event with RX
        public static IObservable<MqttMsgPublishEventArgs> WhenDataReceived
        {
            get
            {
                return Observable.FromEventPattern<MqttClient.MqttMsgPublishEventHandler, MqttMsgPublishEventArgs>(
                        h => client.MqttMsgPublishReceived += h,
                        h => client.MqttMsgPublishReceived -= h)
                    .Select(x => x.EventArgs);
            }
        }

        public static void Main(string[] args)
        {
            //setup mqtt client
            SetupMqtt();
            //run simulator in another thread
            var simulatedSensor = new Task(() => LoopDeviceSimulator());
            simulatedSensor.Start();
            //filter if mqtt data is coming
            var FilteredSensorData =
                from x in WhenDataReceived
                let node = JsonConvert.DeserializeObject<SensorData>(new string(Encoding.UTF8.GetChars(x.Message)))
                where node.Temp > 50
                select x;
            //create window for 5 seconds
            var WindowedData = FilteredSensorData
                .Window(() =>
                {
                    var seqWindowControl = Observable.Interval(TimeSpan.FromSeconds(6));
                    return seqWindowControl;
                });
            var TempRate = new ConcurrentBag<int>();
            //subscribe
            WindowedData
                .Subscribe(seqwindow =>
                {
                    Console.WriteLine($"Data from {DateTime.Now.AddSeconds(-5)} to {DateTime.Now}");
                    if (TempRate.Count > 0)
                    {
                        Console.WriteLine(
                            $"average temperature in 5 secs: {TempRate.Count} items at {TempRate.Average()}");
                        int someItem;
                        while (!TempRate.IsEmpty)
                            TempRate.TryTake(out someItem);
                    }
                    seqwindow.Subscribe(e =>
                    {
                        var msg = new string(Encoding.UTF8.GetChars(e.Message));
                        Console.WriteLine($"{e.Topic} -> {msg}");
                        var node = JsonConvert.DeserializeObject<SensorData>(msg);
                        TempRate.Add(node.Temp);
                    });
                });
            //infinite delay
            Thread.Sleep(Timeout.Infinite);
        }

        private static void LoopDeviceSimulator()
        {
            var rnd = new Random(Environment.TickCount);
            while (true)
            {
                var newData = new SensorData();
                newData.DeviceName = "SimulatorDevice1";
                newData.Humid = rnd.Next(100);
                newData.Temp = rnd.Next(100);
                newData.Light = rnd.Next(1000);
                newData.Gas = rnd.Next(100);
                newData.Created = DateTime.Now;
                Console.WriteLine($"send data - temp:{newData.Temp}");
                client.Publish(TopicPublish, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newData)),
                    MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                Thread.Sleep(2000);
            }
        }

        //public static event EventHandler<MqttMsgPublishEventArgs> MqttEvent;
        private static void SetupMqtt()
        {
            // create client instance
            client = new MqttClient(MQTT_BROKER_ADDRESS);

            // register to message received - don't need this
            //client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            var clientId = "SIMULATED-DEVICE";
            client.Connect(clientId, MQTT_User, MQTT_Pass);

            client.Subscribe(new[] {TopicSubscribe}, new[] {MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE});
        }

        #region Constants

        //host url
        private const string MQTT_BROKER_ADDRESS = "cloud.makestro.com";

        //device id
        private const string MQTT_ClientId = "SimulatorDevice1";

        //devices/{device_id}/messages/events/
        private const string TopicPublish = "mifmasterz/simulateddevice/data";

        //devices/{device_id}/messages/devicebound/#
        private const string TopicSubscribe = "mifmasterz/simulateddevice/data";

        //SharedAccessSignature sig={signature-string}&se={expiry}&sr={URL-encoded-resourceURI}
        private const string MQTT_Pass = "123qweasd";

        //{iothubhostname}/{device_id}/api-version=2016-11-14
        private const string MQTT_User = "mifmasterz";

        #endregion
    }
}