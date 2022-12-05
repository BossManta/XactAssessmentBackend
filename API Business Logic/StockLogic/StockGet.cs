using Microsoft.Data.Sqlite;
using XactERPAssessment.Models;
using static XactERPAssessment.StockMasterTools;

public partial class StockLogic: IStockLogic
{
    
    //Retrieves all stock items from database and returns them
    public IEnumerable<StockModel> Get(string DBConnectionsString)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM stock_master;";

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