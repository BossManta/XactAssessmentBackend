using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;

public partial class StockLogic: IStockLogic
{
    public ActionResult Edit(string DBConnectionsString, StockModel changedStock)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"UPDATE stock_master SET 
                                        stock_description = @StockDescription,
                                        cost = @Cost,
                                        selling_price = @SellingPrice,
                                        total_purchases_excl_vat = @TotalPurchasesExclVat,
                                        total_sales_excl_vat = @TotalSalesExclVat,
                                        qty_purchased = @QtyPurchased,
                                        qty_sold = @QtySold,
                                        stock_on_hand = @StockOnHand
                                        WHERE stock_code = @StockCode;";

                CommandObjectMapper.Map<StockModel>(command, changedStock);

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