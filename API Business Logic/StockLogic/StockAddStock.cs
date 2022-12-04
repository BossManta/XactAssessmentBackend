using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;

public partial class StockLogic: IStockLogic
{
    public ActionResult AddStock(string DBConnectionsString, StockCount stockCount)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE stock_master SET stock_on_hand = stock_on_hand+@Count,
                                    total_purchases_excl_vat=total_purchases_excl_vat+(cost*@Count) 
                                    WHERE stock_code=@StockCode;";
            CommandObjectMapper.Map<StockCount>(command, stockCount);

            command.ExecuteNonQuery();
            return new OkObjectResult("All Good");
        }
    }
}