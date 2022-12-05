using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

//Class to store both stock code and item count.
public class StockCount
{
    [Required]
    public long StockCode { get; set; }

    [Required]
    public long Count { get; set; }
}

//Model that represents stripped down invoice.
//Only contains debtor account code and stock codes.
public class InvoiceMinimalModel
{
    [Required]
    public long AccountCode { get; set; }
    
    [Required]
    public StockCount[]? StockCountArray { get; set; }
}