using Br.Com.Posi.Shelf.Enums;
using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Enums;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    sealed class PerfilDAOImpl : DAOImpl<Perfil>, IPerfilDAO
    {

        private IFuncionarioDadosPessoaisDAO daoFuncionarioDadosPessoais;

        public PerfilDAOImpl() : base("Perfil", "ID", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoFuncionarioDadosPessoais = new FuncionarioDadosPessoaisDAOImpl();
        }

        public override Perfil parseToDTO(DataRow row)
        {
            Perfil perfil = new Perfil();
            perfil.IDPerfil = row.GetValue("ID", default(long));
            perfil.Nome = row.GetValue("Nome", string.Empty);
            perfil.Atendimento = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Atendimento", default(Int16)));
            perfil.Manutencao = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Manutencao", default(Int16)));
            perfil.Funcionario = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Funcionario", default(Int16)));
            perfil.Cliente = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Cliente", default(Int16)));
            perfil.AtendimentoTexto = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Atendimento", default(Int16))).GetName();
            perfil.ManutencaoTexto = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Manutencao", default(Int16))).GetName();
            perfil.FuncionarioTexto = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Funcionario", default(Int16))).GetName();
            perfil.ClienteTexto = PrivilegioCRUD.VISUALIZAR.GetFromCode(row.GetValue("Cliente", default(Int16))).GetName();

            daoFuncionarioDadosPessoais.GetListWhere("ID_Perfil", perfil.IDPerfil).ForEach(s => perfil.FuncionariosDadosPessoais.Add(s));
           
            return perfil;
        }

        public override Dictionary<string, object> ParseToParamenters(Perfil t)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("ID", t.IDPerfil);
            parms.Add("Nome", t.Nome);
            parms.Add("Atendimento", t.Atendimento);
            parms.Add("Manutencao", t.Manutencao);
            parms.Add("Funcionario", t.Funcionario);
            parms.Add("Cliente", t.Cliente);
            parms.Add("ID_Setor", t.Setor.IDSetor);
            return parms;
        }

        public Perfil VerificaPerfil(string perfil)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Perfil", perfil);

            using (DataTable dataTable = this.GetDataTable($"SELECT * FROM {this.GetTableName()} WHERE Nome = @Perfil", dic))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return null;
        }

        public override Perfil Save(Perfil t)
        {
            t = SaveSimple(t, (r, id) => r.IDPerfil = id, $"INSERT INTO {this.GetTableName()} (Nome,Atendimento,Manutencao,Funcionario,Cliente,ID_Setor)"
               + "output INSERTED.ID VALUES (@Nome,@Atendimento,@Manutencao,@Funcionario,@Cliente,@ID_Setor)", this.ParseToParamenters(t));
            if (t.FuncionariosDadosPessoais.Any())
            {
                t.FuncionariosDadosPessoais.Select(f => { return daoFuncionarioDadosPessoais.Save(f); }).ToList();
            }
            return t;
        }

        public override Perfil Update(Perfil t)
        {
            if (t.IDPerfil <= 0)
            {
                throw new ArgumentNullException("O campo IDPerfil não pode ser menor ou igual a 0");
            }

            t =  this.UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Nome = @Nome, Atendimento = @Atendimento, Manutencao = @Manutencao, Cliente = @Cliente, ID_Setor = @ID_Setor WHERE ID = @ID", ParseToParamenters(t));
            if (t.FuncionariosDadosPessoais.Any())
            {
                t.FuncionariosDadosPessoais.Select(f => { return daoFuncionarioDadosPessoais.SaveOrUpdate(f); }).ToList();
            }
            return t;
        }
    }
}