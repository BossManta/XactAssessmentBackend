using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using XactERPAssessment;

public partial class StockLogic: IStockLogic
{
    public ActionResult Insert(string DBConnectionsString, StockMaster newStock)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO stock_master VALUES (@StockCode, @StockDescription, @Cost, @SellingPrice, @TotalPurchasesExclVat, @TotalSalesExclVat, @QtyPurchased, @QtySold, @StockOnHand);";
                
                CommandObjectMapper.Map<StockMaster>(command, newStock);

                command.ExecuteNonQuery();
            }
        }
        catch(SqliteException e)
        {    
            return new ObjectResult(e.Message){StatusCode=StatusCodes.Status500InternalServerError};
        }

        return new OkObjectResult("All Good");
    }
}