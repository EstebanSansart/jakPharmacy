using Domain.Entities.Generics;

namespace Domain.Entities;
public class Type_person: BaseEntityA{
  public string Description { get; set; }

  public ICollection<Person> Persons {get; set;}
}