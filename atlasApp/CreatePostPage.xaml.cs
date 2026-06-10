namespace atlasApp;
using atlasApp.Models;
using atlasApp.Services;
using System.Net.Http.Json;

public partial class CreatePostPage : ContentPage
{
    public CreatePostPage()
    {
        InitializeComponent();
    }

    private async void SavePost_Clicked(object sender, EventArgs e)
    {
        try
        {
            var request = new CreatePostRequest
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,

                CreatedBy = UserSession.Id,
                CreatedByName = UserSession.UserName,

                CreatedDate = DateTime.Now
            };

            using HttpClient client = new();

            var response = await client.PostAsJsonAsync(
    $"{ApiConfig.Servers[0]}api/posts/create",
    request);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlertAsync("Success",
                    "Post Saved Successfully",
                    "OK");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();

                await DisplayAlertAsync("API Error",
                    error,
                    "OK");

                return;
            }
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
    }
}