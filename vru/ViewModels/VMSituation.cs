using System.ComponentModel.DataAnnotations;

namespace vru.ViewModels
{
    public sealed class VMSituation
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Поле обязательно для заполнения"), Display(Name ="Описание"), DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}