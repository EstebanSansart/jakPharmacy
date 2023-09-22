using Domain.Entities.Generics;

namespace Domain.Entities;

public class Document_type : BaseEntityA
{
    public string Description {get;set;}
    public ICollection<Person> Persons {get;set;}
}
