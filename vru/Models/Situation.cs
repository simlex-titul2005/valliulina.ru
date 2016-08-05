using SX.WebCore.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vru.Models
{
    [Table("D_SITUATION")]
    public class Situation : SxDbUpdatedModel<int>
    {
        [Required]
        public string Text { get; set; }
    }
}