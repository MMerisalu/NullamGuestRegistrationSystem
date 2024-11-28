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
    public interface IEventRepository : IEntityRepository<EventDTO, int>,
    IEventRepositoryCustom<EventDTO>
    {

    }

    public interface IEventRepositoryCustom<TEntity>
    {
        Task<List<TEntity?>> GetAllEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true);
        List<TEntity?> GetAllEventsOrderedByName(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true);
        Task<TEntity?> GetEventByIdAsync(int id, bool noTracking = true, bool noIncludes = false/*, bool showPastEvents = true*/);
        TEntity? GetEventById(int id, bool noTracking = true, bool noIncludes = false/*, bool showPastEvents = true*/);
        Task<IEnumerable<TEntity?>> GetAllEventsDTOOrderedByNameAsync(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true);
        IEnumerable<TEntity?> GetAllEventsDTOOrderedByName(bool noTracking = true, bool noIncludes = false, bool showPastEvents = true);
        int NumberOfAttendeesPerEvent(int eventId, bool noTracking = true, bool noIncludes = false);
        IEnumerable<TEntity?> GetAllFutureEventsOrderedByTimeAndName(int attendeeId,bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<TEntity?>> GetAllFutureEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<TEntity?> GetAllFutureEventsOrderedByName(bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<TEntity?>> GetAllFutureEventsOrderedByTimeAndNameAsync(int attendeeId, bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<TEntity?>> GetAllPastEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<TEntity?> GetAllPastEventsOrderedByName(bool noTracking = true, bool noIncludes = false);
    }
}
