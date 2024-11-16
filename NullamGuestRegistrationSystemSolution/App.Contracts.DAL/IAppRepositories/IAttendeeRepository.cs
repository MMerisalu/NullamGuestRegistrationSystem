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
        Task<IEnumerable<TEntity?>> GetAllAttendeestOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<TEntity?> GetAllAttendeesOrderedByName( bool noTracking = true, bool noIncludes = false);
        //Task<int?> GetAttendeIdAsync(AttendeeType attendeeType,string? surName, string? givenName, string? companyName  bool noTracking = true, bool noIncludes = false);
        int? GetAttendeId(AttendeeType attendeeType,string? surName = null, string? givenName = null, string? companyName = null, bool noTracking = true, bool noIncludes = false);
        Task<TEntity?> GetAttendeByIdAsync(int id, bool noTracking = true, bool noIncludes = false);
        TEntity? GetEntityById(int id, bool noTracking = true, bool noIncludes = false);
    }
}
