using System.Text;
using System.Text.Json;

namespace atlasApp;

public partial class MainPage : ContentPage
{
    HttpClient client = new HttpClient();

    string baseUrl = "http://192.168.1.204:5223/api/auth/";

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSignupClicked(object sender, EventArgs e)
    {
        try
        {
            var user = new
            {
                Email = txtEmail.Text ?? "",
                Password = txtPassword.Text ?? ""
            };

            var json = JsonSerializer.Serialize(user);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                baseUrl + "signup",
                content);

            var result = await response.Content.ReadAsStringAsync();

            await DisplayAlert("Signup", result, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            var user = new
            {
                Email = txtEmail.Text ?? "",
                Password = txtPassword.Text ?? ""
            };

            var json = JsonSerializer.Serialize(user);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                baseUrl + "login",
                content);

            var result = await response.Content.ReadAsStringAsync();

            await DisplayAlert("Login", result, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}