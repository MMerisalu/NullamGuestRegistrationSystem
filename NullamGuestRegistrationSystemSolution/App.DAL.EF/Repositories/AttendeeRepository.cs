using App.Contracts.DAL.IAppRepositories;
using App.DAL.DTO;
using App.Domain;
using App.Enum;
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
    public class AttendeeRepository : BaseEntityRepository<Attendee, AttendeeDTO, int, AppDbContext>, IAttendeeRepository
    {
        public AttendeeRepository(AppDbContext dbContext, IMapper<Attendee, AttendeeDTO> mapper) : base(dbContext, mapper)
        {
        }

        public List<AttendeeDTO?>? GetAllAttendeesOfEventOrderedByName(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            return CreateQuery(noTracking, noIncludes)
            .Where(a => a.Events.Any(e => e.EventId == eventId)) 
          .OrderBy(a => a.GivenName).ThenBy( a => a.SurName) 
          .Select(a => Mapper.Map(a)).ToList(); 

            
        }

        public async Task<List<AttendeeDTO?>>? GetAllAttendeesOfEventOrderedByNameAsync(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            var results =(await CreateQuery(noTracking, noIncludes).Where(a => a.Events.Any(e => e.EventId == eventId))
                .OrderBy(a => a.GivenName).ThenBy(a => a.SurName).Select(a => Mapper.Map(a)).ToListAsync());
            return results;
        }

        public IEnumerable<AttendeeDTO?>? GetAllAttendeesOrderedByName(bool noTracking = true, bool noIncludes = false)
        {
            return CreateQuery(noTracking, noIncludes).OrderBy(a => a.GivenName).ThenBy(a => a.SurName).Select(a => Mapper.Map(a)).ToList();
        }

        public async Task<IEnumerable<AttendeeDTO?>>? GetAllAttendeestOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
        {
            return(await CreateQuery(noTracking, noIncludes).OrderBy(a => a.GivenName).ThenBy(a => a.SurName).Select(a => Mapper.Map(a)).ToListAsync());
        }

        public async Task<AttendeeDTO?> GetAttendeByIdAsync(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(await CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(a => a.Id.Equals(id)));
        }

        public int? GetAttendeId(AttendeeType attendeType, string? surName, string? givenName, string? companyName, bool noTracking = true, bool noIncludes = false)
        {
            if (attendeType == AttendeeType.Person)
            {
                return CreateQuery(noTracking, noIncludes).SingleOrDefault(a => a.SurName.Equals(surName) && a.GivenName == givenName).Id;
            }
            else if (attendeType == AttendeeType.Company)
            {
                return CreateQuery(noTracking, noIncludes).SingleOrDefault(a => a.CompanyName.Equals(companyName)).Id;
            }
            // Should not get here
            return null;
        }

        public AttendeeDTO? GetEntityById(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(a => a.Id.Equals(id)));
        }



        

        protected override IQueryable<Attendee> CreateQuery(bool noTracking = true, bool noIncludes = false)
        {
            {
                var query = base.CreateQuery(noTracking, noIncludes);
                if (noTracking) query = query.AsNoTracking();
                if (!noIncludes)
                    query = query.Include(a => a.Events)
                        .Include(a => a.PaymentMethod);
                return query;
            }
        }
    }
}
