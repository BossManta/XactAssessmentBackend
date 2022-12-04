using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;

namespace XactERPAssessment;

public static class DebtorsMasterTools
{
    public static DebtorModel PopulateNewDebtorMasterFromReader(SqliteDataReader reader)
    {
        return new DebtorModel
        {
            AccountCode = reader.GetInt64(0),
            Name = reader.GetString(1),
            Address1 = reader.GetString(2),
            Address2 = reader.GetString(3),
            Address3 = reader.GetString(4),
            Balance = reader.GetDouble(5),
            SalesYearToDate = DateOnly.Parse(reader.GetString(6)),
            CostYearToDate = DateOnly.Parse(reader.GetString(7))
        };
    }

}