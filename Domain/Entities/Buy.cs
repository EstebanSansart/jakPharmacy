using Domain.Entities.Generics;

namespace Domain.Entities;

public class Buy : BaseEntityA 
{
  public DateTime BuyDate { get; set; }

  public int Provider_Id { get; set; }
  public Provider Provider { get; set; }

  public int Employee_Id { get; set; }
  public Employee Employee { get; set; }

  public ICollection<Medicine> Medicines {get; set;} = new HashSet<Medicine>();
  public ICollection<Detail_buy> Detail_Buys {get; set;}
}