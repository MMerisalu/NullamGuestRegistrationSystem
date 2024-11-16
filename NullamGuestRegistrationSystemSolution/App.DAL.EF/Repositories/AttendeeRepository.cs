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
            return CreateQuery(noTracking, noIncludes).Where(a => a.Events.Any(e => e.EventId == eventId)).Select(a => Mapper.Map(a)).ToList();
        }

        public async Task<List<AttendeeDTO?>>? GetAllAttendeesOfEventOrderedByNameAsync(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            return (await CreateQuery(noTracking, noIncludes).Where(a => a.Events.Any(e => e.EventId == eventId)).Select(a => Mapper.Map(a)).ToListAsync());
        }

        public IEnumerable<AttendeeDTO?> GetAllAttendeesOrderedByName(bool noTracking = true, bool noIncludes = false)
        {
            return CreateQuery(noTracking, noIncludes)
                .OrderBy(a => a.GivenName).ThenBy(a => a.SurName).
                OrderBy(a => a.CompanyName).Select(a => Mapper.Map(a)).ToList();
        }

        public async Task<IEnumerable<AttendeeDTO?>> GetAllAttendeesOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
        {
            return (await CreateQuery(noTracking, noIncludes)
               .OrderBy(a => a.GivenName).ThenBy(a => a.SurName).
               OrderBy(a => a.CompanyName).Select(a => Mapper.Map(a)).ToListAsync());
        }

        public AttendeeDTO? GetAttendeeById(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(a => a.Id == id));
        }

        public async Task<AttendeeDTO?> GetAttendeeByIdAsync(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(await CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(a => a.Id == id));
        }

        public AttendeeDTO? GetAttendeeId(AttendeeType attendeeType, string? surName = null, string? givenName = null, string? companyName = null, bool noTracking = true, bool noIncludes = false)
        {
            if (attendeeType == AttendeeType.Person)
            {
                return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(a => a.SurName.Equals(surName) 
                 && givenName.Equals(givenName)));
            }
            else if (attendeeType == AttendeeType.Company)
            {
                return Mapper.Map(CreateQuery(noTracking, noIncludes)
                    .FirstOrDefault(a => a.CompanyName.Equals(companyName)));
            }
            // Should not get here
            return null;
        }

        public async Task<AttendeeDTO?> GetAttendeeIdAsync(AttendeeType attendeeType, string? surName, string? givenName, string? companyName, bool noTracking = true, bool noIncludes = false)
        {
            if (attendeeType == AttendeeType.Person)
            {
                var attendee = await CreateQuery(noTracking, noIncludes)
                .FirstOrDefaultAsync(a => a.SurName!.Equals(surName) && 
                a.GivenName!.Equals(givenName));
                return attendee != null ? Mapper.Map(attendee) : null;
            }
            else if (attendeeType == AttendeeType.Company)
            {
                var attendee = await CreateQuery(noTracking, noIncludes)
                    .FirstOrDefaultAsync(a => a.CompanyName!.Equals(companyName));
                return attendee != null ? Mapper.Map(attendee) : null;
            }
            // Should not get here
            return null;
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
