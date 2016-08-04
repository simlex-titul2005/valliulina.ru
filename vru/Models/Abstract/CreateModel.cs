using System;
using System.ComponentModel.DataAnnotations;

namespace vru.Models.Abstract
{
    public abstract class CreateModel
    {
        [DataType(DataType.DateTime)]
        public DateTime DateCreate { get; set; }
    }
}