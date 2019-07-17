using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    sealed class FuncionarioDAOImpl : DAOImpl<Funcionario>, IFuncionarioDAO
    {
      
        
        public FuncionarioDAOImpl() : base("FuncionarioLogin", "ID", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
           
        }

        public override Funcionario parseToDTO(DataRow row)
        {
            Funcionario funcionario = new Funcionario();            
            funcionario.IDFuncionarioLogin = row.GetValue("ID", default(long));            
            funcionario.Nome = row.GetValue("Nome", string.Empty);
            funcionario.Senha = Criptografia.Descriptografar(row.GetValue("Senha", string.Empty));            
            return funcionario;
        }

        public override Dictionary<string, object> ParseToParamenters(Funcionario t)
        {
            Dictionary<string, object> parms = new Dictionary<string, object>();
            parms.Add("ID", t.IDFuncionarioLogin);
            parms.Add("Nome", t.Nome);
            parms.Add("Senha", Criptografia.Criptografar(t.Senha));
            parms.Add("ID_FuncionarioDados", t.FuncionarioDadosPessoais.IDFuncionario);
            

            return parms;
        }

        public Funcionario Find(string user, string pass)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Nome", user);
            dic.Add("Senha", Criptografia.Criptografar(pass));
            using (DataTable dataTable = this.GetDataTable($"SELECT * FROM {this.GetTableName()} WHERE Nome = @Nome and Senha = @Senha", dic))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return null;
        }

        public Funcionario VerificaUsuario(string user)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Nome", user);

            using (DataTable dataTable = this.GetDataTable($"SELECT * FROM {this.GetTableName()} WHERE Nome = @Nome", dic))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return null;
        }

        public override Funcionario Save(Funcionario t)
        {
            Funcionario fun = SaveSimple(t, (r, id) => r.IDFuncionarioLogin = id, $"INSERT INTO {this.GetTableName()} (Nome,Senha,ID_FuncionarioDados)"
                + "output INSERTED.ID VALUES (@Nome, @Senha, @ID_FuncionarioDados)", this.ParseToParamenters(t));
            

            return fun;
        }

        public override Funcionario Update(Funcionario t)
        {
            if (t.IDFuncionarioLogin <= 0)
            {
                throw new ArgumentNullException("O campo IDFuncionario não pode ser menor ou igual a 0");
            }

            t = this.UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Nome = @Nome, Senha = @Senha, ID_FuncionarioDados = @ID_Funcionariodados WHERE ID = @ID", ParseToParamenters(t));


            


            //if (t.FuncionarioDadosPessoais != null)
            //{
            //    FuncionarioDadosPessoais funcionarioDadosPessoais = fun.FuncionarioDadosPessoais;
            //    funcionarioDadosPessoais.IDFuncionarioLogin = fun.IDFuncionarioLogin;
            //    funcionarioDadosPessoais = dao.SaveOrUpdate(t.FuncionarioDadosPessoais);
            //    t.FuncionarioDadosPessoais = funcionarioDadosPessoais;
            //}

            return t;
        }
    }
}

