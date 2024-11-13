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
        List<IEnumerable<TEntity?>> GetAllEventsOrderedByName(bool noTracking = true, bool noIncludes = false);
        Task<TEntity?> GetEventByIdAsync(int id, bool noTracking = true, bool noIncludes = false);
        TEntity? GetEventById(int id, bool noTracking = true, bool noIncludes = false);
       
    }
}
