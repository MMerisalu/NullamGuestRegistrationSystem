using App.DAL.DTO;
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
        Task<List<TEntity?>> GetAllEventsOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
        List<TEntity?> GetAllEventsOrderedByName(bool noTracking = true, bool noIncludes = false);
        Task<TEntity?> GetEventByIdAsync(int id, bool noTracking = true, bool noIncludes = false);
        TEntity? GetEventById(int id, bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<TEntity?>> GetAllEventsDTOOrderedByNameAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<TEntity?> GetAllEventsDTOOrderedByName(bool noTracking = true, bool noIncludes = false);
        Task<int> GetNumberOfAttendeesAsync(int eventId, bool noTracking = true, bool noIncludes = false);
        int GetNumberOfAttendees(int eventId, bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<EventDTO?>> GetFutureEventsAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<EventDTO?> GetFutureEvents(bool noTracking = true, bool noIncludes = false);
        Task<IEnumerable<EventDTO?>> GetPastEventsAsync(bool noTracking = true, bool noIncludes = false);
        IEnumerable<EventDTO?> GetPastEvents(bool noTracking = true, bool noIncludes = false);
    }
}
