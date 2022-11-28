using System.ComponentModel.DataAnnotations;

public class InvoiceDetail
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
    public double UnitSell { get; set; }
    
    [Required]
    public string? Disc { get; set; }
    
    [Required]
    public double Total { get; set; }
}