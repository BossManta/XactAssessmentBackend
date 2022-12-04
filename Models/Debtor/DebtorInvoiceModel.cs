using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

public class DebtorInvoiceModel
{
    public long InvoiceNo { get; set; }
    public DateOnly Date { get; set; }
    public long ItemCount { get; set; }
    public Double Total { get; set; }
}
