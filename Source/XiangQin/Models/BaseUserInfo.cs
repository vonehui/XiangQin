using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XiangQin.Models
{
    public partial class BaseUserInfo
    {
        //用户基本信息的字段
        [Required(ErrorMessage = "该项不能为空")]
        public int uId { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uName { get; set; }

        [Required(ErrorMessage = "该项不能为空")]
        public int uAge { get; set; }

        [Required(ErrorMessage = "该项不能为空")]
        public int uYear { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public int uMonth { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public int uDate { get; set; }

        [Required(ErrorMessage = "该项不能为空")]
        public string uAddress { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public int uHeight { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uBlood { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uWeight { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uEdu { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uProfession { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uMoney { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uMarriage { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uHouse { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uHobby { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uSpeak { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string uFaceUrl { get; set; }
        //下面为征友条件的字段
        [Required(ErrorMessage = "该项不能为空")]
        public string fAddress { get; set; }
        public string fAge { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string fHeight { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string fEdu { get; set; }
        [Required(ErrorMessage = "该项不能为空")]
        public string fMoney { get; set; }
        public string aRemark { get; set; }
    }
}
