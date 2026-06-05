namespace atlasApp;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    public DashboardPage(string username)
    {
        InitializeComponent();
        lblWelcome.Text = "Welcome " + username;
    }

    private void Menu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}