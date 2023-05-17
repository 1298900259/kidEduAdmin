using System.Data;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AppArchi.Networking
{
    public class DatabaseMgr
    {
        //request
        public static MySqlConnection sqlConnection;
        //Host
        string host;
        public string Host { get { return host; } set { host = value; } }
        //User
        string user;
        public string User { get { return user; } set { user = value; } }
        //Password
        string pwd;
        public string Pwd { get { return pwd; } set { pwd = value; } }
        //Database
        string database;
        public string Database { get { return database; } set { database = value; } }
        public void Bind(string host, string user, string pwd, string database)
        {
            this.host = host;
            this.user = user;
            this.pwd = pwd;
            this.database = database;
        }
        public void Connect()
        {
            try
            {
                string sql = string.Format("Database={0};Data Source={1};User Id={2};Password={3};", database, host, user, pwd, "3306");
                sqlConnection = new MySqlConnection(sql);
                sqlConnection.Open();
#if UNITY_EDITOR
                Debug.Log("Database have connected!");
#endif
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Connect Failed!");
#endif
            }
        }
        public void Close()
        {
            if (IsDatabaseOpen())
            {
                sqlConnection.Close();
#if UNITY_EDITOR
                Debug.Log("Database have closed!");
#endif
            }
        }
        bool IsDatabaseOpen()
        {
            if (sqlConnection == null) return false;
            return sqlConnection.State == ConnectionState.Open;
        }
        public bool Insert<T>(string table, string column, T value)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("Insert into {0} where {1} = @paral0", table, column);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value);
                    MySqlDataReader reader = insert.ExecuteReader();
                    if (reader.Read())
                    {
                    }
                }
#if UNITY_EDITOR
                Debug.Log("Insert Succeed!");
#endif
                return true;

            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Insert Failed!");
#endif
                return false;
            }
        }
        public bool Insert<T1, T2>(string table, string column1, T1 value1, string column2, T2 value2)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("insert into {0} set {1} =@paral0 and {2} =@paral1", table, column1, column2);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value1);
                    insert.Parameters.AddWithValue("paral1", value2);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                }
#if UNITY_EDITOR
                Debug.Log("Insert Succeed!");
#endif
                return true;

            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Insert Failed!");
#endif
                throw;
            }
        }
        public bool Insert<T1, T2, T3>(string table, string column1, T1 value1, string column2, T2 value2, string column3, T3 value3)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("insert into {0}({1},{2},{3})VALUES(@paral0,@paral1,@paral2)", table, column1, column2, column3);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value1);
                    insert.Parameters.AddWithValue("paral1", value2);
                    insert.Parameters.AddWithValue("paral2", value3);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                }
#if UNITY_EDITOR
                Debug.Log("Insert Succeed!");
#endif
                return true;

            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Insert Failed!");
#endif
                throw;
            }
        }
        public bool Clear(string table)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("delete from {0}", table);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                }

#if UNITY_EDITOR
                Debug.Log("Clear Succeed!");
#endif
                return true;

            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Delete Failed!");
#endif
                return false;
            }
        }
        public bool Delete<T>(string table, string column, T value)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("delete from {0} where {1} =@paral0", table, column);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                }

#if UNITY_EDITOR
                Debug.Log("Delete Succeed!");
#endif
                return true;

            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Delete Failed!");
#endif
                return false;
            }
        }
        public bool Delete<T1, T2>(string table, string column1, T1 value1, string column2, T2 value2)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("delete from {0} where {1} =@paral0 and {2} =@paral1", table, column1, column2);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value1);
                    insert.Parameters.AddWithValue("paral1", value2);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                }

#if UNITY_EDITOR
                Debug.Log("Delete Succeed!");
#endif
                return true;

            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Delete Failed!");
#endif
                return false;
            }
        }
        public List<int> Select(string key, string table)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("Select {0} from {1} ", key, table);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    MySqlDataReader reader = insert.ExecuteReader();
                    List<int> dataArray = new List<int>();
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetInt32(0));
                        dataArray.Add(reader.GetInt32(0));
                    }
                    //string str = reader[0].ToString();
                    return dataArray;
                }
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Search Failed!");
#endif
                return null;
            }
        }
        public List<int> GetScoreNum()
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = "SELECT" +
                    "  (CASE WHEN firstDayScore <> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN secondDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN thirdDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN fourthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN fifthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN sixthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN seventhDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN eighthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN ninthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN tenthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN eleventhDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN twelfthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN thirteenthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN fourteenthDayScore<> 0 THEN 1 ELSE 0 END) +" +
                    "(CASE WHEN fifteenthDayScore<> 0 THEN 1 ELSE 0 END) AS non_zero_count " +
                    "FROM scorelist; ";
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    MySqlDataReader reader = insert.ExecuteReader();
                    List<int> dataArray = new List<int>();
                    while (reader.Read())
                    {
                        dataArray.Add(reader.GetInt32(0));
                    }
                    //string str = reader[0].ToString();
                    return dataArray;
                }
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Search Failed!");
#endif
                return null;
            }
        }
        public List<string> Select<T>(string key, string table)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("Select {0} from {1} ", key, table);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    MySqlDataReader reader = insert.ExecuteReader();
                    List<string> dataArray = new List<string>();
                    while (reader.Read())
                    {
                        dataArray.Add(reader.GetString(0));
                    }
                    //string str = reader[0].ToString();
                    return dataArray;
                }
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Search Failed!");
#endif
                return null;
            }
        }

        public string Select<T>(string key, string table, string column, T value)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("Select {0} from {1} where {2} = @paral0", key, table, column);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                    string str = reader[0].ToString();
                    Close();
                    return str;
                }
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Search Failed!");
#endif
                return null;
            }
        }
        public string Select<T1, T2>(string key, string table, string column1, T1 value1, string column2, T2 value2)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("Select {0} from {1} where {2} = @paral0 and {3} =@paral1", key, table, column1, column2);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value1);
                    insert.Parameters.AddWithValue("paral1", value2);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                    string str = reader[0].ToString();
                    Close();
                    return str;
                }
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Search Failed!");
#endif
                return null;
            }
        }
        public bool Update<T1>(string table, string key, T1 value1, string column, object paraml)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("Update {0} Set {1} = @paral0 where {2} = @paral1", table, key, column);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value1);
                    insert.Parameters.AddWithValue("paral1", paraml);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                    Close();
                    return true;
                }
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Update Failed!");
#endif
                return false;
            }
        }
        public bool Update<T1>(string table, string key, T1 value1, string column1, object paraml1, string column2, object paraml2)
        {
            if (!IsDatabaseOpen()) Connect();
            try
            {
                string cmd = string.Format("Update {0} Set {1} = @paral0 where {2} = @paral1 and {3} = @paral2", table, key, column1,column2);
                using (MySqlCommand insert = new MySqlCommand(cmd, sqlConnection))
                {
                    insert.Parameters.AddWithValue("paral0", value1);
                    insert.Parameters.AddWithValue("paral1", paraml1);
                    insert.Parameters.AddWithValue("paral2", paraml2);
                    MySqlDataReader reader = insert.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                    Close();
                    return true;
                }
            }
            catch (System.Exception)
            {
#if UNITY_EDITOR
                Debug.Log("Update Failed!");
#endif
                return false;
            }
        }
    }

}
