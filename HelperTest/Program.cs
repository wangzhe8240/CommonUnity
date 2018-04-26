using PublicProgram.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
namespace HelperTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //log4net日志      
            log4net.ILog logInfo = log4net.LogManager.GetLogger("loginfo");
            logInfo.Info("测试日志写入");
            logInfo.Debug("初始化连接开始");
            logInfo.InfoFormat("测试日志 name={0}", "client");
        }
    }
    /// <summary>
    /// 测试sqlhelper
    /// </summary>
    public class TestSqlHelper
    {
        public TestSqlHelper()
        {
            //IDataReader reader = null;
            string connStr = "User ID=agile;Password=agile;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            CommonSqlHelper commonsql = new CommonSqlHelper(connStr);
            commonsql.CreateConn();
            commonsql.ExecuteNonQuery("select * from SYS_CITY");
        }
    }
}
