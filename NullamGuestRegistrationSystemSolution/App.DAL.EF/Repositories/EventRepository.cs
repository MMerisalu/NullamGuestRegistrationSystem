using App.Contracts.DAL.IAppRepositories;
using App.DAL.DTO;
using App.Domain;
using App.Enum;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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


        public IEnumerable<EventDTO?> GetAllEventsDTOOrderedByName(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true)
        {
            if (showPastEvents == false)
            {
                return CreateQuery(noTracking, noIncludes).Where(e => e.EventDateAndTime >= DateTime.Now).Select(e => Mapper.Map(e)).ToList();
            }
            else
            {
                return CreateQuery(noTracking, noIncludes).Select(e => Mapper.Map(e)).ToList();
            }

        }

        
        public async Task<IEnumerable<EventDTO?>> GetAllEventsDTOOrderedByNameAsync(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true)
        {
            if (showPastEvents == false)
            {
                return (await CreateQuery(noTracking, noIncludes).Where(e => e.EventDateAndTime >= DateTime.Now).Select(e => Mapper.Map(e)).ToListAsync());
            }
            else
            {
                return (await CreateQuery(noTracking, noIncludes).Select(e => Mapper.Map(e)).ToListAsync());
            }
        }

        
        public List<EventDTO?> GetAllEventsOrderedByName(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true)
        {
            if (showPastEvents == false)
            {
                return CreateQuery(noTracking, noIncludes).Where(e => e.EventDateAndTime >= DateTime.Now).Select(e => Mapper.Map(e)).ToList();
            }
            else
            {
                return CreateQuery(noTracking, noIncludes).Select(e => Mapper.Map(e)).ToList();
            }
        }

        public async Task<List<EventDTO?>> GetAllEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true)
        {
            if (showPastEvents == false)
            {
                return (await CreateQuery(noTracking, noIncludes).OrderBy(e => e.Name).Where(e => e.EventDateAndTime > DateTime.Now).Select(e => Mapper.Map(e)).ToListAsync());
            }

            else
            {
                return (await CreateQuery(noTracking, noIncludes).OrderBy(e => e.Name).Select(e => Mapper.Map(e)).ToListAsync());
            }
        
            
        }

        public IEnumerable<EventDTO?> GetAllFutureEventsOrderedByName(bool noTracking = true, bool noIncludes = false)
        {
            var currentDateTime = DateTime.Now;
            var futureEvents = CreateQuery(noTracking, noIncludes)
                .OrderBy(e => e.Name)
                .Where(e => (e.EventDateAndTime >= currentDateTime)).Select(e => Mapper.Map(e)).ToList();
            return futureEvents;
        }
        

        public async Task<IEnumerable<EventDTO?>> GetAllFutureEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
        {
            var currentDateTime = DateTime.Now;
            var futureEvents = await CreateQuery(noTracking, noIncludes)
                .OrderBy(e => e.Name)
                .Where(e => (e.EventDateAndTime >= currentDateTime)).Select(e => Mapper.Map(e)).ToListAsync();
            return futureEvents;
        }

        public IEnumerable<EventDTO?> GetAllFutureEventsOrderedByTimeAndName(int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            var currentDateTime = DateTime.Now;
            var futureEvents = CreateQuery(noTracking, noIncludes).OrderBy(e => e.EventDateAndTime.Date)
                    .ThenBy(e => e.EventDateAndTime.Hour)
                    .ThenBy(e => e.EventDateAndTime.Minute)
                    .ThenBy(e => e.Name).Select(e => e)
                .Where(e => (e.EventDateAndTime >= currentDateTime) && 
                    !e.Attendees!.Any(e => e.AttendeeId == attendeeId))
                .ToList();
            return futureEvents.Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<EventDTO?>> GetAllFutureEventsOrderedByTimeAndNameAsync(int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            var currentDateTime = DateTime.Now;
            var futureEvents = await CreateQuery(noTracking, noIncludes)
                .OrderBy(e => e.EventDateAndTime.Date)
                    .ThenBy(e => e.EventDateAndTime.Hour)
                    .ThenBy(e => e.EventDateAndTime.Minute)
                    .ThenBy(e => e.Name)
                .Where(e => (e.EventDateAndTime >= currentDateTime)).Select(e => Mapper.Map(e)).ToListAsync();
            return futureEvents;
        }

        

        public IEnumerable<EventDTO?> GetAllPastEventsOrderedByName(bool noTracking = true, bool noIncludes = false)
        {
            var currentDateTime = DateTime.Now;
            var pastEvents = CreateQuery(noTracking, noIncludes)
                .OrderBy(e => e.Name)
                .Where(e => (e.EventDateAndTime < currentDateTime)).Select(e => Mapper.Map(e)).ToList();
            return pastEvents;
            
        }

        public async Task<IEnumerable<EventDTO?>> GetAllPastEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
        {
            var currentDateTime = DateTime.Now;
            var pastEvents = await CreateQuery(noTracking, noIncludes)
                .OrderBy(e => e.Name)
                .Where(e => (e.EventDateAndTime < currentDateTime)).Select(e => Mapper.Map(e)).ToListAsync();
            return pastEvents;
        }

        public EventDTO? GetEventById(int id, bool noTracking = true, bool noIncludes = false)
        {
            
          return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(e => e.Id.Equals(id) && e.EventDateAndTime >= DateTime.Now));
            
            
        }

        public async Task<EventDTO?> GetEventByIdAsync(int id, bool noTracking = true, bool noIncludes = false)
        {
         var result = Mapper.Map(await base.CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(e => e.Id.Equals(id) && e.EventDateAndTime >= DateTime.Now));
           return result;
            
        }

       
        
           public int NumberOfAttendeesPerEvent(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            var result = RepoDbContext.Events.Where(e => e.Id == eventId)
                                       .SelectMany(x => x.Attendees!)
                                       .Sum(x => x.NumberOfPeople);

            return result;
        
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
