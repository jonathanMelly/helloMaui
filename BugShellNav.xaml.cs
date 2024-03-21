namespace HelloMaui1;

public partial class BugShellNav : ContentPage
{
    public BugShellNav()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new BugShellNav());
    }
}