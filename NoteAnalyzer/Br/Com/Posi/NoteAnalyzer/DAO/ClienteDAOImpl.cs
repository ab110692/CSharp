using Br.Com.Posi.NoteAnalyzer.Model;
using Br.Com.Posi.Shelf.DAO;
using NoteAnalyzer.Br.Com.Posi.NoteAnalyzer.Enums;
using System;
using System.Collections.Generic;
using System.Data;

namespace Br.Com.Posi.NoteAnalyzer.DAO
{
    class ClienteDAOImpl : DAOImpl<Cliente>, IClienteDAO
    {
        public ClienteDAOImpl() : base("TblClientes", "NumeroDoCliente","","iNET") { }

        public List<Cliente> ListPerState(String UF)
        {
            using (DataTable dataTable = this.GetDataTable(String.Format("select '0'+right(numerodocliente,3) as NumeroDoCliente,"
                    + "NomeDocliente, RazaoSocial, CPFCNPJ, InscEstRG, Telefone, Fax, Endereco,"
                    + "Numero, Complemento, Bairro, Cidade, Estado, Cep, Email, Contrato "
                    + "from {0} WHERE estado like '%{1}%'", GetTableName(), UF)))
            {
                List<Cliente> list = new List<Cliente>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }

        public List<Cliente> ListState()
        {
            using (DataTable dataTable = this.GetDataTable(String.Format("select distinct(estado) from {0} ", GetTableName())))
            {
                List<Cliente> list = new List<Cliente>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }

        public override Cliente parseToDTO(DataRow row)
        {
            Cliente cliente = new Cliente();
            if (row.Table.Columns.Contains("NumeroDoCliente"))
            {
                cliente.NumeroDoCliente = (String)row["NumeroDoCliente"];
            }
            if (row.Table.Columns.Contains("Estado"))
            {
                cliente.Estado = (String)row["Estado"];
            }
            if (row.Table.Columns.Contains("NomeDoCliente"))
            {
                cliente.NomeDoCliente = (String)row["NomeDoCliente"];
            }
            if (row.Table.Columns.Contains("CEP"))
            {
                cliente.CEP = (String)row["CEP"];
            }
            return cliente;
        }

        public override Dictionary<string, object> ParseToParamenters(Cliente t)
        {
            throw new NotImplementedException();
        }

        public override Cliente Save(Cliente t)
        {
            throw new NotImplementedException();
        }

        public override Cliente Update(Cliente t)
        {
            throw new NotImplementedException();
        }
    }
}
