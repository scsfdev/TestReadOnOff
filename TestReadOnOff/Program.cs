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
        static System.Timers.Timer tmr = new System.Timers.Timer(2000);
        static byte[] READ_ON = Encoding.UTF8.GetBytes("READON" + Environment.NewLine);
        static byte[] READ_OFF = Encoding.UTF8.GetBytes("READOFF" + Environment.NewLine);

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
                    Thread.Sleep(500);
                    sr.Write(READ_ON, 0, READ_ON.Length);
                    Thread.Sleep(500);

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
                Thread.Sleep(500);
                sr.Write(READ_ON, 0, READ_ON.Length);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Console.WriteLine("Err:" + e.Message);
            }
        }

        private static void Sr_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[sr.BytesToRead];
            int bytesRead = sr.Read(buffer, 0, buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesRead));

            sr.Write(READ_OFF, 0, READ_OFF.Length);

            Thread.Sleep(3000);

            sr.Write(READ_ON, 0, READ_ON.Length);
            Thread.Sleep(100);
            sr.DiscardOutBuffer();
            sr.DiscardInBuffer();
        }

        private static void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
