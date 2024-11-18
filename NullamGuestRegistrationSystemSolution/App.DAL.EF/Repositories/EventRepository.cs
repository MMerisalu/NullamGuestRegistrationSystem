using App.Contracts.DAL.IAppRepositories;
using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.EF.Repositories
{
    public class EventRepository : BaseEntityRepository<Event, EventDTO, int, AppDbContext>, IEventRepository
    {
        public EventRepository(AppDbContext dbContext, IMapper<Event, EventDTO> mapper) : base(dbContext, mapper)
        {
        }

        public IEnumerable<EventDTO?> GetAllEventsDTOOrderedByName(bool noTracking = true, bool noIncludes = false)
        {
           return CreateQuery(noTracking, noIncludes).Select(e => Mapper.Map(e)).ToList();
        }

        public async Task<IEnumerable<EventDTO?>> GetAllEventsDTOOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
        {
            return(await CreateQuery(noTracking, noIncludes).Select(e => Mapper.Map(e)).ToListAsync());
        }

        public List<EventDTO?> GetAllEventsOrderedByName(bool noTracking = true, bool noIncludes = false)
        {
            return CreateQuery(noTracking, noIncludes).Select(e => Mapper.Map(e)).ToList();
        }

        public async Task<List<EventDTO?>> GetAllEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
        {
            return (await CreateQuery(noTracking, noIncludes).OrderBy(e => e.Name).Select(e => Mapper.Map(e)).ToListAsync());
        }

        public EventDTO? GetEventById(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(e => e.Id.Equals(id)));
        }

        public async Task<EventDTO?> GetEventByIdAsync(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(await base.CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public int NumberOfAttendeesPerEvent(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            return CreateQuery(noTracking, noIncludes).Count(a => a.Attendees!.Any(a => a.EventId == eventId));
        }

        public async Task<int> NumberOfAttendeesPerEventAsync(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            return (await CreateQuery(noTracking, noIncludes).CountAsync(a => a.Attendees!.Any(a => a.EventId == eventId)));
        }

        protected override IQueryable<Event> CreateQuery(bool noTracking = true, bool noIncludes = false)
        {
            var query = base.CreateQuery(noTracking, noIncludes);
            if (noTracking) query = query.AsNoTracking();
            if (noIncludes)
            {
                return query;
            }

            query = query.Include(a => a.Attendees);
                
            return query;
        }
    }
}
