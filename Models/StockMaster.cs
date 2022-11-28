using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment;

public class StockMaster
{
    [Required]
    public long StockCode { get; set; }
    
    [Required]
    public string? StockDescription { get; set; }

    [Required]
    public double Cost { get; set; }

    [Required]
    public double SellingPrice { get; set; }

    [Required]
    public double TotalPurchasesExclVat { get; set; }

    [Required]
    public double TotalSalesExclVat { get; set; }

    [Required]
    public long QtyPurchased { get; set; }
    
    [Required]
    public long QtySold { get; set; }
    
    [Required]
    public long StockOnHand { get; set; }
}
