namespace atlasApp;

using atlasApp.Models;
using atlasApp.Services;
using System.Net.Http.Json;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
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

        await LoadPosts();
    }
    private void Menu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void CreatePost_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CreatePostPage());
    }

    private async Task LoadPosts()
    {
        try
        {
            using HttpClient client = new();

            var posts = await client.GetFromJsonAsync<List<Post>>(
                $"{ApiConfig.BaseUrl}api/posts");

            PostsContainer.Children.Clear();

            foreach (var post in posts)
            {
                var title = new Label
                {
                    Text = post.Title,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Black
                };

                var description = new Label
                {
                    Text = post.Description,
                    TextColor = Colors.Black
                };

                var author = new Label
                {
                    Text = $"Posted By {post.CreatedByName}",
                    FontSize = 12,
                    TextColor = Colors.Gray
                };

                var card = new Frame
                {
                    CornerRadius = 15,
                    Padding = 15,
                    BackgroundColor = Colors.White,
                    Content = new VerticalStackLayout
                    {
                        Children =
        {
            title,
            description,
            author
        }
                    }
                };

                PostsContainer.Children.Add(card);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }
}