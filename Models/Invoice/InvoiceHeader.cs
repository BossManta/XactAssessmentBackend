using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment;

public class InvoiceHeader
{
    [Required]
    public long InvoiceNo { get; set; }

    [Required]
    public long AccountCode { get; set; }

    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public double TotalSellAmountExclVat { get; set; }

    [Required]
    public double Vat { get; set; }

    [Required]
    public double TotalCost { get; set; }
}