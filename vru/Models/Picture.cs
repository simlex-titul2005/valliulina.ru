using System;
using System.ComponentModel.DataAnnotations;
using vru.Models.Abstract;

namespace vru.Models
{
    public class Picture : UpdateModel
    {
        public Guid Id { get; set; }

        [MaxLength(50), Required]
        public string ImgFormat { get; set; }

        [Required]
        public byte[] OriginalContent { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        [Required, MaxLength(100)]
        public string Caption { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int Size { get; set; }
    }
}