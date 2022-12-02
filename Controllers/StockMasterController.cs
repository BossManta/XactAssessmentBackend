using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<StockMaster> Get()
    {
        return _stockLogic.Get(DBConnectionsString);
    }


    [HttpGet("search/{id}")]
    public IEnumerable<StockMaster> Get(string id)
    {
        return _stockLogic.Search(DBConnectionsString, id);
    }

    
    [HttpPost("insert")]
    public ActionResult Post(StockMaster newStock)
    {
        return _stockLogic.Insert(DBConnectionsString, newStock);
    }


    [HttpPut("edit")]
    public ActionResult Put(StockMaster changedStock)
    {
        return _stockLogic.Edit(DBConnectionsString, changedStock);
    }
}
