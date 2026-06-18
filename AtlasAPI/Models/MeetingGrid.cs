using System.ComponentModel.DataAnnotations;

namespace AtlasAPI.Models
{
    public class MeetingGrid
    {

        [Key]
        public int TMG_Id { get; set; }

        public int TMG_TM_Id { get; set; }

        public string TMG_TradeType { get; set; }
        public string TMG_TransportMode { get; set; }
        public string TMG_VolumeYear { get; set; }

        public decimal? TMG_Nomination { get; set; }
        public decimal? TMG_Freehand { get; set; }

        public Meeting Meeting { get; set; }
    }
}
