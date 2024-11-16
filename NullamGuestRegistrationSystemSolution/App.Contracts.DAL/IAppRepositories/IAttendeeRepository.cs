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
    public interface IAttendeeRepository : IEntityRepository<AttendeeDTO, int>,
    IAttendeeRepositoryCustom<AttendeeDTO>
    {

    }

    public interface IAttendeeRepositoryCustom<TEntity>
    {
        Task<List<TEntity?>>? GetAllAttendeesOfEventOrderedByNameAsync(int eventId, bool noTracking = true, bool noIncludes = false);
        List<TEntity?>? GetAllAttendeesOfEventOrderedByName(int eventId, bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<TEntity?>> GetAllAttendeesOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<TEntity?> GetAllAttendeesOrderedByName( bool noTracking = true, bool noIncludes = false);
        //Task<AttendeeDTO?> GetAttendeeIdAsync(AttendeeType attendeeType,string? surName=null, string? givenName = null, string? companyName = null, bool noTracking = true, bool noIncludes = false);
        int? GetAttendeeId(AttendeeType attendeeType,string? surName = null, string? givenName = null, string? companyName = null, bool noTracking = true, bool noIncludes = false);
        Task<TEntity?> GetAttendeeByIdAsync(int id, bool noTracking = true, bool noIncludes = false);
        TEntity? GetAttendeeById(int id, bool noTracking = true, bool noIncludes = false);

    }
}
