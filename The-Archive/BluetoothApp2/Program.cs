using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace BluetoothApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BluetoothClient bluetoothClient = new BluetoothClient();
            BluetoothDeviceInfo[] array = bluetoothClient.DiscoverDevices();
            
            foreach (BluetoothDeviceInfo bluetoothDeviceInfo in array)
            {
                System.Console.WriteLine(bluetoothDeviceInfo.DeviceName);

                if(!bluetoothDeviceInfo.Authenticated)
                {
                    BluetoothSecurity.PairRequest(bluetoothDeviceInfo.DeviceAddress, "1234");
                }
                else
                {
                    System.Console.WriteLine("Already authenticated");
                }

                bluetoothDeviceInfo.Refresh();
                System.Console.WriteLine(bluetoothDeviceInfo.Authenticated);
            }
        }
    }
}