namespace atlasApp.Models;

public class MeetingGridDto
{
    public string TMG_TradeType { get; set; } = "";

    public string TMG_TransportMode { get; set; } = "";

    public string TMG_VolumeYear { get; set; } = "";

    public decimal TMG_Nomination { get; set; }

    public decimal TMG_Freehand { get; set; }
}