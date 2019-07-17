
using Br.Com.Posi.NoteAnalyzer.Model;
using System.Collections.Generic;

namespace Br.Com.Posi.NoteAnalyzer.DAO
{
    public interface IDAO<T> where T : IModel
    {
        T GetById(int id);
        T GetFisrt();
        T GetLast();
        List<T> GetList();
        bool Exist(T t);
        bool Save(T t);
        bool SaveOrUpdate(T t);
        bool Update(T t);
        bool Delete(T t);
        void SaveList(List<T> t);
        void UpdateList(List<T> t);
        void DeleteList(List<T> t);
        string GetTableName();
        string GetIdColumnName();
    }
}
