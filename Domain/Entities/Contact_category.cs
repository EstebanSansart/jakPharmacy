using Domain.Entities.Generics;

namespace Domain.Entities;

public class Contact_category : BaseEntityA
{

    //Email , PhoneNumber
    public string Name {get;set;}
    
    ICollection<Contact> Contacts {get;set;}
}
