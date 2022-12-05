using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;
using static XactERPAssessment.DebtorsMasterTools;

public partial class DebtorLogic: IDebtorLogic
{
    
    //Retrieves all debtors from database and returns them
    public IEnumerable<DebtorModel> Get(string DBConnectionsString)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM debtors_master;";

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