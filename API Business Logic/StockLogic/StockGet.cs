using Microsoft.Data.Sqlite;
using XactERPAssessment;
using static XactERPAssessment.StockMasterTools;

public partial class StockLogic: IStockLogic
{
    public IEnumerable<StockMaster> Get(string DBConnectionsString)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM stock_master;";

            List<StockMaster> output = new List<StockMaster>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(PopulateNewStockMasterFromReader(reader));
                }
            }
            return output;
        }
    }
}