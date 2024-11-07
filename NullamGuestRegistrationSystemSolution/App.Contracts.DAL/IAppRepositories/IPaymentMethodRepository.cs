using App.DAL.DTO;

using Base.Contracts.DAL;
using System.Threading.Tasks;

namespace App.Contracts.DAL.IAppRepositories;

public interface IPaymentMethodRepository: IEntityRepository<PaymentMethodDTO, int>, IPaymentMethodRepositoryCustom<PaymentMethodDTO>
{
    
}

public interface IPaymentMethodRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity?>> GetAllPaymentMehodsOrderedByNameAsync();
    IEnumerable<TEntity?> GetAllPaymentMethodsOrderedByName();
    



}