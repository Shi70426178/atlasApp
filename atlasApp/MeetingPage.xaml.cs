using atlasApp.Models;
using atlasApp.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace atlasApp;

public partial class MeetingPage : ContentPage
{
    private readonly HttpClient client = new();

    private readonly string ApiUrl =
        $"{ApiConfig.BaseUrl}api/meeting/create";

    // store dynamic rows
    private readonly List<GridRowModel> meetingGrids = new();

    public MeetingPage()
    {
        InitializeComponent();

        // first row auto add
        AddGridRow_Clicked(null, EventArgs.Empty);
    }

    // helper class INSIDE page
    private class GridRowModel
    {
        public Picker TradePicker { get; set; }
        public Picker TransportPicker { get; set; }
        public Picker YearPicker { get; set; }
        public Entry NominationEntry { get; set; }
        public Entry FreehandEntry { get; set; }
    }

    private decimal GetDecimal(string? value)
    {
        return decimal.TryParse(value, out var result)
            ? result
            : 0;
    }

    // Add dynamic row
    private void AddGridRow_Clicked(object sender, EventArgs e)
    {
        var tradePicker = new Picker { Title = "Trade Type" };
        tradePicker.Items.Add("Export");
        tradePicker.Items.Add("Import");

        var transportPicker = new Picker { Title = "Transport Mode" };
        transportPicker.Items.Add("Sea");
        transportPicker.Items.Add("Air");

        var yearPicker = new Picker { Title = "Year" };
        yearPicker.Items.Add("2023-24");
        yearPicker.Items.Add("2024-25");
        yearPicker.Items.Add("2025-26");

        var txtNomination = new Entry
        {
            Placeholder = "Nomination",
            Keyboard = Keyboard.Numeric
        };

        var txtFreehand = new Entry
        {
            Placeholder = "Freehand",
            Keyboard = Keyboard.Numeric
        };

        var rowFrame = new Frame
        {
            CornerRadius = 12,
            Padding = 10,
            Margin = new Thickness(0, 5),
            BackgroundColor = Colors.White,

            Content = new VerticalStackLayout
            {
                Spacing = 8,
                Children =
                {
                    tradePicker,
                    transportPicker,
                    yearPicker,
                    txtNomination,
                    txtFreehand
                }
            }
        };

        GridContainer.Children.Add(rowFrame);

        meetingGrids.Add(new GridRowModel
        {
            TradePicker = tradePicker,
            TransportPicker = transportPicker,
            YearPicker = yearPicker,
            NominationEntry = txtNomination,
            FreehandEntry = txtFreehand
        });
    }

    // Save meeting
    private async void SaveMeeting_Clicked(object sender, EventArgs e)
    {
        try
        {
            // basic validation
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                await DisplayAlertAsync(
                    "Validation",
                    "Company Name is required",
                    "OK");
                return;
            }
            
            var dto = new MeetingDto
            {
                TM_MeetingDate = dpMeetingDate.Date,

                TM_MeetingTime = tpMeetingTime.Time,
                TM_Location =
                    txtLocation.Text ?? "",

                TM_CompanyName =
                    txtCompanyName.Text ?? "",

                TM_City =
                    txtCity.Text ?? "",

                TM_Country =
                    txtCountry.Text ?? "",

                TM_ContactPerson =
                    txtContactPerson.Text ?? "",

                TM_OurRepresentative =
                    txtOurRepresentative.Text ?? "",

                TM_RequestedBy =
                    txtRequestedBy.Text ?? "",

                TM_CompanyBrief =
                    txtCompanyBrief.Text ?? "",

                TM_IsWorking =
                    chkWorking.IsChecked,

                TM_IndiaBusiness =
                    chkIndiaBusiness.IsChecked,

                TM_NepalBusiness =
                    chkNepalBusiness.IsChecked,

                TM_AgentsCoreStrength =
                    txtAgentsCoreStrength.Text ?? "",

                TM_OwnLCL =
                    txtOwnLCL.Text ?? "",

                TM_MajorTradeLanes =
                    txtMajorTradeLanes.Text ?? "",

                TM_AirPercentage =
                    GetDecimal(txtAirPercentage.Text),

                TM_SeaPercentage =
                    GetDecimal(txtSeaPercentage.Text),

                MeetingGrids =
                    new List<MeetingGridDto>()
            };

            // collect dynamic rows
            foreach (var row in meetingGrids)
            {
                dto.MeetingGrids.Add(
                    new MeetingGridDto
                    {
                        TMG_TradeType =
                            row.TradePicker
                               .SelectedItem?.ToString() ?? "",

                        TMG_TransportMode =
                            row.TransportPicker
                               .SelectedItem?.ToString() ?? "",

                        TMG_VolumeYear =
                            row.YearPicker
                               .SelectedItem?.ToString() ?? "",

                        TMG_Nomination =
                            GetDecimal(
                                row.NominationEntry.Text),

                        TMG_Freehand =
                            GetDecimal(
                                row.FreehandEntry.Text)
                    });
            }

            var options =
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                };

            var json =
                JsonSerializer.Serialize(dto, options);

            var content = new StringContent(json);
            content.Headers.ContentType =
    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            content.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");

            var response =
                await client.PostAsync(
                    ApiUrl,
                    content);

            var result =
                await response.Content
                    .ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlertAsync(
                    "Success",
                    "Meeting saved successfully",
                    "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlertAsync(
                    "API Error",
                    result,
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