using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace BluetoothApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BluetoothClient client = new BluetoothClient();
            BluetoothDeviceInfo[] devices = client.DiscoverDevices();

            BluetoothDeviceInfo device = null;

            foreach (BluetoothDeviceInfo d in devices)
            {
                if(d.DeviceName.Contains("boAt"))
                {
                    device = d;
                    break;
                }
            }

            if(device == null)
            {
                Console.WriteLine("No device found");
                return;
            }
            else
            {
                Console.WriteLine(device.ToString() + "Device found");
            }

            // list of some common bluetooth pins
            string[] pins = new string[] { "0000", "1111", "1234", "9999" };

            // try to connect to the device using the pins
            foreach (string pin in pins)
            {
                if(device.Authenticated)
                {
                    Console.WriteLine("Device already authenticated");
                    break;
                }
                if (BluetoothSecurity.PairRequest(device.DeviceAddress, pin))
                {
                    Console.WriteLine("Device paired");
                    break;
                }
            }

            try 
            { 
                client.Connect(device.DeviceAddress, BluetoothService.SerialPort);

                var stream = client.GetStream();
                StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.ASCII);
                sw.WriteLine("Hello world!\r\n\r\n");
                sw.Close();

                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to device " + e);
            }
        }
    }
}
