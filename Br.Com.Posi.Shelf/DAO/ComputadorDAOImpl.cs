using Br.Com.Posi.Shelf.Model;
using System;
using System.Collections.Generic;
using System.Data;
using Br.Com.Posi.Connection;
using Br.Com.Posi.Connection.Util;

namespace Br.Com.Posi.Shelf.DAO
{
    internal class ComputadorDAOImpl : DAOImpl<Computador>, IComputadorDAO
    {

        public ComputadorDAOImpl() : base("Computador", "IDComputador", FactoryConnection.BuildShelf(Connection.Enums.Modo.XML)) { }

        public override Computador parseToDTO(DataRow row)
        {
            Computador computador = new Computador();
            computador.IDComputador = row.GetValue("IDComputador", default(long));
            computador.Nome = row.GetValue("Nome", string.Empty);
            computador.LicencaWindows = row.GetValue("LicencaWindows", string.Empty);
            computador.LicencaAntiVirus = row.GetValue("LicencaAntiVirus", string.Empty);
            computador.MacAddress = row.GetValue("MacAddress", string.Empty);
            computador.DataAquisicaoAntiVirus = row.GetValue("DataAquisicaoAntiVirus", default(DateTime));
            computador.DataTerminoAntiVirus = row.GetValue("DataTerminoAntiVirus", default(DateTime));
            return computador;
        }

        public override Dictionary<string, object> ParseToParamenters(Computador t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IDComputador", t.IDComputador);
            dic.Add("Cliente_ID", t.Cliente.IDCliente);
            dic.Add("Nome", t.Nome);
            dic.Add("MSWindows_ID", t.MSWindows.IDWindows);
            dic.Add("LicencaWindows", t.LicencaWindows);
            dic.Add("AntiVirus_ID", t.AntiVirus.IDAntiVirus);
            dic.Add("LicencaAntiVirus", t.LicencaAntiVirus);
            dic.Add("MacAddress", t.MacAddress);
            dic.Add("DataAquisicaoAntiVirus", t.DataAquisicaoAntiVirus);
            dic.Add("DataTerminoAntiVirus", t.DataTerminoAntiVirus);
            return dic;
        }

        public override Computador Save(Computador t)
        {
            Computador computador = SaveSimple(t, (c, id) => c.IDComputador = id, $"INSERT INTO {this.GetTableName()} (Cliente_ID,Nome,MSWindows_ID,LicencaWindows,AntiVirus_ID,LicencaAntiVirus,MacAddress,DataAquisicaoAntiVirus,DataTerminoAntiVirus) output INSERTED.IDComputador VALUES "
               + $"(@Cliente_ID, @Nome, @MSWindows_ID, @LicencaWindows, @AntiVirus_ID, @LicencaAntiVirus, @MacAddress, @DataAquisicaoAntiVirus, @DataTerminoAntiVirus)", this.ParseToParamenters(t));
            return computador;
        }

        public override Computador Update(Computador t)
        {
            return ExecuteNonQuery($"UPDATE {this.GetTableName()} SET Cliente_ID = @Cliente_ID, Nome = @Nome, MSWindows_ID = @MSWindows_ID, LicencaWindows = @LicencaWindows, AntiVirus_ID = @AntiVirus_ID, LicencaAntiVirus = @LicencaAntiVirus, MacAddress = @MacAddress, DataAquisicaoAntiVirus = @DataAquisicaoAntiVirus, DataTerminoAntiVirus = @DataTerminoAntiVirus WHERE {this.GetPKColumnName()} = @IDComputador", this.ParseToParamenters(t)) > 0 ? t : default(Computador);
        }

        public List<Computador> List(Cliente cliente)
        {
            using (DataTable dataTable = this.GetDataTable($"select * from {this.GetTableName()} WHERE Cliente_ID = {cliente.IDCliente}"))
            {
                List<Computador> list = new List<Computador>();
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(this.parseToDTO(row));
                }
                return list;
            }
        }
    }
}
