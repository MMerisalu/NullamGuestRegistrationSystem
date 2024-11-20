using App.Contracts.DAL.IAppRepositories;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IPaymentMethodRepository PaymentMethods{ get;}
    IEventRepository Events{ get;}
    IAttendeeRepository Attendees { get;}
    IEventAndAttendeeRepository EventsAndAttendes { get;}
    
}