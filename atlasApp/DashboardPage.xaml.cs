namespace atlasApp;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lblWelcome.Text = UserSession.UserName;

        int hour = DateTime.Now.Hour;

        if (hour < 12)
            lblGreeting.Text = "Good Morning 👋";
        else if (hour < 17)
            lblGreeting.Text = "Good Afternoon ☀️";
        else
            lblGreeting.Text = "Good Evening 🌙";
    }
    private void Menu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void CreatePost_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CreatePostPage());
    }
}