using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace vru.Models
{
    public class User : IdentityUser
    {
        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        [MaxLength(100)]
        public string NikName { get; set; }

        public virtual Picture Avatar { get; set; }
        public Guid? AvatarId { get; set; }
    }
}