using Microsoft.AspNetCore.Mvc;
using XactERPAssessment.Models;
using Microsoft.Data.Sqlite;

namespace XactERPAssessment.Controllers;

////////////////////////////////////////////////////////////////////////////
//This controller contains all the endpoints related to managing invoices.//
////////////////////////////////////////////////////////////////////////////

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;
    private readonly IInvoiceLogic _invoiceLogic;
    private readonly string DBConnectionsString;

    public InvoiceController(ILogger<InvoiceController> logger, IInvoiceLogic invoiceLogic)
    {
        _logger = logger;
        _invoiceLogic = invoiceLogic;
        DBConnectionsString = DbConfig.ConnectionString;
    }


    [HttpPut("preview")]
    public InvoiceDisplayModel PutPreview(InvoiceMinimalModel invoiceMinimal)
    {
        return _invoiceLogic.Preview(DBConnectionsString, invoiceMinimal);
    }

    [HttpPut("submit")]
    public InvoiceDisplayModel PutSubmit(InvoiceMinimalModel invoiceMinimal)
    {
        return _invoiceLogic.Submit(DBConnectionsString, invoiceMinimal);
    }
}
