using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace vru.ViewModels
{
    public sealed class VMService
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Заголовок не может быть пустым"), MaxLength(255), Display(Name ="Заголовок")]
        public string Title { get; set; }

        [MaxLength(255), Display(Name = "Строковый ключ")]
        public string TitleUrl { get; set; }

        [Required(ErrorMessage = "Описание не может быть пустым"), AllowHtml, DataType(DataType.MultilineText), Display(Name = "Описание")]
        public string Html { get; set; }

        [MaxLength(100, ErrorMessage ="Кол-во знаков поля не должно превышать {1}"), Display(Name = "Продолжительность")]
        public string Duration { get; set; }

        [MaxLength(100, ErrorMessage = "Кол-во знаков поля не должно превышать {1}"), Display(Name = "Стоимость")]
        public string Cost { get; set; }
    }
}