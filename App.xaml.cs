namespace HelloMaui1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new NewPage1());
            MainPage = new AppShell();
        }
    }
}
