using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TestReadOnOff
{
    class Program
    {
        static SerialPort sr = new SerialPort();
        static System.Timers.Timer tmr = new System.Timers.Timer(1000);
        static byte[] READ_ON = Encoding.UTF8.GetBytes("READON" + '\r');        // Cannot pass in Environment.NewLine, it will not work. must use \r .
        static byte[] READ_OFF = Encoding.UTF8.GetBytes("READOFF" + '\r');

        static void Main(string[] args)
        {
            sr.PortName = "COM10";
            sr.BaudRate = 115200;
            sr.DiscardNull = true;

            sr.DataReceived += Sr_DataReceived;
            tmr.Elapsed += Tmr_Elapsed;

            portOpen();
            Console.ReadLine();
        }

        private static void portClose()
        {
            try
            {
                if (sr.IsOpen)
                {
                    Thread.Sleep(100);
                    sr.Write(READ_ON, 0, READ_ON.Length);
                    Thread.Sleep(100);

                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Err:" + e.Message);
            }
        }

        private static void portOpen()
        {
            try
            {
                sr.Open();
                Thread.Sleep(100);
                sr.Write(READ_ON, 0, READ_ON.Length);
                Thread.Sleep(100);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err:" + e.Message);
            }
        }

        private static void Sr_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            Console.WriteLine("Incoming data length: " + sr.BytesToRead);

            byte[] buffer = new byte[sr.BytesToRead];
            int bytesRead = sr.Read(buffer, 0, buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesRead));

            // Off the reading.
            sr.Write(READ_OFF, 0, READ_OFF.Length);

            // Start 1 sec timer.
            tmr.Start();
        }

        private static void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Stop timer.
            tmr.Stop();

            Thread.Sleep(100);
            sr.DiscardInBuffer();
            sr.DiscardOutBuffer();

            // Re-enable reading.
            sr.Write(READ_ON, 0, READ_ON.Length);

            Thread.Sleep(100);
            sr.DiscardInBuffer();
            sr.DiscardOutBuffer();
        }
    }
}
