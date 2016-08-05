using SX.WebCore.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using static SX.WebCore.Enums;

namespace vru.ViewModels.Abstract
{
    public abstract class VMMaterial
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public DateTime DateOfPublication { get; set; }

        [MaxLength(255), Required]
        public string Title { get; set; }

        [MaxLength(255), Required]
        public string TitleUrl { get; set; }

        public string Html { get; set; }

        [MaxLength(400)]
        public string Foreword { get; set; }

        public ModelCoreType ModelCoreType { get; set; }

        public bool Show { get; set; }

        public Guid? FrontPictureId { get; set; }
        public virtual SxVMPicture FrontPicture { get; set; }

        public bool ShowFrontPictureOnDetailPage { get; set; }

        public int ViewsCount { get; set; }

        public virtual SxVMAppUser User { get; set; }
        [MaxLength(128)]
        public string UserId { get; set; }

        public virtual SxVMSeoTags SeoTags { get; set; }
        public int? SeoTagsId { get; set; }

        public string CategoryId { get; set; }

        public bool IsTop { get; set; }

        [MaxLength(255)]
        public string SourceUrl { get; set; }
    }
}