using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading;

namespace TSFCS.SCOP
{
    /// <summary>
    /// 数据库接口类
    /// </summary>
    public class Sqlite : IDisposable
    {
        #region Variable
        private bool isOpen = false;
        private bool disposed = false;
        private string path = string.Empty;
        private string datasource = string.Empty;
        private SQLiteConnection connection = null;
        private Dictionary<string, object> parameters = null;
        #endregion

        #region Constructor
        /**/
        /// <summary>
        /// 连接指定的数据库
        /// </summary>
        /// <param name="path">路径</param>
        public Sqlite(string path)
        {
            Init(path);
        }
        public Sqlite(string path, string password)
        {
            Init(path, password);
        }
        /**/
        /// <summary>
        /// 清理托管资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /**/
        /// <summary>
        /// 清理所有使用资源
        /// </summary>
        /// <param name="disposing">如果为true则清理托管资源</param>
        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                // dispose all managed resources.
                if (disposing)
                {
                    this.isOpen = false;
                    connection.Dispose();
                }
                // dispose all unmanaged resources
                //this.Close();
                disposed = true;
            }
        }

        ~Sqlite()
        {
            if (this.isOpen)
                Dispose(false);
        }
        #endregion

        #region Methods
        private void Init(string path)
        {
            Init(path, null);
        }

        private void Init(string path, string password)
        {
            this.path = path;
            if (string.IsNullOrEmpty(password))
            {
                this.datasource = "Data Source=" + path;
            }
            else
            {
                this.datasource = "Data Source=" + path + ";Password=" + password;
            }
            this.isOpen = false;
            this.connection = new SQLiteConnection(this.datasource);
            this.parameters = new Dictionary<string, object>();
        }

        private bool CheckDbExist()
        {
            if (System.IO.File.Exists(path))
                return true;
            else
                return false;
        }

        public bool IsOpen
        {
            get { return isOpen; }
        }

        public void Open()
        {
            if (!CheckDbExist())
                throw new SQLiteException();
            if (!isOpen)
                connection.Open();
            this.isOpen = true;
        }

        public void Close()
        {
            if (isOpen)
                connection.Close();
            this.isOpen = false;
        }

        /**/
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        public void AddParameter(string key, object value)
        {
            parameters.Add(key, value);
        }
        /**/
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        public void ExecuteNonQuery(string queryStr)
        {
            this.Open();
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = queryStr;
                        foreach (KeyValuePair<string, object> kvp in this.parameters)
                        {
                            command.Parameters.Add(new SQLiteParameter(kvp.Key, kvp.Value));
                        }
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    this.parameters.Clear();
                }
            }
            this.Close();
        }
        /**/
        /// <summary>
        /// 执行多条SQL语句
        /// </summary>
        /// <param name="dataList">插入对象的集合</param>
        public void ExecuteNonQuery(string queryStr, List<object> dataList)
        {
            this.Open();
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = queryStr;
                        foreach (object data in dataList)
                        {
                            //this.parameters.Add("sendtime", data.SendTime);

                            foreach (KeyValuePair<string, object> kvp in this.parameters)
                            {
                                command.Parameters.Add(new SQLiteParameter(kvp.Key, kvp.Value));
                            }
                            command.ExecuteNonQuery();

                            this.parameters.Clear();
                        }
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    this.parameters.Clear();
                }
            }
            this.Close();
        }
        /**/
        /// <summary>
        /// 执行SQL语句并返回所有结果
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        /// <returns>返回DataTable</returns>
        public DataTable ExecuteQuery(string queryStr)
        {
            DataTable dt = new DataTable();
            this.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    command.CommandText = queryStr;
                    foreach (KeyValuePair<string, object> kvp in this.parameters)
                    {
                        command.Parameters.Add(new SQLiteParameter(kvp.Key, kvp.Value));
                    }
                    adapter.Fill(dt);
                }
            }
            catch (SQLiteException e)
            {
                throw new SQLiteException(SQLiteErrorCode.IoErr_Read, e.Message);
            }
            finally
            {
                this.Close();
                this.parameters.Clear();
            }
            return dt;
        }
        /**/
        /// <summary>
        /// 执行SQL语句并返回第一行
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        /// <returns>返回DataRow</returns>
        public DataRow ExecuteRow(string queryStr)
        {
            DataRow row;
            this.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    command.CommandText = queryStr;
                    foreach (KeyValuePair<string, object> kvp in this.parameters)
                    {
                        command.Parameters.Add(new SQLiteParameter(kvp.Key, kvp.Value));
                    }
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count == 0)
                        row = null;
                    else
                        row = dt.Rows[0];
                }
            }
            catch (SQLiteException e)
            {
                throw new SQLiteException(SQLiteErrorCode.Row, e.Message);
            }
            finally
            {
                this.parameters.Clear();
                this.Close();
            }

            return row;
        }
        /**/
        /// <summary>
        /// 执行SQL语句并返回结果第一行的第一列
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        /// <returns>返回值</returns>
        public Object ExecuteScalar(string queryStr)
        {
            Object obj = null;

            this.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = queryStr;
                    foreach (KeyValuePair<string, object> kvp in this.parameters)
                    {
                        command.Parameters.Add(new SQLiteParameter(kvp.Key, kvp.Value));
                    }
                    obj = command.ExecuteScalar();
                }
            }
            finally
            {
                this.parameters.Clear();
                this.Close();
            }

            return obj;
        }

        public SQLiteDataReader ExecuteReader(string query)
        {
            SQLiteDataReader reader = null;

            this.Open();
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = query;
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (SQLiteException e)
            {
                throw new SQLiteException(SQLiteErrorCode.IoErr_Read, e.Message);
            }
            //SQLiteDataReader对象和Connection对象暂时不能关闭和释放掉，否则在调用时无法使用;待使用完毕SQLiteDataReader，再关闭SQLiteDataReader对象（同时会自动关闭关联的Connection对象）  --reader.Close();
            //Connection的关闭是指关闭连接通道，但连接对象依然存在,Connection的释放掉是指销毁连接对象

            return reader;
        }
        #endregion

    }

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class SQLiteConnectionString
    {
        public static string GetConnectionString(string path)
        {
            return GetConnectionString(path, null);
        }

        public static string GetConnectionString(string path, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "Data Source=" + path;
            }
            else
            {
                return "Data Source=" + path + ";Password=" + password;
            }
        }

    }

}
