using SX.WebCore.DbModels.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vru.Models
{
    [Table("D_QUESTION")]
    public class Question : SxDbModel<int>
    {
        [MaxLength(100), Required]
        public string Name { get; set; }

        [MaxLength(100), Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }


        [MaxLength(600), Required]
        public string Text { get; set; }

        public bool IsReaded { get; set; }
    }
}