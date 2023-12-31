using System.Linq.Expressions;
using Application.Repository.Generics.GenericsId;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public sealed class OrderRepository : GenericRepositoryA<Order>, IOrderRepository{
    private readonly PharmacyContext _context;
    public OrderRepository(PharmacyContext context) : base(context){

        _context = context;
    }
        
  protected override async Task<IEnumerable<Order>> GetAll(Expression<Func<Order, bool>> expression = null)
    {
        if (expression is not null)
        {
            return await _Entities
                .Include(x => x.Sale)        
                .Include(x => x.Eps)        
                
                .Where(expression).ToListAsync();
        }
        return await _Entities
                .Include(x => x.Sale)        
                .Include(x => x.Eps)        
            .ToListAsync();
    }



          public async Task<IEnumerable<Order>> GetAfterDateJanuary()
        {
            DateTime january1st2023 = new(2023, 1, 1);

            return await Task.FromResult(_Entities
                .Where(order => order.Order_Date > january1st2023)
                .AsEnumerable());
        }
}