using App.Contracts.DAL.IAppRepositories;
using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.EF.Repositories
{
    public class EventAndAttendeeRepository : BaseEntityRepository<EventAndAttendee, EventAndAttendeeDTO, int, AppDbContext>, IEventAndAttendeRepository
    {
        public EventAndAttendeeRepository(AppDbContext dbContext, IMapper<EventAndAttendee, EventAndAttendeeDTO> mapper) : base(dbContext, mapper)
        {
        }
    }
}
