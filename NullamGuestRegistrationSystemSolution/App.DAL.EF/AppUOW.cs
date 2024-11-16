using App.Contracts.DAL;
using App.Contracts.DAL.IAppRepositories;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;

    private IPaymentMethodRepository? _paymentMethods;
    private IEventRepository? _events;
    private IAttendeeRepository? _attendees;
    private IEventAndAttendeRepository? _eventsAndAttendees;

    public AppUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public virtual IPaymentMethodRepository PaymentMethods => _paymentMethods ??=
    new PaymentMethodRepository(UOWDbContext, new PaymentMethodMapper(_mapper));

    public virtual IEventRepository Events => _events ??=
    new EventRepository(UOWDbContext, new EventMapper(_mapper));

    public virtual IAttendeeRepository Attendees => _attendees 
        ??= new AttendeeRepository(UOWDbContext, new AttendeeMapper(_mapper));

    public virtual IEventAndAttendeRepository EventsAndAttendes => _eventsAndAttendees ??=
        new EventAndAttendeeRepository(UOWDbContext, new EventAndAttendeeMapper(_mapper));
}