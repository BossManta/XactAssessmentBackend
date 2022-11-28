using Microsoft.Data.Sqlite;

namespace XactERPAssessment;

public static class DebtorsMasterTools
{
    //----------------------Reusable Tools---------------------
    public static void DebtorAddAllValuesToCommand(SqliteCommand command, DebtorsMaster debtor)
    {
        command.Parameters.AddWithValue("@AccountCode", debtor.AccountCode);
        command.Parameters.AddWithValue("@Address1", debtor.Address1);
        command.Parameters.AddWithValue("@Address2", debtor.Address2);
        command.Parameters.AddWithValue("@Address3", debtor.Address3);
        command.Parameters.AddWithValue("@Balance", debtor.Balance);
        command.Parameters.AddWithValue("@SalesYearToDate", debtor.SalesYearToDate);
        command.Parameters.AddWithValue("@CostYearToDate", debtor.CostYearToDate);
    }

    public static DebtorsMaster PopulateNewDebtorMasterFromReader(SqliteDataReader reader)
    {
        return new DebtorsMaster
        {
            AccountCode = reader.GetInt64(0),
            Address1 = reader.GetString(1),
            Address2 = reader.GetString(2),
            Address3 = reader.GetString(3),
            Balance = reader.GetFloat(4),
            SalesYearToDate = DateOnly.Parse(reader.GetString(5)),
            CostYearToDate = DateOnly.Parse(reader.GetString(6))
        };
    }

}