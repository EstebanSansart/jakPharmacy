using System.ComponentModel.DataAnnotations;

namespace ApiJakPharmacy.Dtos;
public class SaleDto{
    [Required]
    public DateTime SaleDate { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int PersonId { get; set; }
}