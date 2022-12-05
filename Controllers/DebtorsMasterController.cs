using Microsoft.AspNetCore.Mvc;
using XactERPAssessment.Models;

namespace XactERPAssessment.Controllers;

///////////////////////////////////////////////////////////////////////////
//This controller contains all the endpoints related to managing debtors.//
///////////////////////////////////////////////////////////////////////////


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
    public IEnumerable<DebtorModel> Get() {
         return _debtorLogic.Get(DBConnectionsString); 
    }

    [HttpGet("search/{id}")]
    public IEnumerable<DebtorModel> Get(string id) {
        return _debtorLogic.Search(DBConnectionsString, id);
    }
 
    [HttpPost("insert")]
    public ActionResult Post(DebtorModel newDebtor)
    {
        return _debtorLogic.Insert(DBConnectionsString, newDebtor);
    }

    [HttpPut("edit")]
    public ActionResult Put(DebtorModel newDebtor)
    {
        return _debtorLogic.Edit(DBConnectionsString, newDebtor);
    }

    [HttpGet("invoices/{accountCode}")]
    public IEnumerable<DebtorInvoiceModel> GetInvoices(long accountCode)
    {
        return _debtorLogic.GetInvoices(DBConnectionsString, accountCode);
    }

    [HttpGet("invoiceItems/{invoiceNo}")]
    public IEnumerable<InvoiceItemModel> GetInvoiceItems(long invoiceNo)
    {
        return _debtorLogic.GetInvoiceItems(DBConnectionsString, invoiceNo);
    }
}
