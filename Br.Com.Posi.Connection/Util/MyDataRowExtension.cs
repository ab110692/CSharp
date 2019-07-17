using System;
using System.Data;

namespace Br.Com.Posi.Connection.Util
{
    public static class MyDataRowExtension
    {
        public static T GetValue<T>(this DataRow dataRow, string column, T DefaultValue)
        {
            return dataRow.Table.Columns.Contains(column) ? dataRow[column] != DBNull.Value ? (T) dataRow[column] : DefaultValue : DefaultValue;
        }
    }
}
