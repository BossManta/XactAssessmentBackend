using XactERPAssessment.Models;

//These are all actions primarily related to invoices.
public interface IInvoiceLogic
{
    public InvoiceDisplayModel Preview(string DBConnectionsString, InvoiceMinimalModel foundation);
    public InvoiceDisplayModel Submit(string DBConnectionsString, InvoiceMinimalModel foundation);
}