using System.ComponentModel.DataAnnotations;

namespace XactERPAssessment.Models;

//Model that contains all information in order to display invoice.
public class InvoiceDisplayModel
{
    [Required]
    public InvoiceGeneralModel? GeneralInfo { get; set; }
    
    [Required]
    public InvoiceItemModel[]? ItemInfo { get; set; }
    
    [Required]
    public DebtorModel? DebtorInfo { get; set; }
}