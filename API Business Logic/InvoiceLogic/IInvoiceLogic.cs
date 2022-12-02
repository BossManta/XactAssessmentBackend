using XactERPAssessment;

public interface IInvoiceLogic
{
    public InvoiceFull Preview(string DBConnectionsString, InvoiceFoundation foundation);
}