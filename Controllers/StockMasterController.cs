using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using static XactERPAssessment.StockMasterTools;

namespace XactERPAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockMasterController : ControllerBase
{
    private readonly ILogger<StockMasterController> _logger;
    private readonly string DBConnectionsString;

    public StockMasterController(ILogger<StockMasterController> logger)
    {
        _logger = logger;
        DBConnectionsString = DbConfig.ConnectionString;
    }

    //-------------------ENDPOINTS-----------------------
    [HttpGet]
    public IEnumerable<StockMaster> Get()
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


    [HttpGet("search/{id}")]
    public IEnumerable<StockMaster> Get(string id)
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

    
    [HttpPost("insert")]
    public ActionResult Post(StockMaster newStock)
    {
        try
        {
            using (var connection = new SqliteConnection(DBConnectionsString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO stock_master VALUES (@StockCode, @StockDescription, @Cost, @SellingPrice, @TotalPurchasesExclVat, @TotalSalesExclVat, @QtyPurchased, @QtySold, @StockOnHand);";
                
                StockAddAllValuesToCommand(command, newStock);

                command.ExecuteNonQuery();
            }
        }
        catch(SqliteException e)
        {    
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return StatusCode(StatusCodes.Status200OK);
    }


    [HttpPut("edit")]
    public ActionResult Put(StockMaster newStock)
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

                StockAddAllValuesToCommand(command, newStock);

                command.ExecuteNonQuery();
            }
        }
        catch(SqliteException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        return StatusCode(StatusCodes.Status200OK);
    }
}
