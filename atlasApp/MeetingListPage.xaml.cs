using atlasApp.Models;
using System.Net.Http.Json;
namespace atlasApp;
public partial class MeetingListPage : ContentPage
{
    private readonly HttpClient client = new();

    private readonly string ApiUrl =
        "http://192.168.1.200:5223/api/meeting";

    public MeetingListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadMeetings();
    }

    // refresh

    private async void RefreshControl_Refreshing(
        object sender,
        EventArgs e)
    {
        await LoadMeetings();

        RefreshControl.IsRefreshing = false;
    }

    // create page

    private async void AddMeeting_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PushAsync(
            new MeetingPage());
    }

    // load meetings

    private async Task LoadMeetings()
    {
        try
        {
            Loader.IsVisible = true;
            Loader.IsRunning = true;

            MeetingContainer.Children.Clear();

            var meetings =
                await client.GetFromJsonAsync<
                    List<MeetingDto>>(ApiUrl);

            if (meetings == null)
                return;

            foreach (var meeting in meetings)
            {
                CreateMeetingCard(meeting);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
        finally
        {
            Loader.IsVisible = false;
            Loader.IsRunning = false;
        }
    }

    // create card

    private void CreateMeetingCard(
        MeetingDto meeting)
    {
        var company = new Label
        {
            Text = meeting.TM_CompanyName,
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black
        };

        var location = new Label
        {
            Text = $"📍 {meeting.TM_Location}",
            FontSize = 14,
            TextColor = Colors.Gray
        };

        var contact = new Label
        {
            Text = $"👤 {meeting.TM_ContactPerson}",
            FontSize = 14,
            TextColor = Colors.Gray
        };

        var date = new Label
        {
            Text =
                $"📅 {meeting.TM_MeetingDate:dd-MMM-yyyy}",
            FontSize = 13,
            TextColor = Colors.DarkGray
        };

        // delete button

        var deleteBtn = new Button
        {
            Text = "Delete",
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 40
        };

        deleteBtn.Clicked += async (s, e) =>
        {
            bool confirm =
                await DisplayAlertAsync(
                    "Delete",
                    "Delete this meeting?",
                    "Yes",
                    "No");

            if (!confirm)
                return;

            await DeleteMeeting(meeting.TM_Id);
        };

        var card = new Frame
        {
            CornerRadius = 18,
            Padding = 15,
            BackgroundColor = Colors.White,
            HasShadow = true,

            Content = new VerticalStackLayout
            {
                Spacing = 8,

                Children =
                {
                    company,
                    location,
                    contact,
                    date,
                    deleteBtn
                }
            }
        };

        MeetingContainer.Children.Add(card);
    }

    // delete api

    private async Task DeleteMeeting(int id)
    {
        try
        {
            var response =
                await client.DeleteAsync(
                    $"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlertAsync(
                    "Success",
                    "Deleted",
                    "OK");

                await LoadMeetings();
            }
            else
            {
                await DisplayAlertAsync(
                    "Error",
                    "Delete failed",
                    "OK");
            }
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