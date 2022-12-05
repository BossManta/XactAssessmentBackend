using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;

public partial class StockLogic: IStockLogic
{
    //Inserts given stock item into database
    public ActionResult Insert(string DBConnectionsString, StockModel newStock)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO stock_master VALUES (@StockCode, @StockDescription, @Cost, @SellingPrice, @TotalPurchasesExclVat, @TotalSalesExclVat, @QtyPurchased, @QtySold, @StockOnHand);";
                
                CommandObjectMapper.Map<StockModel>(command, newStock);

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