# AirPods Connect
An app that helps you switch your AirPods between your Windows PC and other Apple Family Devices. Written with MAUI and .NET 7.0

![](https://i.imgur.com/DU8l06F.gif)


## Please Read!!

- This is an highly experimental app which **I don't guarantee that it'll work on each computer**
- To use you have to lower UAC restrictions 0 or else it'll ask you an UAC Prompt after pressing Connect/Disconnect


## Usage

To use you first have to clone the repo. Change the AirPodsName in `MainPage.xaml.cs#L19`. Then you have to run

```sh
dotnet publish -f net7.0-windows10.0.19041.0 -c Release
```

After that you should have your installer at `AirPodsConnect.MAUIApp\bin\Release\net7.0-windows10.0.19041.0\win10-x64\AppPackages\AirPodsConnect.MAUIApp_X.X.X.X_Test`.