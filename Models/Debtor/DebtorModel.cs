using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

//Model that represents debtor information.
public class DebtorModel
{
    [Required]
    public long AccountCode { get; set; }

    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Address1 { get; set; }

    [Required]
    public string? Address2 { get; set; }

    [Required]
    public string? Address3 { get; set; }

    [Required]
    public double Balance { get; set; }

    [Required]
    public double SalesYearToDate { get; set; }
    
    [Required]
    public double CostYearToDate { get; set; }
}
