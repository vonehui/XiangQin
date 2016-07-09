using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data.OleDb;
using System.Web;

    namespace DBBase
    {
        public class DBHelper
        {
            private static DBHelper theInstance = new DBHelper();
            private string strConn = null;
            public DBHelper()
            {
                //
                // TODO: 在此处添加构造函数逻辑
                //
                strConn = string.Format("provider=microsoft.jet.oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("~/XiangQin.mdb") + ";Persist Security Info=False");
            }

            public static OleDbConnection GetNewConn()
            {
                OleDbConnection myConn = new OleDbConnection(theInstance.strConn);
                myConn.Open();
                return myConn;
            }

            /// <summary>
            /// 无需返回的SQL执行
            /// </summary>
            /// <param name="OleDbCommand"></param>
            public static int NonQuery(string OleDbCommand)
            {
                using (OleDbConnection myConn = GetNewConn())
                {
                    OleDbCommand _OleDbCommand = new OleDbCommand(OleDbCommand, myConn);
                    return _OleDbCommand.ExecuteNonQuery();
                }

            }

            /// <summary>
            /// 返回记录集的SQL执行
            /// </summary>
            /// <param name="OleDbCommand"></param>
            /// <returns></returns>
            public static DataTable QurySql(string OleDbCommand)
            {

                using (OleDbConnection myConn = GetNewConn())
                {

                    OleDbCommand _OleDbCommand = new OleDbCommand(OleDbCommand, myConn);
                    OleDbDataAdapter _OleDbDataAdapter = new OleDbDataAdapter(_OleDbCommand);

                    DataTable _dt = new DataTable("returnTable");
                    _OleDbDataAdapter.Fill(_dt);

                    return _dt;
                }

            }
            /// <summary>
            /// 执行SQL返回DataSet
            /// </summary>
            /// <param name="OleDbCommand"></param>
            /// <returns></returns>
            public static DataSet QurySqlToDS(string OleDbCommand)
            {
                using (OleDbConnection myConn = GetNewConn())
                {
                    OleDbCommand _OleDbCommand = new OleDbCommand(OleDbCommand, myConn);
                    OleDbDataAdapter _OleDbDataAdapter = new OleDbDataAdapter(_OleDbCommand);

                    DataSet _ds = new DataSet("returnDataSet");
                    _OleDbDataAdapter.Fill(_ds);

                    return _ds;
                }
            }
            /// <summary>
            /// 返回第一行的一列的值
            /// </summary>
            /// <param name="OleDbCommand"></param>
            /// <returns></returns>
            public static object SalarQury(string OleDbCommand)
            {

                using (OleDbConnection myConn = GetNewConn())
                {

                    OleDbCommand _OleDbCommand = new OleDbCommand(OleDbCommand, myConn);

                    object tempObj = _OleDbCommand.ExecuteScalar();


                    return tempObj;
                }

            }
            /// <summary>
            /// 构造OleDbCommand
            /// </summary>
            /// <param name="spp"></param>
            /// <returns></returns>
            private static OleDbCommand GetSqlCmd(SPParam spp)
            {

                OleDbCommand _OleDbCommand = new OleDbCommand();
                _OleDbCommand.CommandType = CommandType.StoredProcedure;
                _OleDbCommand.CommandText = spp.m_SpName;

                foreach (string Pname in spp.m_Param.Keys)
                {
                    SqlParameter myParam = new SqlParameter(Pname, ((ParamObj)spp.m_Param[Pname]).PValue);
                    myParam.Direction = (ParameterDirection)(((ParamObj)spp.m_Param[Pname]).PDirection);
                    _OleDbCommand.Parameters.Add(myParam);
                }

                return _OleDbCommand;

            }

            /// <summary>
            /// 执行存储过程，并返回表
            /// </summary>
            public static DataTable QuerySP(SPParam spp)
            {
                using (OleDbConnection myConn = GetNewConn())
                {
                    OleDbCommand _OleDbCommand = GetSqlCmd(spp);
                    _OleDbCommand.Connection = myConn;
                    OleDbDataAdapter _OleDbDataAdapter = new OleDbDataAdapter(_OleDbCommand);

                    DataTable _dt = new DataTable("returnTable");
                    _OleDbDataAdapter.Fill(_dt);

                    return _dt;
                }
            }

            /// <summary>
            /// 执行存储过程，并返回表
            /// </summary>
            public static DataTable QuerySP(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return QuerySP(temp);
            }

            /// <summary>
            /// 执行存储过程,返回DataSet
            /// </summary>
            /// <param name="spp"></param>
            /// <returns></returns>
            public static DataSet QuerySPtoDataSet(SPParam spp)
            {
                using (OleDbConnection myConn = GetNewConn())
                {
                    OleDbCommand _OleDbCommand = GetSqlCmd(spp);
                    _OleDbCommand.Connection = myConn;
                    OleDbDataAdapter _OleDbDataAdapter = new OleDbDataAdapter(_OleDbCommand);

                    DataSet _ds = new DataSet("returnDataSet");
                    _OleDbDataAdapter.Fill(_ds);

                    return _ds;
                }
            }

            /// <summary>
            /// 执行存储过程,返回DataSet
            /// </summary>
            /// <param name="spp"></param>
            /// <returns></returns>
            public static DataSet QuerySPtoDataSet(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return QuerySPtoDataSet(temp);
            }

            /// <summary>
            /// 执行存储过程，并返回受影响的行数
            /// </summary>
            public static int NonQuerySP(SPParam spp)
            {
                try
                {
                    using (OleDbConnection myConn = GetNewConn())
                    {
                        OleDbCommand _OleDbCommand = GetSqlCmd(spp);
                        _OleDbCommand.Connection = myConn;
                        return _OleDbCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("sd", ex);
                }
            }

            /// <summary>
            /// 执行存储过程，返回受影响的行数(有返回参数)
            /// </summary>
            public static int NonQuerySPR(SPParam spp)
            {
                using (OleDbConnection myConn = GetNewConn())
                {
                    OleDbCommand _OleDbCommand = GetSqlCmd(spp);
                    _OleDbCommand.Connection = myConn;
                    int result = _OleDbCommand.ExecuteNonQuery();

                    foreach (SqlParameter pobj in _OleDbCommand.Parameters)
                    {
                        spp[pobj.ParameterName] = pobj.Value;
                    }

                    return result;
                }
            }


            /// <summary>
            /// 执行存储过程，并返回受影响的行数
            /// </summary>
            public static int NonQuerySP(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return NonQuerySP(temp);
            }

            /// <summary>
            /// 执行存储过程，并返回第一行第一单元的值
            /// </summary>
            public static object ScalarSP(SPParam spp)
            {
                using (OleDbConnection myConn = GetNewConn())
                {
                    OleDbCommand _OleDbCommand = GetSqlCmd(spp);
                    _OleDbCommand.Connection = myConn;
                    return _OleDbCommand.ExecuteScalar();
                }
            }

            /// <summary>
            /// 执行存储过程，并返回第一行第一单元的值
            /// </summary>
            public static object ScalarSP(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return ScalarSP(temp);
            }
        }
        /// <summary>
        /// 参数方向
        /// </summary>
        public enum ParamDirection
        {
            /// <summary>
            /// 输入参数
            /// </summary>
            Input = 1,
            /// <summary>
            /// 输入输出参数
            /// </summary>
            InputOutput = 3,
            /// <summary>
            /// 输出参数
            /// </summary>
            Output = 2,
            /// <summary>
            /// 返回值
            /// </summary>
            ReturnValue = 6

        }

        /// <summary>
        /// 参数结构
        /// </summary>
        public struct ParamObj
        {
            /// <summary>
            /// 值
            /// </summary>
            public object PValue;
            /// <summary>
            /// 方向
            /// </summary>
            public ParamDirection PDirection;

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="pvalue">值</param>
            /// <param name="pDirection">参数方向</param>
            public ParamObj(object pvalue, ParamDirection pDirection)
            {
                PValue = pvalue;
                PDirection = pDirection;
            }
        }

        /// <summary>
        /// 存储过程参数容器
        /// </summary>
        public class SPParam
        {
            /// <summary>
            /// 存储过程名
            /// </summary>
            public string m_SpName;
            /// <summary>
            /// 参数容器
            /// </summary>
            internal Hashtable m_Param;

            /// <summary>
            /// 参数容器构造
            /// </summary>
            /// <param name="SPName">要操作的存储过程名</param>
            public SPParam(string SPName)
            {
                m_Param = new Hashtable();

                m_SpName = SPName;
            }

            /// <summary>
            /// 参数容器构造，使用这个重载可以获得更好性能
            /// </summary>
            /// <param name="SPName">要操作的存储过程名</param>
            /// <param name="ParamCount">参数的个数</param>
            public SPParam(string SPName, int ParamCount)
            {
                m_Param = new Hashtable(ParamCount);

                m_SpName = SPName;
            }

            /// <summary>
            /// 参数索引器
            /// </summary>
            public object this[string ParamName]
            {
                get
                {
                    return ((ParamObj)m_Param[ParamName]).PValue;
                }
                set
                {
                    ParamObj myParam = new ParamObj(value, ParamDirection.Input);
                    if (value != null)
                    {
                        m_Param[ParamName] = myParam;
                    }

                }
            }

            /// <summary>
            /// 添加非输入参数
            /// </summary>
            /// <param name="ParamName">参数名</param>
            /// <param name="pDirection">参数方向</param>
            public void addParam(string ParamName, ParamDirection pDirection)
            {
                ParamObj myParam = new ParamObj(null, pDirection);
                m_Param[ParamName] = myParam;
            }
        }
        /// <summary>
        /// 数据转换类
        /// </summary>
        public class Dc
        {
            /// <summary>
            /// 转换数据到Int32,如果为DbNull则返回0
            /// </summary>
            static public int ToInt32(object obj)
            {
                if (DBNull.Value.Equals(obj))
                {
                    return 0;
                }
                else
                {
                    return (int)obj;
                }
            }

            /// <summary>
            /// 转换数据到String,如果为DbNull则返回null
            /// </summary>
            static public string ToString(object obj)
            {
                if (DBNull.Value.Equals(obj))
                {
                    return null;
                }
                else
                {
                    return (string)obj;
                }
            }

            /// <summary>
            /// 转换数据到Decimal,如果为DbNull则返回0
            /// </summary>
            static public decimal ToDecimal(object obj)
            {
                if (DBNull.Value.Equals(obj))
                {
                    return 0;
                }
                else
                {
                    return (decimal)obj;
                }
            }

            /// <summary>
            /// 转换数据到DateTime,如果为DbNull则返回1900-01-01
            /// </summary>
            static public DateTime ToDateTime(object obj)
            {
                if (DBNull.Value.Equals(obj))
                {
                    return new DateTime(1900, 1, 1, 0, 0, 0);
                }
                else
                {
                    return (DateTime)obj;
                }
            }

            /// <summary>
            /// 转换数据到bool,如果为DbNull则返回false
            /// </summary>
            static public bool ToBoolean(object obj)
            {
                if (DBNull.Value.Equals(obj))
                {
                    return false;
                }
                else
                {
                    return (bool)obj;
                }
            }

        }
    }
