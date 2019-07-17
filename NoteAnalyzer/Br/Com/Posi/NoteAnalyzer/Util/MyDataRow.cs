using System;
using System.Data;

namespace Br.Com.Posi.NoteAnalyzer.Util
{
    public static class MyDataRow
    {
        public static T GetValue<T>(this DataRow row, string column, T defaultValue)
        {
            return row.Table.Columns.Contains(column) ? row[column] != DBNull.Value ? (T)row[column] : defaultValue : defaultValue;
        }
    }
}
