using Microsoft.Data.Sqlite;

namespace XactERPAssessment;

public static class DebtorsMasterTools
{
    public static DebtorsMaster PopulateNewDebtorMasterFromReader(SqliteDataReader reader)
    {
        return new DebtorsMaster
        {
            AccountCode = reader.GetInt64(0),
            Address1 = reader.GetString(1),
            Address2 = reader.GetString(2),
            Address3 = reader.GetString(3),
            Balance = reader.GetDouble(4),
            SalesYearToDate = DateOnly.Parse(reader.GetString(5)),
            CostYearToDate = DateOnly.Parse(reader.GetString(6))
        };
    }

}