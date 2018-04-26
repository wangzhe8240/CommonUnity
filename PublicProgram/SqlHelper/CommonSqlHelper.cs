using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicProgram.SqlHelper
{
    /// <summary>
    /// 通用SqlHelper
    /// </summary>
    public class CommonSqlHelper
    {
        IDbCommand cmd = null;
        IDataReader reader = null;
        private IDbConnection conn = null;
        private DbType type = DbType.ORACLE;

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="ConnectionString"></param>
        public CommonSqlHelper(string ConnectionString)
        {
            conn = DBFactory.CreateDbConnection(type, ConnectionString);
        }

        /// <summary>
        /// 判断并打开连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConn()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// 执行查询sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回一个表</returns>
        public DataTable ExecuteReader(string sql)
        {
            DataTable dt = new DataTable();
            using (cmd = DBFactory.CreateDbCommand(sql, CreateConn()))
            {
                using (reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            conn.Close();
            return dt;
        }

        /// <summary>
        /// 查询带有参数的sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>返回一个表</returns>
        public DataTable ExecuteReader(string sql, IDataParameter[] param)
        {
            DataTable dt = new DataTable();
            try
            {
                using (cmd = DBFactory.CreateDbCommand(sql, CreateConn()))
                {
                    cmd.Parameters.AddRange(param);
                    using (reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public DataTable ExecuteReader(string sql, IDataParameter param)
        {
            DataTable dt = new DataTable();
            using (cmd = DBFactory.CreateDbCommand(sql, CreateConn()))
            {
                cmd.Parameters.Add(param);
                using (reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            conn.Close();
            return dt;
        }


        /// <summary>
        /// 执行无参曾删改sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>ExecuteNonQuery（返回所影响的行数）</returns>
        public int ExecuteNonQuery(string sql)
        {
            int rows = 0;
            using (cmd = DBFactory.CreateDbCommand(sql, CreateConn()))
            {
                rows = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return rows;
        }

        /// <summary>
        /// 带参数增删改sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, IDataParameter[] param)
        {
            int rows = 0;
            using (cmd = DBFactory.CreateDbCommand(sql, CreateConn()))
            {
                cmd.Parameters.AddRange(param);
                rows = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return rows;
        }
        public int ExecuteNonQuery(string sql, IDataParameter param)
        {
            int rows = 0;
            using (cmd = DBFactory.CreateDbCommand(sql, CreateConn()))
            {
                cmd.Parameters.Add(param);
                rows = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return rows;
        }


        #region 事务
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLList">SQL语句的哈希表（key为sql语句，value是该语句的OleDbParameter[]）</param>
        public bool ExecuteTransaction(Hashtable SqlList)
        {
            CreateConn();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                IDbCommand cmd = DBFactory.CreateDbCommand(type);
                try
                {
                    //循环
                    foreach (DictionaryEntry myDE in SqlList)
                    {
                        string cmdText = myDE.Key.ToString();
                        IDbDataParameter[] cmdParms = (IDbDataParameter[])myDE.Value;
                        PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        private void PrepareCommand(IDbCommand cmd, IDbConnection conn, IDbTransaction trans, string cmdText, IDataParameter[] cmdParms)
        {
            CreateConn();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
                cmd.Parameters.AddRange(cmdParms);
        }
        #endregion
    }

    /// <summary>
    /// 扩展的AddRange方法
    /// </summary>
    public static class SqlHelperExt
    {
        public static int AddRange(this IDataParameterCollection coll, IDataParameter[] Param)
        {
            int i = 0;
            foreach (var item in Param)
            {
                coll.Add(item);
                i++;
            }
            return i;
        }
    }
}
