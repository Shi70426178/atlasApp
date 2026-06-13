using System.ComponentModel.DataAnnotations.Schema;

namespace AtlasAPI.Models
{
    [Table("tblPosts")]
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}