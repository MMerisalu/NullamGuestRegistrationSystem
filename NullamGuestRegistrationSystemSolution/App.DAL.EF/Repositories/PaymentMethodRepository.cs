
using App.Contracts.DAL.IAppRepositories;
using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PaymentMethodRepository : BaseEntityRepository<PaymentMethod,PaymentMethodDTO, int, AppDbContext>, IPaymentMethodRepository
{
    public PaymentMethodRepository(AppDbContext dbContext, IMapper<PaymentMethod, PaymentMethodDTO> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<PaymentMethodDTO?>> GetAllPaymentMehodsOrderedByNameAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PaymentMethodDTO?> GetAllPaymentMethodsOrderedByName()
    {
        throw new NotImplementedException();
    }
}