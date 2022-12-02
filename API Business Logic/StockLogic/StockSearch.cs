using Microsoft.Data.Sqlite;
using XactERPAssessment;
using static XactERPAssessment.StockMasterTools;

public partial class StockLogic: IStockLogic
{
    public IEnumerable<StockMaster> Search(string DBConnectionsString, string id)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM stock_master Where stock_code LIKE @SEARCH_CODE OR stock_description LIKE @SEARCH_DISC ;";
            command.Parameters.AddWithValue("@SEARCH_CODE", $"%{id}%");
            command.Parameters.AddWithValue("@SEARCH_DISC", $"%{id}%");

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