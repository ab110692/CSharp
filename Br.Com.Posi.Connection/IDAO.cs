using Br.Com.Posi.Connection.Model;
using System;
using System.Collections.Generic;

namespace Br.Com.Posi.Connection
{
    public interface IDAO<T> where T : IModelo
    {
        /// <summary>
        /// Obtem Modelo por Primary Key
        /// </summary>
        /// <param name="id">Informa a chave primaria</param>
        /// <returns>Modelo</returns>
        T GetByPK(long id);
        
        /// <summary>
        /// Obtem a primeira linha do modelo.
        /// </summary>
        /// <returns>Modelo</returns>
        T GetFirst();

        /// <summary>
        /// Obtem a ultima linha do modelo.
        /// </summary>
        /// <returns>Modelo</returns>
        T GetLast();        
        
        /// <summary>
        /// Obtem a tabela inteira para seu modelo
        /// </summary>
        /// <returns>modelo</returns>
        List<T> GetList();

        /// <summary>
        /// Obtem tabela de acordo com seu where
        /// </summary>
        /// <param name="column">nome da coluna</param>
        /// <param name="Value">valor para essa coluna</param>
        /// <returns></returns>
        List<T> GetListWhere(string column, object value);

        /// <summary>
        /// Verifica a existencia do modelo
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Exist(T t);

        /// <summary>
        /// Obtem date e hora.
        /// </summary>
        /// <returns></returns>
        DateTime GetDateTime();

        T Save(T t);

        T Update(T t);

        bool Delete(T t);

        List<T> SaveList(List<T> t);

        List<T> UpdateList(List<T> t);

        T SaveOrUpdate(T t);

        List<T> SaveOrUpdate(List<T> t);

        List<bool> DeleteList(List<T> t);

        string GetTableName();
    }
}
