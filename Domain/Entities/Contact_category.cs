using Domain.Entities.Generics;

namespace Domain.Entities;

public class Contact_category : BaseEntityA
{
    public string Name {get;set;}
    
    public ICollection<Contact> Contacts {get;set;}
}
