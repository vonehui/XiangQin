using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiangQin.Models
{
    public partial class Admin
    {
        public int aId { get; set; }
         [Required(ErrorMessage = "用户名不能为空")]
        public string aUserName { get; set; }
         [Required(ErrorMessage = "密码不能为空")]
        public string aPassword { get; set; }
    }
}