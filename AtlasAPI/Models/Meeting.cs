using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlasAPI.Models
{
    [Table("TblMeetingShi")]

    public class Meeting
    {
        [Key]
        public int TM_Id { get; set; }

        public DateTime? TM_MeetingDate { get; set; }
        public TimeSpan TM_MeetingTime { get; set; }
        public string TM_Location { get; set; }

        public string TM_CompanyName { get; set; }
        public string TM_City { get; set; }
        public string TM_Country { get; set; }

        public string TM_ContactPerson { get; set; }
        public string TM_OurRepresentative { get; set; }
        public string TM_RequestedBy { get; set; }

        public string TM_CompanyBrief { get; set; }

        public bool? TM_IsWorking { get; set; }
        public bool? TM_IndiaBusiness { get; set; }
        public bool? TM_NepalBusiness { get; set; }

        public string TM_AgentsCoreStrength { get; set; }
        public string TM_OwnLCL { get; set; }
        public string TM_MajorTradeLanes { get; set; }

        public decimal? TM_AirPercentage { get; set; }
        public decimal? TM_SeaPercentage { get; set; }

        public int TM_CreatedBy { get; set; }
        public DateTime TM_CreatedOn { get; set; }

        //public List<MeetingGrid> MeetingGrids { get; set; }
        public List<MeetingGrid> MeetingGrids { get; set; } = new List<MeetingGrid>();
    }
}
