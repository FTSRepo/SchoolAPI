using Microsoft.Data.SqlClient;

public static class SqlDataReaderExtensions
    {
    public static string GetSafeString(this SqlDataReader reader, string columnName)
        {
        var val = reader [columnName];
        return val != DBNull.Value && !string.IsNullOrWhiteSpace(val.ToString()) ? val.ToString() : string.Empty;
        }

    public static int GetSafeInt(this SqlDataReader reader, string columnName)
        {
        var val = reader [columnName];
        return val != DBNull.Value && !string.IsNullOrWhiteSpace(val.ToString()) ? Convert.ToInt32(val) : 0;
        }
    }