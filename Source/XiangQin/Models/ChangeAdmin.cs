using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiangQin.Models
{
    public class ChangeAdmin:Admin
    {
        
        [Required(ErrorMessage = "新密码不能为空")]
        public string aNewPassword { get; set; }
        [Required(ErrorMessage = "新密码不能为空")]
        public string aReNewPassword { get; set; }
    }
}