using DBBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using XiangQin.Models;

namespace XiangQin.BLL
{
    public class BaseUserInfoBLL
    {
        public List<BaseUserInfo> getUserList()
        {
            DataTable userTable = DBHelper.QurySql("select * from BaseUserInfo");
            return ModelConvertHelper<BaseUserInfo>.ConvertToModel(userTable).ToList();
        }
        public BaseUserInfo getUser(int id)
        {
            DataTable userTable = DBHelper.QurySql(string.Format("select * from BaseUserInfo where uId={0}",id));
            if (userTable==null||userTable.Rows.Count==0)
            {
                return new BaseUserInfo() { uName="查无此人"};
            }
            return ModelConvertHelper<BaseUserInfo>.ConvertToModel(userTable).ToList()[0];
        }
        public bool createUser(BaseUserInfo user)
        {
            string sql = string.Format("INSERT INTO BaseUserInfo (uName	,uAge	,uAddress	,uHeight	,uBlood	,uWeight,	uEdu	,uProfession,	uMoney,	uMarriage	,uHouse,	uSpeak,	uFaceUrl	,fAddress	,fAge	,fHeight,	fEdu,	fMoney	,aRemark,uYear,uMonth,uDate,uHobby) VALUES ('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22})", user.uName, user.uAge, user.uAddress, user.uHeight, user.uBlood, user.uWeight, user.uEdu, user.uProfession, user.uMoney, user.uMarriage, user.uHouse, user.uSpeak, user.uFaceUrl, user.fAddress, user.fAge, user.fHeight, user.fEdu, user.fMoney, user.aRemark, user.uYear, user.uMonth, user.uDate,user.uHobby);
            return DBHelper.NonQuery(sql)==1;
        }
        public bool editUser(BaseUserInfo user)
        {

            string sql = string.Format("UPDATE BaseUserInfo SET uName='{0}',uAge={1},uAddress='{2}',uHeight='{3}',uBlood='{4}',uWeight='{5}',uEdu='{6}',uProfession='{7}',uMoney='{8}',uMarriage='{9}',uHouse='{10}',uSpeak='{11}',uFaceUrl='{12}',fAddress='{13}',fAge='{14}',fHeight='{15}',fEdu='{16}',fMoney='{17}',aRemark='{18}',uYear='{19}',uMonth='{20}',uDate='{21}',uHobby='{22}' where uId={23} ", user.uName, user.uAge, user.uAddress, user.uHeight, user.uBlood, user.uWeight, user.uEdu, user.uProfession, user.uMoney, user.uMarriage, user.uHouse, user.uSpeak, user.uFaceUrl, user.fAddress, user.fAge, user.fHeight, user.fEdu, user.fMoney, user.aRemark, user.uYear, user.uMonth, user.uDate,user.uHobby, user.uId);
            int n = DBHelper.NonQuery(sql);
            return  n== 1;
        }
        public bool deleteUser(int id)
        {
            string sql = string.Format("delete from BaseUserInfo where uId={0}", id);
            int n = DBHelper.NonQuery(sql);
            return n == 1;
        }
    }
}