using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class AtendimentoDAOImpl : DAOImpl<Atendimento>, IAtendimentoDAO
    {

        private IClienteDAO daoCliente;
        private IAtendimentoDetalhadoDAO daoAtendimentoDetalhado;
        private IProblemaDAO daoProblema;

        public AtendimentoDAOImpl() : base("Atendimento", "IDAtendimento", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoCliente = DAOFactory.InitClienteDAO();
            daoAtendimentoDetalhado = DAOFactory.InitAtendimentoDetalhadoDAO();
            daoProblema = DAOFactory.InitProblemaDAO();
        }

        public override Atendimento parseToDTO(DataRow row)
        {
            Atendimento atendimento = new Atendimento();
            atendimento.IDAtendimento = row.GetValue("IDAtendimento", default(long));
            atendimento.Problema = row.GetValue("Problema", string.Empty);
            atendimento.Cliente = daoCliente.GetByPK(row.GetValue("Cliente_ID", default(long)));
            daoAtendimentoDetalhado.GetListWhere("Atendimento_ID", atendimento.IDAtendimento).ForEach(a => atendimento.AtendimentoDetalhado.Add(a));
            daoProblema.GetListWhere("Atendimento_ID", atendimento.IDAtendimento).ForEach(a => atendimento.Problemas.Add(a));
            return atendimento;
        }

        public override Dictionary<string, object> ParseToParamenters(Atendimento t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDAtendimento", t.IDAtendimento);
            dic.Add("Problema", string.IsNullOrEmpty(t.Problema) ? string.Empty : t.Problema);
            dic.Add("Protocolo_ID", t.Protocolo.IDProtocolo);
            dic.Add("Cliente_ID", t.Cliente.IDCliente);
            return dic;
        }

        public override Atendimento Save(Atendimento t)
        {
            t = SaveSimple(t, (r, id) => r.IDAtendimento = id, $"INSERT INTO {this.GetTableName()} (Problema,Protocolo_ID,Cliente_ID) output INSERTED.IDAtendimento VALUES (@Problema,@Protocolo_ID,@Cliente_ID)", this.ParseToParamenters(t));

            if (t.AtendimentoDetalhado != null)
            {
                t.AtendimentoDetalhado.Select(s => { return daoAtendimentoDetalhado.SaveOrUpdate(s); }).ToList();
            }

            if (t.Problemas != null)
            {
                t.Problemas.Select(s => { return daoProblema.SaveOrUpdate(s); }).ToList();
            }

            return t;
        }

        public override Atendimento Update(Atendimento t)
        {
            t = UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Problema = @Problema, Protocolo_ID = @Protocolo_ID, Cliente_ID = @Cliente_ID WHERE {this.GetPKColumnName()} = @IDAtendimento", this.ParseToParamenters(t));

            if (t.AtendimentoDetalhado != null)
            {
                t.AtendimentoDetalhado.Select(s => { return daoAtendimentoDetalhado.SaveOrUpdate(s); }).ToList();
            }

            if (t.Problemas != null)
            {
                t.Problemas.Select(s => { return daoProblema.SaveOrUpdate(s); }).ToList();
            }

            return t;
        }
    }
}
