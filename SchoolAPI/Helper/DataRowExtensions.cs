using System.Data;

namespace SchoolAPI.Helper
{
    public static class DataRowExtensions
    {
        public static string GetString(this DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value
                ? Convert.ToString(row[columnName]) ?? string.Empty
                : string.Empty;
        }

        public static int GetInt(this DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value
                ? Convert.ToInt32(row[columnName])
                : 0;
        }

        public static DateTime GetDate(this DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value
                ? Convert.ToDateTime(row[columnName])
                : DateTime.MinValue;
        }
    }

}
