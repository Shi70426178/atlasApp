using System.Text;
using System.Text.Json;

namespace atlasApp;

public partial class SignupPage : ContentPage
{
    public SignupPage()
    {
        InitializeComponent();
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        var user = new
        {
            UserName = txtUserName.Text,
            Role = txtRole.Text,
            Email = txtEmail.Text,
            PhoneNumber = txtPhone.Text,
            Password = txtPassword.Text,
            Branch = txtBranch.Text
        };

        var json = JsonSerializer.Serialize(user);

        using var client = new HttpClient();

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync(
            "http://192.168.1.204:5223/api/auth/signup",
            content);


        if (response.IsSuccessStatusCode)
        {
            await DisplayAlertAsync(
                "Success",
                "Signup Successful",
                "OK");

            await Navigation.PopAsync();
        }
        else
        {
            var msg = await response.Content.ReadAsStringAsync();

            await DisplayAlertAsync(
                "Error",
                msg,
                "OK");
        }
    }
}