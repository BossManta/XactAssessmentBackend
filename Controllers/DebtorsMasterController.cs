using Microsoft.AspNetCore.Mvc;

namespace XactERPAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebtorsMasterController : ControllerBase
{
    private readonly ILogger<DebtorsMasterController> _logger;
    private readonly IDebtorLogic _debtorLogic;
    private readonly string DBConnectionsString;

    public DebtorsMasterController(ILogger<DebtorsMasterController> logger, IDebtorLogic debtorLogic)
    {
        _logger = logger;
        _debtorLogic = debtorLogic;
        DBConnectionsString = DbConfig.ConnectionString;
    }


    //----------------ENDPOINTS-----------------------
    [HttpGet]
    public IEnumerable<DebtorsMaster> Get() {
         return _debtorLogic.Get(DBConnectionsString); 
    }

    [HttpGet("search/{id}")]
    public IEnumerable<DebtorsMaster> Get(string id) {
        return _debtorLogic.Search(DBConnectionsString, id);
    }
 
    [HttpPost("insert")]
    public ActionResult Post(DebtorsMaster newDebtor)
    {
        return _debtorLogic.Insert(DBConnectionsString, newDebtor);
    }

    [HttpPut("edit")]
    public ActionResult Put(DebtorsMaster newDebtor)
    {
        return _debtorLogic.Edit(DBConnectionsString, newDebtor);
    }
}
