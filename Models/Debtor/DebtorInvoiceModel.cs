using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

//Model that represents simplifed invoice information related to a debtor.
public class DebtorInvoiceModel
{
    [Required]
    public long InvoiceNo { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public long ItemCount { get; set; }

    [Required]
    public double Total { get; set; }
}
