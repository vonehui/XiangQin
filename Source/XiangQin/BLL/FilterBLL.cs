using DBBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using XiangQin.Models;

namespace XiangQin.BLL
{
    public class FilterBLL
    {
        public List<BaseUserInfo> getFiltedList(FilterUser u)
        {
            int[] uAgeScope={0,120};//
            int[] uHeightScope ={0,250};//
            string uMoneyScope = u.uMoneyScope;
            string uAddressScope = u.uAddressScope;
            string uEduScope = u.uEduScope;
            #region switch (uAgeScope)获取最大和最小年龄
            switch (u.uAgeScope)
            {
                case "不限":
                    uAgeScope[0] = 0;
                    uAgeScope[1] = 120;
                    break;
                case "18-25岁":
                    uAgeScope[0] = 18;
                    uAgeScope[1] = 25;
                    break;
                case "26-35岁":
                    uAgeScope[0] = 26;
                    uAgeScope[1] = 35;
                    break;
                case "36-45岁":
                    uAgeScope[0] = 36;
                    uAgeScope[1] = 45;
                    break;
                case "46-55岁":
                    uAgeScope[0] = 46;
                    uAgeScope[1] = 55;
                    break;
                case "55岁以上":
                    uAgeScope[0] = 55;
                    uAgeScope[1] = 120;
                    break;
            }
            #endregion
            #region switch (u.uHeightScope)获取最大和最小身高
            switch (u.uHeightScope)
            {
                case "不限":
                    uHeightScope[0] = 0;
                    uHeightScope[1] = 250;
                    break;
                case "160cm以下":
                    uHeightScope[0] = 0;
                    uHeightScope[1] = 160;
                    break;
                case "161-165cm":
                    uHeightScope[0] = 161;
                    uHeightScope[1] = 165;
                    break;
                case "166-170cm":
                    uHeightScope[0] = 166;
                    uHeightScope[1] = 170;
                    break;
                case "170-180cm":
                    uHeightScope[0] = 170;
                    uHeightScope[1] = 180;
                    break;
                case "180cm以上":
                    uHeightScope[0] = 180;
                    uHeightScope[1] = 250;
                    break;
            }
            #endregion
            string sql = "select * from BaseUserInfo where ";
            int[] uYearScope={DateTime.Now.Year-uAgeScope[0]-1,DateTime.Now.Year-uAgeScope[1]-1};
            string uAgeScopeSql = string.Format(" uYear<={0} and uYear>={1} and ", uYearScope[0],uYearScope[1]);
            sql += uAgeScopeSql;
            string uHeightScopeSql = string.Format(" uHeight>={0} and uHeight<={1} ", uHeightScope[0], uHeightScope[1]);
            sql += uHeightScopeSql;
            if (uMoneyScope!="不限")
            {
                string uMoneyScopeSql = string.Format(" and uMoney='{0}' ", uMoneyScope);
                sql += uMoneyScopeSql;
            }
            if (uAddressScope != "不限")
            {
                string uAddressScopeSql = string.Format(" and uAddress like'%{0}%' ", uAddressScope);
                sql += uAddressScopeSql;
            }
            if (uEduScope != "不限")
            {
                string uEduScopeSql = string.Format(" and uEdu='{0}' ", uEduScope);
                sql += uEduScopeSql;
            }
            DataTable userTable = DBHelper.QurySql(sql);
            return ModelConvertHelper<BaseUserInfo>.ConvertToModel(userTable).ToList();
        
        }
    }
}