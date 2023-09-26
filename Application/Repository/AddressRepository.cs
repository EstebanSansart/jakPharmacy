using System.Linq.Expressions;
using Application.Repository.Generics.GenericsId;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public sealed class AddressRepository : GenericRepositoryA<Address>, IAddressRepository{
    public AddressRepository(PharmacyContext context) : base(context){}
    
    protected override async Task<IEnumerable<Address>> GetAll(Expression<Func<Address, bool>> expression = null)
    {
        if (expression is not null)
        {
            return await _Entities
                .Include(x => x.Person)
                .Include(x => x.City)
                .Where(expression).ToListAsync();
        }
        return await _Entities
            .Include(x => x.Person)
            .Include(x => x.City)
            .ToListAsync();
    }
   
}