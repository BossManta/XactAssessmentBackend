using Microsoft.AspNetCore.Mvc;
using XactERPAssessment.Models;
using Microsoft.Data.Sqlite;
using static XactERPAssessment.StockMasterTools;

namespace XactERPAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockMasterController : ControllerBase
{
    private readonly ILogger<StockMasterController> _logger;
    private readonly IStockLogic _stockLogic;
    private readonly string DBConnectionsString;

    public StockMasterController(ILogger<StockMasterController> logger, IStockLogic stockLogic)
    {
        _logger = logger;
        _stockLogic = stockLogic;
        DBConnectionsString = DbConfig.ConnectionString;
    }

    //-------------------ENDPOINTS-----------------------
    [HttpGet]
    public IEnumerable<StockModel> Get()
    {
        return _stockLogic.Get(DBConnectionsString);
    }


    [HttpGet("search/{id}")]
    public IEnumerable<StockModel> Get(string id)
    {
        return _stockLogic.Search(DBConnectionsString, id);
    }

    
    [HttpPost("insert")]
    public ActionResult Post(StockModel newStock)
    {
        return _stockLogic.Insert(DBConnectionsString, newStock);
    }


    [HttpPut("edit")]
    public ActionResult Put(StockModel changedStock)
    {
        return _stockLogic.Edit(DBConnectionsString, changedStock);
    }

    [HttpPatch("addstock")]
    public ActionResult Patch(StockCount changedStock)
    {
        return _stockLogic.AddStock(DBConnectionsString, changedStock);
    }
    
    [HttpGet("details/{stockCode}")]
    public IEnumerable<StockDetailsModel> GetDetails(long stockCode)
    {
        return _stockLogic.Details(DBConnectionsString, stockCode);
    }
}
