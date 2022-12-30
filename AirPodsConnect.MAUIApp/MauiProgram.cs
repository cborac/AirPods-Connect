using AirPodsConnect.Bluetooth;

namespace AirPodsConnect.Interface
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("SF-Pro-Display-Medium.ttf", "SFProMedium");
                });

            SetUpScripts().Wait();

            return builder.Build();
        }

        private static async Task SetUpScripts()
        {
            if (!File.Exists(AirPods.CONNECT_PS))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("headphones-connect.ps1");
                using var file = File.OpenWrite(AirPods.CONNECT_PS);
                await stream.CopyToAsync(file);
            }

            if (!File.Exists(AirPods.DISCONNECT_PS))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("headphones-disconnect.ps1");
                using var file = File.OpenWrite(AirPods.DISCONNECT_PS);
                await stream.CopyToAsync(file);
            }
        }
    }
}