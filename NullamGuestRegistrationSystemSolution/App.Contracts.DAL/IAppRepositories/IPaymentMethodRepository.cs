using App.DAL.DTO;

using Base.Contracts.DAL;
using System.Threading.Tasks;

namespace App.Contracts.DAL.IAppRepositories;

public interface IPaymentMethodRepository: IEntityRepository<PaymentMethodDTO, int>, IPaymentMethodRepositoryCustom<PaymentMethodDTO>
{
    
}

public interface IPaymentMethodRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity?>> GetAllPaymentMehodsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
    IEnumerable<TEntity?> GetAllPaymentMethodsOrderedByName(bool noTracking = true, bool noIncludes = false);
    Task<TEntity?> GetPaymentMethodByIdAsync(int id,bool noTracking = true, bool noIncludes = false);
    TEntity? GetPaymentMethodById(int id, bool noTracking = true, bool noIncludes = false);


}