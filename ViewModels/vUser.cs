using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AttendanceMgmt.ViewModels
{
    public class vUser
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail format is not valid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required]
        public int UserTypeId { get; set; }

        [Required]
        public string Address { get; set; }

        
        public byte[] ProfilePhoto { get; set; }

    }
}