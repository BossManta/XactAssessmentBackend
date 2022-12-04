using Microsoft.AspNetCore.Mvc;
using XactERPAssessment.Models;

public interface IDebtorLogic
{
    public IEnumerable<DebtorModel> Get(string DBConnectionsString);
    public IEnumerable<DebtorModel> Search(string DBConnectionsString, string id);
    public ActionResult Edit(string DBConnectionsString, DebtorModel changedDebtor);
    public ActionResult Insert(string DBConnectionsString, DebtorModel newDebtor);
    public IEnumerable<DebtorInvoiceModel> GetInvoices(string DBConnectionsString, long accountCode);
    public IEnumerable<InvoiceItemModel> GetInvoiceItems(string DBConnectionsString, long invoiceNo);
}