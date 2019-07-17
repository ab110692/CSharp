using System;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection.Util;
using Br.Com.Posi.Connection;
using Br.Com.Posi.NotaFiscal.Model;

namespace Br.Com.Posi.NotaFiscal.DAO
{
    class NoteDAOImpl : DAOImpl<Note>, INoteDAO
    {
        public NoteDAOImpl() : base("Note", "", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }

        public override Note parseToDTO(DataRow row)
        {
            Note note = new Note();
            note.NumeroLoja = row.GetValue("NumeroLoja", string.Empty);
            note.NomeLoja = row.GetValue("NomeLoja", string.Empty);
            note.NotaInicial = row.GetValue("NotaInicial", string.Empty);
            note.NotaFinal = row.GetValue("NotaFinal", string.Empty);
            note.UltimaTransferencia = row.GetValue("UltimaTransferencia", string.Empty);
            note.QuantidadeNota = row.GetValue("QuantidadeNota", string.Empty);
            note.Faltantes = row.GetValue("Faltantes", string.Empty);
            note.Rede = row.GetValue("Rede", string.Empty);
            note.Estado = row.GetValue("Estado", string.Empty);
            note.Ano = row.GetValue("Ano", string.Empty);
            note.Mes = row.GetValue("Mes", string.Empty);
            note.Fiscal = row.GetValue("Fiscal", string.Empty);
            note.DataCriado = row.GetValue("DataCriado", string.Empty);
            return note;
        }

        public override Dictionary<string, object> ParseToParamenters(Note t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("NumeroLoja", t.NumeroLoja);
            dic.Add("NomeLoja", t.NomeLoja);
            dic.Add("NotaInicial", t.NotaInicial);
            dic.Add("NotaFinal", t.NotaFinal);
            dic.Add("UltimaTransferencia", t.UltimaTransferencia);
            dic.Add("QuantidadeNota", t.QuantidadeNota);
            dic.Add("Faltantes", t.Faltantes);
            dic.Add("Rede", t.Rede);
            dic.Add("Estado", t.Estado);
            dic.Add("Ano", t.Ano);
            dic.Add("Mes", t.Mes);
            dic.Add("Fiscal", t.Fiscal);
            dic.Add("DataCriado", String.Format("{0:dd/MM/yyyy}", DateTime.Now));
            return dic;
        }

        public override Note Save(Note t)
        {
            return this.SaveSimple(t, null, "INSERT INTO Note VALUES (@NumeroLoja, @NomeLoja, @NotaInicial, @NotaFinal, @UltimaTransferencia, @QuantidadeNota, @Faltantes, @Rede, @Estado, @Ano, @Mes, @Fiscal, @DataCriado)", this.ParseToParamenters(t));
        }

        public override Note Update(Note t)
        {
            throw new NotImplementedException();
        }
    }
}
