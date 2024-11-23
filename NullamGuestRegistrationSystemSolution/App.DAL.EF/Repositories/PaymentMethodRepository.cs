
using App.Contracts.DAL.IAppRepositories;
using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace App.DAL.EF.Repositories;

public class PaymentMethodRepository : BaseEntityRepository<PaymentMethod,PaymentMethodDTO, int, AppDbContext>, IPaymentMethodRepository
{
    public PaymentMethodRepository(AppDbContext dbContext, IMapper<PaymentMethod, PaymentMethodDTO> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<PaymentMethodDTO?>> GetAllPaymentMehodsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
    {
        return(await CreateQuery(noTracking, noIncludes).OrderBy(p => p.Name).Select(p => Mapper.Map(p)).ToListAsync());
    }


    public IEnumerable<PaymentMethodDTO?> GetAllPaymentMethodsOrderedByName(bool noTracking = true, bool noIncludes = false)
    {
        return CreateQuery(noTracking, noIncludes).OrderBy(p => p.Name).Select(p => Mapper.Map(p));
    }

    public PaymentMethodDTO? GetPaymentMethodById(int id, bool noTracking = true, bool noIncludes = false)
    {
        return Mapper.Map(CreateQuery(noTracking, noIncludes).First(p => p.Id.Equals(id)));
    }

    public async Task<PaymentMethodDTO?> GetPaymentMethodByIdAsync(int id, bool noTracking = true, bool noIncludes = false)
    {
        return Mapper.Map( await CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(p => p.Id.Equals(id)));
        
    }

    

    protected override IQueryable<PaymentMethod> CreateQuery(bool noTracking = true, bool noIncludes = false)
    {
        
        if (noTracking)
        {
            return RepoDbSet
                .AsNoTracking();
        }

        if (noIncludes)
        {
            return RepoDbSet;

        }

        return RepoDbSet
            .AsNoTracking();

    }
}