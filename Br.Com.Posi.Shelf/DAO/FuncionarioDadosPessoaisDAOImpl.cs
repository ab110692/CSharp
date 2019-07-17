using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Enums;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    public sealed class FuncionarioDadosPessoaisDAOImpl : DAOImpl<FuncionarioDadosPessoais>, IFuncionarioDadosPessoaisDAO
    {
        private IFuncionarioDAO funcionarioDAO;
        public FuncionarioDadosPessoaisDAOImpl() : base("FuncionarioDadosPessoais", "ID", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            funcionarioDAO = new FuncionarioDAOImpl();
        }

        public override FuncionarioDadosPessoais parseToDTO(DataRow row)
        {
            FuncionarioDadosPessoais funcionarioDados = new FuncionarioDadosPessoais();
            funcionarioDados.IDFuncionario = row.GetValue("ID", default(long));
            funcionarioDados.NomeCompleto = row.GetValue("Nome", string.Empty);
            funcionarioDados.RG = row.GetValue("RG", string.Empty);
            funcionarioDados.CPF = row.GetValue("CPF", string.Empty);
            funcionarioDados.Telefone = row.GetValue("Telefone", string.Empty);
            funcionarioDados.Pis = row.GetValue("PIS", string.Empty);
            funcionarioDados.Email = row.GetValue("Email", string.Empty);
            funcionarioDados.CEP = row.GetValue("CEP", string.Empty);
            funcionarioDados.Endereco = row.GetValue("Endereco", string.Empty);
            funcionarioDados.Bairro = row.GetValue("Bairro", string.Empty);
            funcionarioDados.Numero = row.GetValue("Numero", string.Empty);
            funcionarioDados.Cidade = row.GetValue("Cidade", string.Empty);
            funcionarioDados.Estado = Estado.SAO_PAULO.FromNomeEstado(row.GetValue("Estado", string.Empty));

            funcionarioDAO.GetListWhere("ID_FuncionarioDados", funcionarioDados.IDFuncionario).ForEach(s => funcionarioDados.Funcionarios.Add(s));
            return funcionarioDados;
        }

        public override Dictionary<string, object> ParseToParamenters(FuncionarioDadosPessoais t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ID", t.IDFuncionario);
            dic.Add("Nome", t.NomeCompleto);
            dic.Add("RG", t.RG);
            dic.Add("CPF", t.CPF);
            dic.Add("Telefone", t.Telefone);
            dic.Add("PIS", t.Pis);
            dic.Add("Email", t.Email);
            dic.Add("CEP", t.CEP);
            dic.Add("Endereco", t.Endereco);
            dic.Add("Bairro", t.Bairro);
            dic.Add("Numero", t.Numero);
            dic.Add("Cidade", t.Cidade);
            dic.Add("Estado", t.Estado.GetNomeEstado());
            dic.Add("ID_Perfil", t.Perfil.IDPerfil);

            return dic;
        }

        public override FuncionarioDadosPessoais Save(FuncionarioDadosPessoais t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("Funcionário não pode ser nulo");
            }

            t = SaveSimple(t, (d, id) => d.IDFuncionario = id, $"INSERT INTO {this.GetTableName()} (Nome, RG, CPF, Telefone, PIS, Email, CEP, Endereco, Bairro, Numero, Cidade, Estado, ID_Perfil)"
                + $"output INSERTED.ID VALUES (@Nome, @RG, @CPF, @Telefone, @PIS, @Email, @CEP, @Endereco, @Bairro, @Numero, @Cidade, @Estado, @ID_Perfil)", this.ParseToParamenters(t));
            if (t.Funcionarios.Any())
            {
                t.Funcionarios.Select(f => { return funcionarioDAO.Save(f); }).ToList();
            }
            return t;
        }

        public override FuncionarioDadosPessoais Update(FuncionarioDadosPessoais t)
        {

            if (t == null)
            {
                throw new ArgumentNullException("Funcionário não pode ser nulo");
            }

            if (t.IDFuncionario <= 0)
            {
                throw new ArgumentNullException("O campo IDFuncionarioDadosPessoais não pode ser menor ou igual a 0");
            }

            t = UpdateSimple(t, $"UPDATE {this.GetTableName()} SET ID = @ID_FuncionarioLogin, Nome = @Nome, RG = @RG, CPF = @CPF, Telefone = @Telefone, PIS = @PIS, Email = @Email, CEP = @CEP, Endereco = @Endereco, Bairro = @Bairro, Numero = @Numero, Cidade = @Cidade, Estado = @Estado, ID_Perfil = @ID_Perfil Where {this.GetPKColumnName()} = @ID", this.ParseToParamenters(t));
            if (t.Funcionarios.Any())
            {
                t.Funcionarios.Select(f => { return funcionarioDAO.SaveOrUpdate(f); }).ToList();
            }
            return t;
        }
    }
}
