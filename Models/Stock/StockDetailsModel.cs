using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

//Model that represents stock transaction information.
public class StockDetailsModel
{
    [Required]
    public long InvoiceNo { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public long DebtorAccountCode { get; set; }

    [Required]
    public string? DebtorName { get; set; }

    [Required]
    public long PurchaseQty { get; set; }

    [Required]
    public double Total { get; set; }

}