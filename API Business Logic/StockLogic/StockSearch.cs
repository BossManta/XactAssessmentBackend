using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;
using static XactERPAssessment.StockMasterTools;

public partial class StockLogic: IStockLogic
{
    //Uses provided string to find a related stock item. Searches description and stock codes.
    public IEnumerable<StockModel> Search(string DBConnectionsString, string id)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM stock_master Where stock_code LIKE @SEARCH OR stock_description LIKE @SEARCH;";
            command.Parameters.AddWithValue("@SEARCH", $"%{id}%");

            List<StockModel> output = new List<StockModel>();
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