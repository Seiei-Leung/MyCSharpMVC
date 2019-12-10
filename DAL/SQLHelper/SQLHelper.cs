using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;


namespace DAL
{
    public class SQLHelper
    {
        // 设置要放在 MVC 项目根目录的 Web.config 文件中
        private static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        /// <summary>
        /// 增，删，改
        /// </summary>
        /// <param name="sql"></param>
        public static int executeNonQuery(string sql)
        {
            MySqlConnection connection = new MySqlConnection(SQLHelper.connString);
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 获取 结果集 的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static MySqlDataReader getReader(string sql)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection); // 关闭数据库链接，不能在 finally 语句中关闭链接，因为如果关闭了，MySqlDataReader 而又尚未读取，关闭之后 MySqlDataReader 也就读取不到了
                return result;
            }
            catch (Exception ex)
            {
                // todo 写入日志
                throw ex;
                conn.Close();
            }
        }

        /// <summary>
        /// 设置事务
        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public static bool ExecuteTransaction(List<string> sqlList)
        {
            MySqlConnection connection = new MySqlConnection(connString);
            MySqlCommand command = new MySqlCommand();
            
            // 设置链接
            command.Connection = connection;
            
            // 打开连接
            connection.Open();
            
            // 批量操作后是否成功
            bool isSuccess = true;

            // 设置事务
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (string sql in sqlList)
                {
                    command.CommandText = sql; // 设置 sql 语句
                    int result = command.ExecuteNonQuery();
                    if (result == 0)
                    {
                        isSuccess = false;
                    }
                }

                // 提交事务
                if (isSuccess)
                {
                    transaction.Commit();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // 事务回滚
                throw ex;
            }
            finally
            {
                connection.Close(); // 关闭链接
            }
        }

    }
}
