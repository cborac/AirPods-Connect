using Microsoft.Maui.Controls.Compatibility.Platform.UWP;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using WinRT.Interop;
using Windows.Graphics;
using AirPodsConnect.Bluetooth;

namespace AirPodsConnect.Interface
{
    enum Status
    {
        Connecting,
        Disconnected,
        Connected
    }

    public partial class MainPage : ContentPage
    {
        private const string AirPodsName = "Bora’nın İşitme Cihazı";
        AirPods airpods;

        Status status = Status.Disconnected;

        public MainPage()
        {
            InitializeComponent();
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await InitiateAirPods();
                PrepareWebView();
            });
        }

        private async Task InitiateAirPods()
        {
            airpods = await AirPods.FindAirPodsAsync(AirPodsName);

            airpods.device.ConnectionStatusChanged += (sender, args) => MainThread.BeginInvokeOnMainThread(Update);
        }

        private async Task PrepareWebView()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("view.html");
            using var reader = new StreamReader(stream);

            string document = reader.ReadToEnd();

            webView.Source = new HtmlWebViewSource()
            {
                Html = document.Replace("//<x:Function>", airpods.ConnectionStatus == ConnectionStatus.Connected ? "openPods()" : "")
            };

            Update();
        }

        private void Update()
        {
            if (airpods.ConnectionStatus == ConnectionStatus.Connected)
            {
                Title.Text = "Connected";
                Button.Text = "Disconnect"; 
                Circle.Fill = new SolidColorBrush(new Color(48, 209, 88));
                status = Status.Connected;
                webView.EvaluateJavaScriptAsync("window.openPods()");
            }
            else
            {
                Title.Text = "Disconnected";
                Button.Text = "Connect";
                Circle.Fill = new SolidColorBrush(new Color(255, 69, 58));
                status = Status.Disconnected;
                webView.EvaluateJavaScriptAsync("window.closePods()");
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (status == Status.Connected)
            {
                Button.Text = "Disconnecting...";
                airpods.Disconnect();
            }
            else if (status == Status.Disconnected)
            {
                Button.Text = "Connecting...";
                airpods.Connect();
            }
            status = Status.Connecting;
        }
    }
}