using System.ComponentModel.DataAnnotations;
using XactERPAssessment;

namespace XactERPAssessment.Models;

public class InvoiceDisplayModel
{
    [Required]
    public InvoiceGeneralModel? GeneralInfo { get; set; }
    
    [Required]
    public InvoiceItemModel[]? ItemInfo { get; set; }
    
    [Required]
    public DebtorModel? DebtorInfo { get; set; }
}