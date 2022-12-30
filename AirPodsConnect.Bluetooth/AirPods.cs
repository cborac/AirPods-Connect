using Microsoft.Maui.Storage;
using System.Diagnostics;
using System.Management.Automation;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace AirPodsConnect.Bluetooth
{
    public enum ConnectionStatus
    {
        Connected = 1,
        Disconnected = 0
    }

    public class AirPods
    {
        public static readonly string CONNECT_PS = Path.Combine(FileSystem.Current.AppDataDirectory, "connect_headphones.ps1");
        public static readonly string DISCONNECT_PS = Path.Combine(FileSystem.Current.AppDataDirectory, "disconnect_headphones.ps1");

        public BluetoothDevice device;

        public ConnectionStatus ConnectionStatus { get => (ConnectionStatus)device.ConnectionStatus; }

        public AirPods(BluetoothDevice device)
        {
            this.device = device;
        }

        static public async Task<AirPods> FindAirPodsAsync(string name)
        {
            var list = await DeviceInformation.FindAllAsync(
                BluetoothDevice.GetDeviceSelector()
            );

            var deviceInformation = list.First(x => x.Name == name);

            if (deviceInformation == null) { throw new Exception(); }

            var device = await BluetoothDevice.FromIdAsync(deviceInformation.Id);

            return new AirPods(device);
        }

        public void Connect()
        {
            var process = new Process();
            process.StartInfo.FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";
            process.StartInfo.Arguments = CONNECT_PS;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.Verb = "runas";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
        }

        public void Disconnect()
        {
            var process = new Process();
            process.StartInfo.FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";
            process.StartInfo.Arguments = DISCONNECT_PS;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.Verb = "runas";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
        }
    }
}