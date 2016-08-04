using System;
using System.ComponentModel.DataAnnotations;

namespace vru.Models.Abstract
{
    public abstract class UpdateModel : CreateModel
    {
        [DataType(DataType.DateTime)]
        public DateTime DateUpdate { get; set; }
    }
}