using Br.Com.Posi.Connection.Model;
using Br.Com.Posi.Connection.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Br.Com.Posi.Connection
{
    public abstract class DAOImpl<T> : IDAO<T> where T : IModelo
    {
        private String tableName, pkColumnName;
        private Configuracao Configuration;

        public DAOImpl(String tableName, String pkColumnName, Configuracao Configuration)
        {
            this.tableName = tableName;
            this.pkColumnName = pkColumnName;
            this.Configuration = Configuration;
        }

        public SqlConnection OpenConnection(Configuracao configuration)
        {
            StringBuilder stringConnection = new StringBuilder();
            stringConnection = stringConnection.Append("Data Source=" + configuration.Servidor + ";")
                .Append("User ID=" + configuration.Usuario + ";")
                .Append("Password=" + configuration.Senha + ";")
                .Append("Initial Catalog=" + configuration.BancoDeDados + ";");

            SqlConnection con = new SqlConnection()
            {
                ConnectionString = stringConnection.ToString()
            };
            con.Open();
            return con;
        }

        public int ExecuteNonQuery(String query, Dictionary<String, Object> parameters = null)
        {
            using (SqlConnection con = OpenConnection(Configuration))
            {
                using (SqlCommand command = new SqlCommand(query, con))
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
            }
            throw new Exception("Ocorreu um erro na operação: ExecuteNonQuery");

        }

        public long ExecuteScalar(String query, Dictionary<String, Object> parameters = null)
        {
            using (SqlConnection con = OpenConnection(Configuration))
            {
                using (SqlCommand command = new SqlCommand(query, con))
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
            }
            throw new Exception("Ocorreu um erro na operação: ExecuteScalar");
        }

        protected DataTable GetDataTable(String query, Dictionary<String, Object> paraments = null)
        {
            using (SqlConnection con = OpenConnection(Configuration))
            {
                {
                    using (SqlCommand sql = new SqlCommand(query, con))
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
                            using (DataSet dataSet = new DataSet())
                            {
                                adapter.Fill(dataSet);
                                if (dataSet.Tables.Count > 0)
                                {
                                    DataTable dt = dataSet.Tables[0];
                                    return dt;
                                }
                            }
                        }
                    }
                }
            }
            return new DataTable();
        }

        public abstract T parseToDTO(DataRow row);

        public abstract Dictionary<String, Object> ParseToParamenters(T t);

        public bool Exist(T t)
        {
            return this.ExecuteNonQuery(String.Format("SELECT * FROM {0} WHERE {1} = {2}", this.tableName, this.pkColumnName, t.ID)) > 0;
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

        public T GetFirst()
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

        public List<T> GetListWhere(string column, object value)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(column, value);
            using (DataTable dataTable = this.GetDataTable($"select * from {this.tableName} where {column} = @{column}", dic))
            {
                List<T> list = new List<T>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }

        public DateTime GetDateTime()
        {
            using (DataTable dataTable = this.GetDataTable("Select GetDate() as Data"))
            {
                return dataTable.Rows[0].GetValue("Data", default(DateTime));
            }
        }

        abstract public T Save(T t);

        abstract public T Update(T t);

        public bool Delete(T t)
        {
            return ExecuteNonQuery(String.Format("DELETE FROM {0} WHERE {1} = {2}", this.tableName, this.pkColumnName, t.ID)) > 0;
        }

        public void DeleteList(List<T> t)
        {
            foreach (T list in t)
            {
                Delete(list);
            }
        }

        public T SaveSimple(T t, Action<T, long> sucess, string query, Dictionary<string, object> dic = null)
        {
            long id = this.ExecuteScalar(query, dic);
            sucess?.Invoke(t, id);
            return t;
        }

        public T UpdateSimple(T t, string query, Dictionary<string, object> dic = null)
        {
            long qts = this.ExecuteNonQuery(query, dic);
            if (qts > 0)
            {
                return t;
            }
            else
            {
                throw new Exception("Alteração no banco de dados não realizado!");
            }
        }

        public virtual T SaveOrUpdate(T t)
        {
            return t.ID <= 0 ? this.Save(t) : this.Update(t);
        }

        public virtual void SaveOrUpdate(List<T> t)
        {
            foreach (T list in t)
            {
                if (list.ID <= 0)
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
                if (list.ID <= 0)
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
                if (list.ID <= 0)
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
                if (list.ID <= 0)
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

        List<T> IDAO<T>.SaveList(List<T> t)
        {
            List<T> result = new List<T>();
            foreach (T list in t)
            {
                result.Add(Save(list));
            }
            return result;
        }

        List<T> IDAO<T>.UpdateList(List<T> t)
        {
            List<T> result = new List<T>();
            foreach (T list in t)
            {
                result.Add(Update(list));
            }
            return result;
        }

        List<T> IDAO<T>.SaveOrUpdate(List<T> t)
        {
            List<T> result = new List<T>();
            foreach (T list in t)
            {
                result.Add(SaveOrUpdate(list));
            }
            return result;
        }

        List<bool> IDAO<T>.DeleteList(List<T> t)
        {
            List<bool> result = new List<bool>();
            foreach (T list in t)
            {
                result.Add(Delete(list));
            }
            return result;
        }

        public string GetTableName()
        {
            return this.tableName;
        }

        public string GetPKColumnName()
        {
            return this.pkColumnName;
        }

    }
}
