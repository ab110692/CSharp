using Br.Com.Posi.Shelf.Model;
using System.Collections.Generic;
using Br.Com.Posi.Shelf.Enums;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;
using System.Linq;

namespace Br.Com.Posi.Shelf.DAO
{
    internal sealed class ProtocoloDAOImpl : DAOImpl<Protocolo>, IProtocoloDAO
    {

        private IAtendimentoDAO daoAtendimento;

        public ProtocoloDAOImpl() : base("Protocolo", "IDProtocolo", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML))
        {
            daoAtendimento = DAOFactory.InitAtendimentoDAO();
        }

        public override Protocolo parseToDTO(DataRow row)
        {
            Protocolo protocolo = new Protocolo();
            protocolo.IDProtocolo = row.GetValue("IDProtocolo", default(long));
            protocolo.NumeroProtocolo = row.GetValue("Protocolo", default(long));
            protocolo.TipoProtocolo.FromStatus(row.GetValue("Tipo", default(int)));
            if (protocolo.TipoProtocolo == TipoProtocolo.Atendimento)
            {
                protocolo.Atendimento = daoAtendimento.GetListWhere("Protocolo_ID", protocolo.IDProtocolo).FirstOrDefault();
            }
            return protocolo;
        }

        public override Dictionary<string, object> ParseToParamenters(Protocolo t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDProtocolo", t.IDProtocolo);
            dic.Add("Protocolo", t.NumeroProtocolo);
            dic.Add("Tipo", t.TipoProtocolo);
            return dic;
        }

        public override Protocolo Save(Protocolo t)
        {
            t = SaveSimple(t, (r, id) => r.IDProtocolo = id, $"INSERT INTO {this.GetTableName()} (Protocolo,Tipo) output INSERTED.IDProtocolo VALUES (@Protocolo,@Tipo)", this.ParseToParamenters(t));
            switch (t.TipoProtocolo)
            {
                case TipoProtocolo.Atendimento:
                    if (t.Atendimento != null)
                    {
                        t.Atendimento = daoAtendimento.SaveOrUpdate(t.Atendimento);
                    }
                    break;
            }
            return t;
        }

        public override Protocolo Update(Protocolo t)
        {
            t = UpdateSimple(t, $"UPDATE {this.GetTableName()} SET Protocolo = @Protocolo, Tipo = @Tipo WHERE {this.GetPKColumnName()} = @IDProtocolo", this.ParseToParamenters(t));
            switch (t.TipoProtocolo)
            {
                case TipoProtocolo.Atendimento:
                    if (t.Atendimento != null)
                    {
                        t.Atendimento = daoAtendimento.SaveOrUpdate(t.Atendimento);
                    }
                    break;
            }
            return t;
        }

        /// <summary>
        /// Retorno um protocolo para serviços prestado
        /// </summary>
        /// <param name="tipo">Tipo do serviço</param>
        /// <returns></returns>
        public Protocolo GerarProtocolo(TipoProtocolo tipo)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Tipo", tipo.GetTipo());

            using (DataTable dataTable = this.GetDataTable("GeraProcotolo @Tipo", dic))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    return this.parseToDTO(row);
                }
            }
            return default(Protocolo);
        }
    }
}
