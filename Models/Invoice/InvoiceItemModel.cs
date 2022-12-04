using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

public class InvoiceItemModel
{
    [Required]
    public long InvoiceNo { get; set; }
    
    [Required]
    public long ItemNo { get; set; }

    [Required]
    public long StockCode { get; set; }
    
    [Required]
    public long QtySold { get; set; }
    
    [Required]
    public double UnitCost { get; set; }
    
    [Required]
    public double CombinedCost { get; set; }
    
    [Required]
    public string? Disc { get; set; }
    
    [Required]
    public double Total { get; set; }
}