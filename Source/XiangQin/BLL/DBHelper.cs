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
                // TODO: �ڴ˴���ӹ��캯���߼�
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
            /// ���践�ص�SQLִ��
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
            /// ���ؼ�¼����SQLִ��
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
            /// ִ��SQL����DataSet
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
            /// ���ص�һ�е�һ�е�ֵ
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
            /// ����OleDbCommand
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
            /// ִ�д洢���̣������ر�
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
            /// ִ�д洢���̣������ر�
            /// </summary>
            public static DataTable QuerySP(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return QuerySP(temp);
            }

            /// <summary>
            /// ִ�д洢����,����DataSet
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
            /// ִ�д洢����,����DataSet
            /// </summary>
            /// <param name="spp"></param>
            /// <returns></returns>
            public static DataSet QuerySPtoDataSet(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return QuerySPtoDataSet(temp);
            }

            /// <summary>
            /// ִ�д洢���̣���������Ӱ�������
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
            /// ִ�д洢���̣�������Ӱ�������(�з��ز���)
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
            /// ִ�д洢���̣���������Ӱ�������
            /// </summary>
            public static int NonQuerySP(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return NonQuerySP(temp);
            }

            /// <summary>
            /// ִ�д洢���̣������ص�һ�е�һ��Ԫ��ֵ
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
            /// ִ�д洢���̣������ص�һ�е�һ��Ԫ��ֵ
            /// </summary>
            public static object ScalarSP(string SPName)
            {
                SPParam temp = new SPParam(SPName);
                return ScalarSP(temp);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public enum ParamDirection
        {
            /// <summary>
            /// �������
            /// </summary>
            Input = 1,
            /// <summary>
            /// �����������
            /// </summary>
            InputOutput = 3,
            /// <summary>
            /// �������
            /// </summary>
            Output = 2,
            /// <summary>
            /// ����ֵ
            /// </summary>
            ReturnValue = 6

        }

        /// <summary>
        /// �����ṹ
        /// </summary>
        public struct ParamObj
        {
            /// <summary>
            /// ֵ
            /// </summary>
            public object PValue;
            /// <summary>
            /// ����
            /// </summary>
            public ParamDirection PDirection;

            /// <summary>
            /// ����
            /// </summary>
            /// <param name="pvalue">ֵ</param>
            /// <param name="pDirection">��������</param>
            public ParamObj(object pvalue, ParamDirection pDirection)
            {
                PValue = pvalue;
                PDirection = pDirection;
            }
        }

        /// <summary>
        /// �洢���̲�������
        /// </summary>
        public class SPParam
        {
            /// <summary>
            /// �洢������
            /// </summary>
            public string m_SpName;
            /// <summary>
            /// ��������
            /// </summary>
            internal Hashtable m_Param;

            /// <summary>
            /// ������������
            /// </summary>
            /// <param name="SPName">Ҫ�����Ĵ洢������</param>
            public SPParam(string SPName)
            {
                m_Param = new Hashtable();

                m_SpName = SPName;
            }

            /// <summary>
            /// �����������죬ʹ��������ؿ��Ի�ø�������
            /// </summary>
            /// <param name="SPName">Ҫ�����Ĵ洢������</param>
            /// <param name="ParamCount">�����ĸ���</param>
            public SPParam(string SPName, int ParamCount)
            {
                m_Param = new Hashtable(ParamCount);

                m_SpName = SPName;
            }

            /// <summary>
            /// ����������
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
            /// ��ӷ��������
            /// </summary>
            /// <param name="ParamName">������</param>
            /// <param name="pDirection">��������</param>
            public void addParam(string ParamName, ParamDirection pDirection)
            {
                ParamObj myParam = new ParamObj(null, pDirection);
                m_Param[ParamName] = myParam;
            }
        }
        /// <summary>
        /// ����ת����
        /// </summary>
        public class Dc
        {
            /// <summary>
            /// ת�����ݵ�Int32,���ΪDbNull�򷵻�0
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
            /// ת�����ݵ�String,���ΪDbNull�򷵻�null
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
            /// ת�����ݵ�Decimal,���ΪDbNull�򷵻�0
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
            /// ת�����ݵ�DateTime,���ΪDbNull�򷵻�1900-01-01
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
            /// ת�����ݵ�bool,���ΪDbNull�򷵻�false
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
