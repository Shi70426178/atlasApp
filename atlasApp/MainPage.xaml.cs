using System.Text;
using System.Text.Json;
using Xamarin.Android.Net;

namespace atlasApp;

public partial class MainPage : ContentPage
{
    HttpClient client = new HttpClient(
     new AndroidMessageHandler());

    string baseUrl = "http://192.168.1.198:5223/api/auth/";

    public MainPage()
    {
        InitializeComponent();
    }

    // Open Signup Page
    private async void OnSignupClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
    // Login API
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            var user = new
            {
                Email = txtUserId.Text ?? "",
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

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlertAsync("Success", result, "OK");
            }
            else
            {
                await DisplayAlertAsync("Login Failed", result, "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "ERROR",
                ex.ToString(),
                "OK");
        }


    }
}