using SX.WebCore.DbModels.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vru.Models
{
    [Table("D_SERVICE")]
    public class Service : SxDbUpdatedModel<int>
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required, MaxLength(255), Index]
        public string TitleUrl { get; set; }

        [Required]
        public string Html { get; set; }

        [MaxLength(100)]
        public string Duration { get; set; }

        [MaxLength(100)]
        public string Cost { get; set; }
    }
}