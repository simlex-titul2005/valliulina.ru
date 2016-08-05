using SX.WebCore.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace vru.ViewModels
{
    public sealed class VMEducation
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }

        [Display(Name = "Месяц")]
        public int Month { get; set; }

        public string GroupName { get; set; }

        [AllowHtml, Display(Name ="Описание"), Required(ErrorMessage = "Поле обязательно для заполнения"), DataType(DataType.MultilineText)]
        public string Html { get; set; }

        public SxVMPicture Picture { get; set; }

        [Display(Name ="Картинка"), UIHint("PicturesLookupGrid")]
        public Guid? PictureId { get; set; }
    }
}