using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace XactERPAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;
    private readonly string DBConnectionsString;

    public InvoiceController(ILogger<InvoiceController> logger)
    {
        _logger = logger;
        DBConnectionsString = DbConfig.ConnectionString;
    }


    [HttpPost("invoice/create")]
    public ActionResult Post(InvoiceFoundation foundation)
    {
        //---------------
        //------WIP------
        //---------------


        // StockMaster[] stockInfoArray = new StockMaster[foundation.StockCodeArray.Length];

        // using (var connection = new SqliteConnection(DBConnectionsString))
        // {
        //     connection.Open();

        //     for (int i=0; i<stockInfoArray.Length; i++)
        //     {
        //         var command = connection.CreateCommand();
        //         command.CommandText = "SELECT * FROM stock_master WHERE stock_code == @StockCode";
        //         command.Parameters.AddWithValue("@StockCode",foundation.StockCodeArray[i]);

        //         using (var reader = command.ExecuteReader())
        //         {
        //             stockInfoArray[i] = StockMasterTools.PopulateNewStockMasterFromReader(reader);
        //         }
        //     }
        // }


        // InvoiceHeader invoiceHeader = new InvoiceHeader();      
        // invoiceHeader.AccountCode = foundation.AccountCode;
        // invoiceHeader.Date = DateOnly.FromDateTime(DateTime.Now);
        // invoiceHeader.TotalSellAmountExclVat = 0;
        // for (int i=0;i<foundation.StockCodeArray.Length;i++)
        // {
        //     stockInfoArray[i].SellingPrice*foundation.StockCodeArray[i].
        // }

        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}
