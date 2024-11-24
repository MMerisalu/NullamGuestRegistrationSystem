using App.Contracts.DAL.IAppRepositories;
using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.EF.Repositories
{
    public class EventAndAttendeeRepository : BaseEntityRepository<EventAndAttendee, EventAndAttendeeDTO, int, AppDbContext>, IEventAndAttendeeRepository
    {
        public EventAndAttendeeRepository(AppDbContext dbContext, IMapper<EventAndAttendee, EventAndAttendeeDTO> mapper) : base(dbContext, mapper)
        {
        }

        public EventAndAttendeeDTO? GetEventAndAttendeeDTO(int eventId, int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(ea => ea.EventId == eventId && ea.AttendeeId == attendeeId));
        }

        public async Task<EventAndAttendeeDTO?> GetEventAndAttendeeDTOAsync(int eventId, int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            var result = Mapper.Map(await CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(ea => ea.EventId == eventId && ea.AttendeeId == attendeeId));
            return result;
        }
    }
}
