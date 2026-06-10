namespace atlasApp;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lblUserName.Text = "User Name : " + UserSession.UserName;
        lblEmail.Text = "Email : " + UserSession.Email;
        lblRole.Text = "Role : " + UserSession.Role;
        lblBranch.Text = "Branch : " + UserSession.Branch;
    }

    private async void EditProfile_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditProfilePage());
    }

    private async void AboutCompany_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AboutCompanyPage());
    }

    private async void EmailSupport_Clicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(
            "mailto:support@atlas.com");
    }
}