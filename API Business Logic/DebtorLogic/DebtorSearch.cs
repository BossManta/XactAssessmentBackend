using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;
using static XactERPAssessment.DebtorsMasterTools;

public partial class DebtorLogic: IDebtorLogic
{
    public IEnumerable<DebtorModel> Search(string DBConnectionsString, string id)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM debtors_master Where account_code LIKE @SEARCH ;";
            command.Parameters.AddWithValue("@SEARCH", $"%{id}%");

            List<DebtorModel> output = new List<DebtorModel>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(PopulateNewDebtorMasterFromReader(reader));
                }
            }
            return output;
        }
    }
}