using RestaurantPosMaui.Data;

namespace RestaurantPosMaui
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;
        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            MainPage = new AppShell();

            // initialize and seed database
            Task.Run(async () => await databaseService.InitilizeDatabaseAsync())
                .GetAwaiter().
                GetResult();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.MinimumHeight = 800;
            window.MinimumWidth = 1320;

            return window;
        }
    }
}
