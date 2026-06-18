using atlasApp.Models;
using atlasApp.Services;
using System.Net.Http.Json;

namespace atlasApp;

public partial class DashboardPage : ContentPage
{
    // Pagination
    private int currentPage = 1;
    private const int pageSize = 5;

    // Store all posts
    private List<Post> allPosts = new();

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

    // Load all posts from API
    private async Task LoadPosts()
    {
        try
        {
            using HttpClient client = new();

            var posts = await client.GetFromJsonAsync<List<Post>>(
                $"{ApiConfig.BaseUrl}api/posts");

            if (posts != null)
            {
                // Latest first
                allPosts = posts
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                ShowCurrentPage();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // Show current page posts
    private void ShowCurrentPage()
    {
        PostsContainer.Children.Clear();

        var currentPosts = allPosts
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        foreach (var post in currentPosts)
        {
            CreatePostCard(post);
        }

        UpdatePaginationButtons();
    }

    // Create expandable card
    private void CreatePostCard(Post post)
    {
        bool expanded = false;

        // Title
        var title = new Label
        {
            Text = post.Title,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black
        };

        // Description preview
        string shortDescription =
            post.Description.Length > 50
                ? post.Description.Substring(0, 50) + "..."
                : post.Description;

        var description = new Label
        {
            Text = shortDescription,
            FontSize = 14,
            TextColor = Colors.Black
        };

        // Posted by
        var author = new Label
        {
            Text = $"Posted By {post.CreatedByName}",
            FontSize = 12,
            TextColor = Colors.Gray
        };

        // Date + time
        var date = new Label
        {
            Text = post.CreatedDate
        .ToString("h:mm tt • dd-MMM-yyyy"),
            FontSize = 12,
            TextColor = Colors.DarkGray
        };
        var contentStack = new VerticalStackLayout
        {
            Spacing = 5,
            Children =
            {
                title,
                description,
                author,
                date
            }
        };

        var card = new Frame
        {
            CornerRadius = 15,
            Padding = 15,
            Margin = new Thickness(0, 5),
            BackgroundColor = Colors.White,
            HasShadow = true,
            Content = contentStack
        };

        // Expand / Collapse logic

        var tap = new TapGestureRecognizer();

        tap.Tapped += async (s, e) =>
        {
            expanded = !expanded;

            description.Text = expanded
                ? post.Description
                : (post.Description.Length > 50
                    ? post.Description.Substring(0, 50) + "..."
                    : post.Description);

            card.BackgroundColor = expanded
                ? Color.FromArgb("#EFF6FF")
                : Colors.White;

            
        };

        card.GestureRecognizers.Add(tap);

        PostsContainer.Children.Add(card);
    }

    // Previous button
    private void Previous_Clicked(object sender, EventArgs e)
    {
        if (currentPage > 1)
        {
            currentPage--;
            ShowCurrentPage();
        }
    }

    // Next button
    private void Next_Clicked(object sender, EventArgs e)
    {
        if ((currentPage * pageSize) < allPosts.Count)
        {
            currentPage++;
            ShowCurrentPage();
        }
    }

    // Enable / Disable pagination buttons
    private void UpdatePaginationButtons()
    {
        if (btnPrevious != null)
            btnPrevious.IsEnabled = currentPage > 1;

        if (btnNext != null)
            btnNext.IsEnabled =
                (currentPage * pageSize) < allPosts.Count;
    }
}