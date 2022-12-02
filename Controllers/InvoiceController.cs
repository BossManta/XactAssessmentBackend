using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace XactERPAssessment.Controllers;

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


    [HttpPost("preview")]
    public InvoiceFull Post(InvoiceFoundation foundation)
    {
        return _invoiceLogic.Preview(DBConnectionsString, foundation);
    }
}
