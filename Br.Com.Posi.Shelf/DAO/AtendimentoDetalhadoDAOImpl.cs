using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Enums;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class AtendimentoDetalhadoDAOImpl : DAOImpl<AtendimentoDetalhado>, IAtendimentoDetalhadoDAO
    {
        private IFuncionarioDAO daoFuncionario;

        public AtendimentoDetalhadoDAOImpl() : base("AtendimentoDetalhado", "IDAtendimentoDetalhado", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoFuncionario = DAOFactory.InitFuncionarioDAO();
        }

        public override AtendimentoDetalhado parseToDTO(DataRow row)
        {
            AtendimentoDetalhado atendimentoDetalhado = new AtendimentoDetalhado();
            atendimentoDetalhado.IDAtendimentoDetalhado = row.GetValue("IDAtendimentoDetalhado", default(long));
            atendimentoDetalhado.StatusAtendimento = StatusAtendimento.Aguardando_Feedback.FromStatus((int)row.GetValue("StatusAtendimento", default(long)));
            atendimentoDetalhado.Funcionario = daoFuncionario.GetByPK(row.GetValue("Funcionario_ID", default(long)));
            atendimentoDetalhado.DataInicio = row.GetValue("DataInicio", default(DateTime));
            atendimentoDetalhado.DataFinal = row.GetValue("DataFinal", default(DateTime));
            atendimentoDetalhado.Solucao = row.GetValue("Solucao", string.Empty);
            atendimentoDetalhado.Contato = row.GetValue("Contato", string.Empty);
            return atendimentoDetalhado;
        }

        public override Dictionary<string, object> ParseToParamenters(AtendimentoDetalhado t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDAtendimentoDetalhado", t.IDAtendimentoDetalhado);
            dic.Add("Atendimento_ID", t.Atendimento.IDAtendimento);
            dic.Add("StatusAtendimento", t.StatusAtendimento.GetStatus());
            dic.Add("Funcionario_ID", t.Funcionario.IDFuncionarioLogin);
            dic.Add("DataInicio", t.DataInicio);
            dic.Add("DataFinal", t.DataFinal);
            dic.Add("Solucao", t.Solucao);
            dic.Add("Contato", t.Contato);
            return dic;
        }

        public override AtendimentoDetalhado Save(AtendimentoDetalhado t)
        {
            t = SaveSimple(t, (r, id) => r.IDAtendimentoDetalhado = id, $"INSERT INTO {this.GetTableName()} (Atendimento_ID,StatusAtendimento, Funcionario_ID, DataInicio, DataFinal, Solucao, Contato) output INSERTED.IDAtendimentoDetalhado VALUES (@Atendimento_ID, @StatusAtendimento, @Funcionario_ID, @DataInicio, @DataFinal, @Solucao, @Contato)", this.ParseToParamenters(t));
            
            return t;
        }

        public override AtendimentoDetalhado Update(AtendimentoDetalhado t)
        {
            t = ExecuteNonQuery($"UPDATE {this.GetTableName()} SET Atendimento_ID = @Atendimento_ID, StatusAtendimento = @StatusAtendimento, Funcionario_ID = @Funcionario_ID, DataInicio = @DataInicio, DataFinal = @DataFinal, Solucao = @Solucao, Contato = @Contato WHERE {this.GetPKColumnName()} = @IDAtendimentoDetalhado", this.ParseToParamenters(t)) > 0 ? t : default(AtendimentoDetalhado);
            
            return t;
        }
    }
}
