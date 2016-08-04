using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vru.Models.Abstract;

namespace vru.Models
{
    public class Service : UpdateModel
    {
        public int Id { get; set; }

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