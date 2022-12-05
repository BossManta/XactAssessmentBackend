using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using XactERPAssessment;
using XactERPAssessment.Models;

public partial class StockLogic: IStockLogic
{
    //Takes stock count information and adds stock to relevant stock item.
    //Stock count information includes stock code and stock count. 
    public ActionResult AddStock(string DBConnectionsString, StockCount stockCount)
    {
        using (var connection = new SqliteConnection(DBConnectionsString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE stock_master SET stock_on_hand = stock_on_hand+@Count,
                                    total_purchases_excl_vat=total_purchases_excl_vat+(cost*@Count), 
                                    qty_purchased=qty_purchased+@Count
                                    WHERE stock_code=@StockCode;";
            CommandObjectMapper.Map<StockCount>(command, stockCount);

            command.ExecuteNonQuery();
            return new OkObjectResult("All Good");
        }
    }
}