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
            return CreateQuery(noTracking, noIncludes).Where(a => a.Events!.Any(e => e.EventId == eventId)).Select(a => Mapper.Map(a)).ToList();
        }

        public async Task<List<AttendeeDTO?>>? GetAllAttendeesOfEventOrderedByNameAsync(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            var result = (await CreateQuery(noTracking, noIncludes).Where(a => a.Events!.Any(e => e.EventId == eventId)).Select(a => Mapper.Map(a)).ToListAsync());
            return result;
        }

        public IEnumerable<AttendeeDTO?> GetAllAttendeesOrderedByName(bool noTracking = true, bool noIncludes = false)
        {
            var result = CreateQuery(noTracking, noIncludes)
                .OrderBy(a => a.GivenName).ThenBy(a => a.SurName).
                OrderBy(a => a.CompanyName).Select(a => Mapper.Map(a)).ToList();
            return result ;
        }

        public async Task<IEnumerable<AttendeeDTO?>> GetAllAttendeesOrderedByNameAsync(bool noTracking = true, bool noIncludes = false)
        {
            return (await CreateQuery(noTracking, noIncludes)
               .OrderBy(a => a.GivenName).ThenBy(a => a.SurName).
               OrderBy(a => a.CompanyName).Select(a => Mapper.Map(a)).ToListAsync());
        }

        public IEnumerable<int>? GetAllEventsForAnAttendee(int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            var results = CreateQuery(noTracking, noIncludes).SelectMany(a => a.Events!.Where(a => a.AttendeeId == attendeeId).Select(a => a.Id).ToList());
            return results;
        }

        public AttendeeDTO? GetAttendeeById(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(a => a.Id == id));
        }

        public async Task<AttendeeDTO?> GetAttendeeByIdAsync(int id, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(await CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(a => a.Id == id));
        }

        public int? GetAttendeeId(AttendeeType attendeeType, string? surName = null, string? givenName = null, string? companyName = null, bool noTracking = true, bool noIncludes = false)
        {
            if (attendeeType == AttendeeType.Person)
            {
                var attendeeId = CreateQuery(noTracking, noIncludes).FirstOrDefault(a => a.SurName!.Equals(surName)
                 && givenName!.Equals(givenName))!.Id;
                return attendeeId;

            }
            else if (attendeeType == AttendeeType.Company)
            {
                var attendeeId = CreateQuery(noTracking, noIncludes)
                    .FirstOrDefault(a => a.CompanyName!.Equals(companyName))!.Id;
                return attendeeId;
            }
            // Should not get here
            return null;
        }

        public bool? IsAttendeeAlreadyRegistered(AttendeeType attendeeType, string? personalIdentifier = null, string? companyName = null, string? registeryCode = null, bool noTracking = true, bool noIncludes = false)
        {
            bool isRegistered = false;
            if (attendeeType == AttendeeType.Person)
            {
                isRegistered = CreateQuery(noTracking, noIncludes).Any(a => a.PersonalIdentifier!.Equals(personalIdentifier));
                return isRegistered;
            }
            else if (attendeeType == AttendeeType.Company)
            {
                isRegistered = CreateQuery(noTracking, noIncludes).Any(a => a.CompanyName!.Equals(companyName) && a.RegistryCode!.Equals(registeryCode));
                return isRegistered;
            }
            // Should not get here
            return null;
        }

        public async Task<bool?> IsAttendeeAlreadyRegisteredAsync(AttendeeType attendeeType, string? personalIdentifier = null, string? companyName = null, string? registeryCode = null, bool noTracking = true, bool noIncludes = false)
        {
            bool isRegistered = false;
            if (attendeeType == AttendeeType.Person)
            {
                isRegistered = await CreateQuery(noTracking, noIncludes).AnyAsync(a => a.PersonalIdentifier!.Equals(personalIdentifier));
                return isRegistered;
            }
            else if (attendeeType == AttendeeType.Company)
            {
                isRegistered = await CreateQuery(noTracking, noIncludes).AnyAsync(a => a.CompanyName!.Equals(companyName) || a.RegistryCode!.Equals(registeryCode));
                return isRegistered;
            }
            // Should not get here
            return null;
        }

        public bool IsAttendeeAttendingAnyEvents(int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            var result = CreateQuery(noTracking, noIncludes).Any(a => a.Events!.Any(a => a.AttendeeId == attendeeId));
            return result;
        }

        public async Task<bool> IsAttendeeAttendingAnyEventsAsync(int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            var result = await CreateQuery(noTracking, noIncludes).AnyAsync(a => a.Events!.Any(a => a.AttendeeId == attendeeId));
            return result;
        }

        public bool IsConnectedToAnyPaymentMethods(int paymentMethodId, bool noTracking = true, bool noIncludes = false)
        {
            return CreateQuery(noTracking, noIncludes).Any(a => a.PaymentMethodId == paymentMethodId);
        }

        public async Task<bool> IsConnectedToAnyPaymentMethodsAsync(int paymentMethodId, bool noTracking = true, bool noIncludes = false)
        {
            return await CreateQuery(noTracking, noIncludes).AnyAsync(a => a.PaymentMethodId == paymentMethodId);
        }

        public int NumberOfEventsForAttendee(int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            return CreateQuery(noTracking, noIncludes).SelectMany(e => e.Events!).Where(e => e.AttendeeId == attendeeId).Count();
        }

        public async Task<int> NumberOfEventsForAttendeeAsync(int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
           return await CreateQuery(noTracking, noIncludes).SelectMany(e => e.Events!).Where(e => e.AttendeeId == attendeeId).CountAsync();
        }

        protected override IQueryable<Attendee> CreateQuery(bool noTracking = true, bool noIncludes = false)
        {
            {
                var query = base.CreateQuery(noTracking, noIncludes);
                if (noTracking) query = query.AsNoTracking();
                if (!noIncludes)
                    query = query.Include(a => a.Events);
                return query;
            }
        }
    }
}
