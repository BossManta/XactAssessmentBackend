using XactERPAssessment.Models;

public interface IInvoiceLogic
{
    public InvoiceDisplayModel Preview(string DBConnectionsString, InvoiceMinimalModel foundation);
    public InvoiceDisplayModel Submit(string DBConnectionsString, InvoiceMinimalModel foundation);
}