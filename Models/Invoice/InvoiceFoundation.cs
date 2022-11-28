using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment;

public class StockCount
{
    public long StockCode { get; set; }
    public long Count { get; set; }
}

public class InvoiceFoundation
{
    [Required]
    public long AccountCode { get; set; }
    
    [Required]
    public StockCount[]? StockCountArray { get; set; }
}