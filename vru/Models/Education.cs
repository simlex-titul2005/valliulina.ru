using SX.WebCore;
using SX.WebCore.Abstract;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace vru.Models
{
    [Table("D_EDUCATION")]
    public class Education : SxDbUpdatedModel<int>
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string GroupName { get; set; }

        public string Html { get; set; }

        public virtual SxPicture Picture { get; set; }
        public Guid? PictureId { get; set; }
    }
}