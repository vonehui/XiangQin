using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiangQin.Models
{
    public partial class AD
    {
        [Required(ErrorMessage = "该项不能为空")]
        public int aId { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string aTitle { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string aPicUrl { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string aLinkUrl { get; set; }
    }
}