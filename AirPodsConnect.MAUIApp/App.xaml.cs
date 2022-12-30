namespace AirPodsConnect.Interface
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.MaximumHeight = 423;
            window.MinimumHeight = 423;
            window.Height = 432;

            window.Width = 337;
            window.MinimumWidth = 337;
            window.MaximumWidth = 337;

            return window;
        }
    }
}