using DBBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using XiangQin.Models;

namespace XiangQin.BLL
{
    public class ADBLL
    {
        public List<AD> getTopAD(int n)
        {
            DataTable adTable = DBHelper.QurySql(string.Format("select TOP {0} * from AD",n));
            List<AD> tempList = ModelConvertHelper<AD>.ConvertToModel(adTable).ToList();
            if (tempList.Count==0||tempList==null)
            {
                return null;
            }
            if (tempList.Count==n)
            {
                return tempList;
            }
            if (tempList.Count>n)
            {
                return tempList.Take(n).ToList();
            }
            if (tempList.Count<n)
            {
                int tempCont =n-tempList.Count;
                for (int i = 0; i < tempCont; i++)
                {
                    tempList.Add(tempList[i]);
                }
                return tempList;
            }
            return null;
            
        }
        public List<AD> getADList()
        {
            DataTable adTable = DBHelper.QurySql(string.Format("select * from AD"));
            return ModelConvertHelper<AD>.ConvertToModel(adTable).ToList();
        }
        public AD getAD(int id)
        {
            DataTable adTable = DBHelper.QurySql(string.Format("select * from AD where aId={0}",id));
            return ModelConvertHelper<AD>.ConvertToModel(adTable).ToList()[0];
        }
        public bool EditAD(AD ad)
        {
            int n = DBHelper.NonQuery(string.Format("UPDATE AD SET aTitle='{0}',aPicUrl='{1}',aLinkUrl='{2}' where aId={3}", ad.aTitle, ad.aPicUrl, ad.aLinkUrl,ad.aId));
            return n == 1;
        }
        public bool deleteAD(int id)
        {
            string sql = string.Format("delete from AD where aId={0}", id);
            int n = DBHelper.NonQuery(sql);
            return n == 1;
        }
        public bool createAD(AD ad)
        {
            string sql = string.Format("INSERT INTO AD(aTitle,aPicUrl,aLinkUrl) values('{0}','{1}','{2}')",ad.aTitle,ad.aPicUrl,ad.aLinkUrl);
            int n = DBHelper.NonQuery(sql);
            return n == 1;
        }

    }
}