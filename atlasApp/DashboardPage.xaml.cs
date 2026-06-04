namespace atlasApp;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(string username)
    {
        InitializeComponent();

        lblWelcome.Text = "Welcome " + username;
    }
}