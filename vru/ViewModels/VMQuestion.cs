using System;
using System.ComponentModel.DataAnnotations;

namespace vru.ViewModels
{
    public sealed class VMQuestion
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }

        [MaxLength(100, ErrorMessage = "Количество знаков для поля не должно превышать {1}"), Required(ErrorMessage ="Представьтесь пожалуйста"), Display(Name="Ваше имя")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Невалидный адрес электронной почты")]
        [MaxLength(100, ErrorMessage = "Количество знаков для поля не должно превышать {1}"), Required(ErrorMessage = "Укажите адрес электронной почты"), DataType(DataType.EmailAddress), Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите контактный номер телефона"), DataType(DataType.PhoneNumber), Display(Name = "Номер телефона")]
        public string Phone { get; set; }


        [MaxLength(600, ErrorMessage = "Количество знаков для поля не должно превышать {1}"), Required(ErrorMessage = "Поле обязательно для заполнения"), DataType(DataType.MultilineText), Display(Name = "Текст обращения или заявки")]
        public string Text { get; set; }

        public bool IsReaded { get; set; }
    }
}