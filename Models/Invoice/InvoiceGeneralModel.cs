using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

public class InvoiceGeneralModel
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