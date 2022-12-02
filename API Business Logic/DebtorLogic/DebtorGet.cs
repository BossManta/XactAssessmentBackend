using Microsoft.Data.Sqlite;
using XactERPAssessment;
using static XactERPAssessment.DebtorsMasterTools;

public partial class DebtorLogic: IDebtorLogic
{
    public IEnumerable<DebtorsMaster> Get(string DBConnectionsString)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM debtors_master;";

            List<DebtorsMaster> output = new List<DebtorsMaster>();
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