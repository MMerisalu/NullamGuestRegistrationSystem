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

        public IEnumerable<AttendeeDetailDTO?> GetAllAttendeeDetailsDTOsByEventId(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            var result = CreateQuery(noTracking, noIncludes)
                                .Include(a => a.Attendee)
                                .Where(a => a.EventId == eventId)
                                .Select(a => new AttendeeDetailDTO
                                {
                                    Id = a.Id,
                                    AttendeeId = a.AttendeeId,
                                    EventId = a.EventId,
                                    Name = a.Attendee!.AttendeeType == Enum.AttendeeType.Person ? a.Attendee.GivenAndSurName : a.Attendee.CompanyName,
                                    Code = a.Attendee!.AttendeeType == Enum.AttendeeType.Person ? a.Attendee.PersonalIdentifier : a.Attendee.RegistryCode,
                                    AttendeeType = a.Attendee.AttendeeType,
                                    NumberOfPeople = a.Attendee.AttendeeType == Enum.AttendeeType.Company ? a.NumberOfPeople : 1
                                }).ToList();
            return result;
        }

        public async Task<IEnumerable<AttendeeDetailDTO?>> GetAllAttendeeDetailsDTOsByEventIdAsync(int eventId, bool noTracking = true, bool noIncludes = false)
        {
            var result = (await CreateQuery(noTracking, noIncludes)
                                .Include(a => a.Attendee)
                                .Where(a => a.EventId == eventId)
                                .Select(a => new AttendeeDetailDTO
                                {
                                    Id = a.Id,
                                    AttendeeId = a.AttendeeId,
                                    EventId = a.EventId,
                                    Name = a.Attendee!.AttendeeType == Enum.AttendeeType.Person ? a.Attendee.GivenAndSurName : a.Attendee.CompanyName,
                                    Code = a.Attendee!.AttendeeType == Enum.AttendeeType.Person ? a.Attendee.PersonalIdentifier : a.Attendee.RegistryCode,
                                    AttendeeType = a.Attendee.AttendeeType,
                                    NumberOfPeople = a.Attendee.AttendeeType == Enum.AttendeeType.Company ? a.NumberOfPeople : 1
                                }).ToListAsync());
            return result;
        }

        public EventAndAttendeeDTO? GetEventAndAttendeeDTO(int eventId, int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(CreateQuery(noTracking, noIncludes).FirstOrDefault(ea => ea.EventId == eventId && ea.AttendeeId == attendeeId));
        }

        public async Task<EventAndAttendeeDTO?> GetEventAndAttendeeDTOAsync(int eventId, int attendeeId, bool noTracking = true, bool noIncludes = false)
        {
            return Mapper.Map(await CreateQuery(noTracking, noIncludes).FirstOrDefaultAsync(ea => ea.EventId == eventId && ea.AttendeeId == attendeeId));
        }
    }
}
