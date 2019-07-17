using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.ObjectModel;
using Br.Com.Posi.NoteAnalyzer.Model;
using Br.Com.Posi.NoteAnalyzer.DAO;

namespace Br.Com.Posi.Shelf.DAO
{
    abstract class DAOImpl<T> : IDAO<T> where T : IModel
    {
        private String tableName, pkColumnName, fkColumnName;
        private SqlConnection con;
        private string dataBase;

        public DAOImpl(String tableName, String pkColumnName, String fkColumnName, string dataBase)
        {
            this.tableName = tableName;
            this.pkColumnName = pkColumnName;
            this.fkColumnName = fkColumnName;
            this.dataBase = dataBase;
            if (con == null)
            {
                con = new SqlConnection();
            }

        }

        ~DAOImpl()
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Dispose();
            }
        }


        public SqlConnection OpenConnection(string dataBase)
        {
            string server = "192.193.10.254";
            string user = "sa";
            string pass = "Dko5cv4k";

            if (con.State != ConnectionState.Open)
            {
                StringBuilder stringConnection = new StringBuilder();
                stringConnection = stringConnection.Append("Data Source=" + server + ";")
                    .Append("User ID=" + user + ";")
                    .Append("Password=" + pass + ";")
                    .Append("Initial Catalog=" + dataBase + ";");
                con.ConnectionString = stringConnection.ToString();
                con.Open();
            }
            return con;
        }

        public int ExecuteNonQuery(String query, Dictionary<String, Object> parameters = null)
        {
            con = OpenConnection(this.dataBase);
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                if (parameters != null)
                {
                    foreach (KeyValuePair<String, Object> p in parameters)
                    {
                        command.Parameters.Add(new SqlParameter(p.Key, p.Value));
                    }
                }
                int result = command.ExecuteNonQuery();
                return result;
            }
            finally
            {
                command.Clone();
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            throw new Exception("Ocorreu um erro na operação: ExecuteNonQuery");
        }

        public long ExecuteScalar(String query, Dictionary<String, Object> parameters = null)
        {
            con = OpenConnection(this.dataBase);
            SqlCommand command = new SqlCommand(query, con);
            try
            {


                if (parameters != null)
                {
                    foreach (KeyValuePair<String, Object> p in parameters)
                    {
                        command.Parameters.Add(new SqlParameter(p.Key, p.Value));
                    }
                }
                long result = (long)command.ExecuteScalar();
                return result;
            }

            finally
            {
                command.Clone();
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            throw new Exception("Ocorreu um erro na operação: ExecuteScalar");
        }

        protected DataTable GetDataTable(String query, Dictionary<String, Object> paraments = null)
        {
            con = OpenConnection(this.dataBase);
            SqlCommand sql = new SqlCommand(query, con);
            try
            {
                if (paraments != null)
                {
                    foreach (KeyValuePair<String, Object> p in paraments)
                    {
                        sql.Parameters.Add(new SqlParameter(p.Key, p.Value));
                    }
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if (dataSet.Tables.Count > 0)
                    {
                        DataTable dt = dataSet.Tables[0];
                        return dt;
                    }
                }
            }
            finally
            {
                sql.Clone();
                sql.Dispose();
                con.Close();
                con.Dispose();
            }
            return new DataTable();
        }

        public abstract T parseToDTO(DataRow row);

        public abstract Dictionary<String, Object> ParseToParamenters(T t);

        public bool Exist(T t)
        {
            return this.ExecuteNonQuery(String.Format("SELECT * FROM {0} WHERE {1} = {2}", this.tableName, this.pkColumnName, t.GetID())) > 0;
        }

        public T GetByPK(long id)
        {
            using (DataTable dataTable = this.GetDataTable(String.Format("select * from {0} WHERE {1} = {2}", this.tableName, this.pkColumnName, id)))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return default(T);
        }

        public List<T> GetByFK(long id)
        {
            if (this.GetFKColumnName().Equals(string.Empty))
            {
                throw new ArgumentNullException("pfColumnName", "Não existe chave estrangeira vinculado a Classe !");
            }
            using (DataTable dataTable = this.GetDataTable(String.Format("select * from {0} WHERE {1} = {2}", this.tableName, this.fkColumnName, id)))
            {
                List<T> list = new List<T>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }

        public T GetFisrt()
        {
            using (DataTable dataTable = this.GetDataTable(String.Format("select TOP 1 * from {0} ", this.tableName)))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return default(T);
        }

        public T GetLast()
        {
            using (DataTable dataTable = this.GetDataTable(String.Format("select TOP 1 * from {0} ORDER BY {1} DESC", this.tableName, this.pkColumnName)))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return default(T);
        }

        public List<T> GetList()
        {
            using (DataTable dataTable = this.GetDataTable(String.Format("select * from {0}", this.tableName)))
            {
                List<T> list = new List<T>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }

        /*public DateTime GetDateTime()
        {
            using (DataTable dataTable = this.GetDataTable("Select GetDate() as Data"))
            {
                return dataTable.Rows[0].GetValue("Data", default(DateTime));
            }
        }*/

        abstract public T Save(T t);

        public bool Delete(T t)
        {
            return ExecuteNonQuery(String.Format("DELETE FROM {0} WHERE {1} = {2}", this.tableName, this.pkColumnName, t.GetID())) > 0;
        }

        public void DeleteList(List<T> t)
        {
            foreach (T list in t)
            {
                Delete(list);
            }
        }

        public T SaveSimpleWithRetorn(T t, Action<T, long> sucess, string query, Dictionary<string, object> parms = null)
        {
            long id = this.ExecuteScalar(query, parms);
            sucess?.Invoke(t, id);
            return t;
        }

        public T SaveSimple(T t, string query, Dictionary<string, object> parms = null)
        {
            long id = this.ExecuteNonQuery(query, parms);
            return t;
        }

        public virtual T SaveOrUpdate(T t)
        {
            return t.GetID() <= 0 ? this.Save(t) : this.Update(t);
        }

        public virtual void SaveOrUpdate(List<T> t)
        {
            foreach (T list in t)
            {
                if (list.GetID() <= 0)
                {
                    this.Save(list);
                }
                else
                {
                    this.Update(list);
                }
            }
        }

        public virtual void SaveOrUpdate(ObservableCollection<T> t)
        {
            foreach (T list in t)
            {
                if (list.GetID() <= 0)
                {
                    this.Save(list);
                }
                else
                {
                    this.Update(list);
                }
            }
        }
        public void SaveOrUpdate(List<T> t, List<T> deleteList)
        {
            foreach (T list in t)
            {
                if (list.GetID() <= 0)
                {
                    this.Save(list);
                }
                else
                {
                    this.Update(list);
                }
            }
            foreach (T list in deleteList)
            {
                this.Delete(list);
            }
        }

        public void SaveOrUpdate(ObservableCollection<T> t, ObservableCollection<T> deleteList)
        {
            foreach (T list in t)
            {
                if (list.GetID() <= 0)
                {
                    this.Save(list);
                }
                else
                {
                    this.Update(list);
                }
            }
            foreach (T list in deleteList)
            {
                this.Delete(list);
            }
        }

        abstract public T Update(T t);

        public void SaveList(List<T> t)
        {
            foreach (T list in t)
            {
                Save(list);
            }
        }

        public void UpdateList(List<T> t)
        {
            foreach (T list in t)
            {
                Update(list);
            }
        }

        public string GetTableName()
        {
            return this.tableName;
        }

        public string GetPKColumnName()
        {
            return this.pkColumnName;
        }

        public string GetFKColumnName()
        {
            return this.fkColumnName;
        }

        public void SaveList(ObservableCollection<T> t)
        {
            foreach (T list in t)
            {
                Save(list);
            }
        }

        public void UpdateList(ObservableCollection<T> t)
        {
            foreach (T list in t)
            {
                Update(list);
            }
        }

        public void DeleteList(ObservableCollection<T> t)
        {
            foreach (T list in t)
            {
                Delete(list);
            }
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        bool IDAO<T>.Save(T t)
        {
            throw new NotImplementedException();
        }

        bool IDAO<T>.SaveOrUpdate(T t)
        {
            throw new NotImplementedException();
        }

        bool IDAO<T>.Update(T t)
        {
            throw new NotImplementedException();
        }

        public string GetIdColumnName()
        {
            throw new NotImplementedException();
        }
    }
}
