using Br.Com.Posi.Connection.Model;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

namespace Br.Com.Posi.Connection
{

    public abstract class DAOHibernateImpl<T>
    {

        private readonly ISession session;
        private readonly ISessionFactory sessionFactory;
        private readonly string table;
        

        public DAOHibernateImpl(string table)
        {
            this.table = table;
            var cfg = new Fluently();
            this.sessionFactory = cfg.BeginConfigure(new Configuration().Configure(@"../../hibernate.cfg.xml")).ExposeConfiguration(c => new SchemaUpdate(c)
                                              .Execute(false, true)).BuildSessionFactory();
            this.session = sessionFactory.OpenSession();
        }

        public bool Delete(T t)
        {
            this.session.Delete(t);
            return true;
        }

        public List<bool> DeleteList(List<T> t)
        {
            foreach (T o in t)
            {
                Delete(o);
            }
            return null;
        }

        public T GetByPK(long id)
        {
            return this.session.Load<T>(id);
        }
        
        public List<T> GetList()
        {
            return (List<T>) this.session.CreateSQLQuery($"SELECT * FROM {table}").List();
        }

        public List<T> GetListWhere(string column, object value)
        {
            return (List<T>)this.session.CreateSQLQuery($"SELECT * FROM {table} WHERE {column} = {value}").List();
        }
        
        public T Save(T t)
        {
            return (T) this.session.Save(t);
        }

        public List<T> SaveList(List<T> t)
        {
            List<T> list = new List<T>();
            foreach (T o in t)
            {
                list.Add(Save(o));
            }
            return list;
        }

        public T SaveOrUpdate(T t)
        {
            this.session.SaveOrUpdate(t);
            return default(T);
        }

        public List<T> SaveOrUpdate(List<T> t)
        {
            foreach (T o in t)
            {
                SaveOrUpdate(o);
            }
            return null;
        }

        public T Update(T t)
        {
            this.session.Update(t);
            return default(T);
        }

        public List<T> UpdateList(List<T> t)
        {
            foreach (T o in t)
            {
                this.session.Update(t);
            }
            return null;
        }







        public DateTime GetDateTime()
        {
            throw new NotImplementedException();
        }

        public T GetFirst()
        {
            throw new NotImplementedException();
        }

        public T GetLast()
        {
            throw new NotImplementedException();
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
        }

        public bool Exist(T t)
        {
            throw new NotImplementedException();
        }
    }
}
