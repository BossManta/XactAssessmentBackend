using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

public class StockCount
{
    [Required]
    public long StockCode { get; set; }

    [Required]
    public long Count { get; set; }
}

public class InvoiceMinimalModel
{
    [Required]
    public long AccountCode { get; set; }
    
    [Required]
    public StockCount[]? StockCountArray { get; set; }
}