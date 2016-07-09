using DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiangQin.Models;

namespace XiangQin.BLL
{
    public class AdminBLL
    {
        public bool Login(string u, string p)
        {
            string sql = string.Format("select aId from Admin where aUserName='{0}' and aPassword='{1}'", u, p);
            object n=DBHelper.SalarQury(sql);
            return n!=null;
        }
        public bool editAdmin(ChangeAdmin admin)
        {
            int n = DBHelper.NonQuery(string.Format("UPDATE Admin SET aPassword='{0}' where aUserName='{1}'",admin.aNewPassword,admin.aUserName));
            return n == 1;
        }
    }
}