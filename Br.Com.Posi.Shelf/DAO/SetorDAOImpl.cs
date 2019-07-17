using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;
using System;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    sealed class SetorDAOImpl : DAOImpl<Setor>, ISetorDAO
    {
        private IPerfilDAO daoPerfil;

        public SetorDAOImpl() : base("Setor", "ID", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoPerfil = DAOFactory.InitPerfilDAO();
        }

        public override Setor parseToDTO(DataRow row)
        {
            Setor setor = new Setor();
            setor.IDSetor = row.GetValue("ID", default(long));
            setor.Nome = row.GetValue("Nome", string.Empty);
            daoPerfil.GetListWhere("ID_Setor", setor.IDSetor).ForEach(e => setor.Perfis.Add(e));
            return setor;
        }

        public override Dictionary<string, object> ParseToParamenters(Setor t)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("ID", t.IDSetor);
            parms.Add("Nome", t.Nome);
            return parms;
        }

        public Setor VerificaSetor(string setor)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Setor", setor);

            using (DataTable dataTable = this.GetDataTable($"SELECT * FROM {this.GetTableName()} WHERE Nome = @Setor", dic))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return null;
        }

        public override Setor Save(Setor t)
        {            
            if(t == null)
            {
                throw new ArgumentNullException("Setor não pode ser nulo");
            }

            t = SaveSimple(t, (r, id) => r.IDSetor = id, $"INSERT INTO {this.GetTableName()} (Nome) output INSERTED.ID VALUES (@Nome)", this.ParseToParamenters(t));
            if (t.Perfis.Any())
            {
                t.Perfis.Select(p => { return daoPerfil.Save(p); }).ToList();
            }
            return t;
        }

        public override Setor Update(Setor t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("Setor não pode ser nulo");
            }
            if (t.IDSetor <= 0)
            {
                throw new ArgumentNullException("O campo IDSetor não pode ser menor ou igual a 0");
            }

            t =  this.UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Nome = @Nome WHERE {this.GetPKColumnName()} = @ID", this.ParseToParamenters(t));
            if (t.Perfis.Any())
            {
                t.Perfis.Select(p => { return daoPerfil.SaveOrUpdate(p); }).ToList();
            }

            return t;
        }
    }
}
