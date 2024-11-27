using App.DAL.DTO;
using App.Enum;
using Base.Contracts.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.DAL.IAppRepositories
{
    public interface IEventAndAttendeeRepository : IEntityRepository<EventAndAttendeeDTO, int>, 
        IEventAndAttendeeRepositoryCustom<EventAndAttendeeDTO>
    {
    }

    public interface IEventAndAttendeeRepositoryCustom<TEntity>
    {
        Task <TEntity?> GetEventAndAttendeeDTOAsync(int eventId, int attendeeId, bool noTracking = true, bool noIncludes = false);
        TEntity? GetEventAndAttendeeDTO(int eventId, int attendeeId, bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<AttendeeDetailDTO?>> GetAllAttendeeDetailsDTOsByEventIdAsync(int eventId, bool noTracking = true, bool noIncludes = false);
        IEnumerable<AttendeeDetailDTO?> GetAllAttendeeDetailsDTOsByEventId(int eventId, bool noTracking = true, bool noIncludes = false);
    }
}
