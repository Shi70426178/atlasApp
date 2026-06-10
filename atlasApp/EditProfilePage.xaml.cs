namespace atlasApp;

public partial class EditProfilePage : ContentPage
{
    public EditProfilePage()
    {
        InitializeComponent();

        txtUserName.Text = UserSession.UserName;
        txtEmail.Text = UserSession.Email;
        txtBranch.Text = UserSession.Branch;
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        UserSession.UserName = txtUserName.Text;
        UserSession.Email = txtEmail.Text;
        UserSession.Branch = txtBranch.Text;

        await DisplayAlert(
            "Success",
            "Profile Updated",
            "OK");

        await Navigation.PopAsync();
    }
}