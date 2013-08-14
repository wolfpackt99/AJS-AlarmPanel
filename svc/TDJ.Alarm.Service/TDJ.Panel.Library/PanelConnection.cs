using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Ninject;

namespace TDJ.Panel.Library
{
    public interface IPanelConnection : IDisposable
    {
        string ComPort { get; set; }
        Message Get();
        void Send(string text);
        void Close();
    }

    public class PanelConnection : IPanelConnection
    {
        [Inject]
        public ILog Log { get; set; }

        [Inject]
        public IMessageParser Parser { get; set; }

        public string ComPort { get; set; }
        public static SerialPort serial { get; set; }

        private Message msg = null;


        public PanelConnection()
        {
            if (string.IsNullOrEmpty(ComPort))
            {
                ComPort = "COM5";
            }
            serial = new SerialPort(ComPort, 115200, Parity.None, 8, StopBits.One);
            serial.Handshake = Handshake.None;
            //serial.WriteBufferSize = 1;
            serial.ReadTimeout = 500;
            serial.WriteTimeout = 500;
        }

        public Message Get()
        {
            msg = null;
            Log.DebugFormat("Get Start");
            if (!serial.IsOpen)
                serial.Open();
            
            Log.DebugFormat("Get Data Received");
            string raw = string.Empty;
            //var bytes = new byte[serial.BytesToRead];
            serial.DataReceived += SerialOnDataReceived;
            while (msg == null)
            {
                
            }
            // convert the bytes into a string
            //raw = System.Text.Encoding.UTF8.GetString(s);
            Log.DebugFormat("Get Close");
            serial.Close();
            return msg;
        }

        private void SerialOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            
            int dataLength = serial.BytesToRead;
            var data = new byte[dataLength];
            int nbrDataRead = serial.Read(data, 0, dataLength);
            if (nbrDataRead == 0)
                return;
            string raw = System.Text.Encoding.UTF8.GetString(data);
            msg = Parser.Process(raw);
            Log.DebugFormat("Raw Message");
            Log.DebugFormat(raw);
        }


        public void Send(string text)
        {
            Log.DebugFormat("Serial IsOpen: " + serial.IsOpen);
            if (serial.IsOpen) serial.Close();
            serial.Open();
            Log.DebugFormat("Serial Writing: " + serial.IsOpen);
            serial.Write(text);
            Log.DebugFormat("Serial Wrote: " + text);
            serial.Close();
            Log.DebugFormat("Serial Closed: " + serial.IsOpen);
        }

        public void Close()
        {
            if (serial.IsOpen)
            {
                serial.Close();
            }
        }

        public void Dispose()
        {
            serial.Close();
        }
    }
}
