using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCMARKMANAGE.Models
{
    public class Users
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Types Ctypes { get; set; }
        
    }
    public enum Types
    {
        Select,
        Admin,
        Faculty,
        Student,
        

    }
}