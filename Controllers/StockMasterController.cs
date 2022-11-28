using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Net;

namespace XactERPAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockMasterController : ControllerBase
{
    private readonly ILogger<StockMasterController> _logger;

    public StockMasterController(ILogger<StockMasterController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<StockMaster> Get()
    {
        using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM stock_master;";

            List<StockMaster> output = new List<StockMaster>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(new StockMaster
                    {
                        StockCode = reader.GetInt64(0),
                        StockDescription = reader.GetString(1),
                        Cost = reader.GetInt64(2),
                        SellingPrice = reader.GetDouble(3),
                        TotalPurchasesExclVat = reader.GetDouble(4),
                        TotalSalesExclVat = reader.GetDouble(5),
                        QtyPurchased = reader.GetInt64(6),
                        QtySold = reader.GetInt64(7),
                        StockOnHand = reader.GetInt64(8)
                    });
                }
            }
            return output;
        }
    }


    [HttpGet("search/{id}")]
    public IEnumerable<StockMaster> Get(string id)
    {
        using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
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
                    output.Add(new StockMaster
                    {
                        StockCode = reader.GetInt64(0),
                        StockDescription = reader.GetString(1),
                        Cost = reader.GetInt32(2),
                        SellingPrice = reader.GetDouble(3),
                        TotalPurchasesExclVat = reader.GetDouble(4),
                        TotalSalesExclVat = reader.GetDouble(5),
                        QtyPurchased = reader.GetInt64(6),
                        QtySold = reader.GetInt64(7),
                        StockOnHand = reader.GetInt64(8)
                    });
                }
            }
            return output;
        }
    }

    
    [HttpPost("insert")]
    public ActionResult Post(StockMaster newDebtor)
    {
        try
        {
            using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO stock_master VALUES (@StockCode, @StockDescription, @Cost, @SellingPrice, @TotalPurchasesExclVat, @TotalSalesExclVat, @QtyPurchased, @QtySold, @StockOnHand);";
                
                command.Parameters.AddWithValue("@StockCode", newDebtor.StockCode);
                command.Parameters.AddWithValue("@StockDescription", newDebtor.StockDescription);
                command.Parameters.AddWithValue("@Cost", newDebtor.Cost);
                command.Parameters.AddWithValue("@SellingPrice", newDebtor.SellingPrice);
                command.Parameters.AddWithValue("@TotalPurchasesExclVat", newDebtor.TotalPurchasesExclVat);
                command.Parameters.AddWithValue("@TotalSalesExclVat", newDebtor.TotalSalesExclVat);
                command.Parameters.AddWithValue("@QtyPurchased", newDebtor.QtyPurchased);
                command.Parameters.AddWithValue("@QtySold", newDebtor.QtySold);
                command.Parameters.AddWithValue("@StockOnHand", newDebtor.StockOnHand);

                command.ExecuteNonQuery();
            }
        }
        catch(SqliteException e){
            // if (e.SqliteErrorCode == 19)
            // {
            //     return StatusCode(StatusCodes.Status400BadRequest, "Debtor already exists.");
            // }
            
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return StatusCode(StatusCodes.Status200OK);
    }


    [HttpPut("edit")]
    public ActionResult Put(StockMaster newDebtor)
    {
        try
        {
            using (var connection = new SqliteConnection("Data Source=.\\Database\\AppData.db;"))
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

                command.Parameters.AddWithValue("@StockCode", newDebtor.StockCode);
                command.Parameters.AddWithValue("@StockDescription", newDebtor.StockDescription);
                command.Parameters.AddWithValue("@Cost", newDebtor.Cost);
                command.Parameters.AddWithValue("@SellingPrice", newDebtor.SellingPrice);
                command.Parameters.AddWithValue("@TotalPurchasesExclVat", newDebtor.TotalPurchasesExclVat);
                command.Parameters.AddWithValue("@TotalSalesExclVat", newDebtor.TotalSalesExclVat);
                command.Parameters.AddWithValue("@QtyPurchased", newDebtor.QtyPurchased);
                command.Parameters.AddWithValue("@QtySold", newDebtor.QtySold);
                command.Parameters.AddWithValue("@StockOnHand", newDebtor.StockOnHand);

                command.ExecuteNonQuery();
            }
        }

        catch(SqliteException e){
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return StatusCode(StatusCodes.Status200OK);
    }
}
