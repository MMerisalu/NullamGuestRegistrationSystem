﻿using App.DAL.DTO;
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
        
        
        int GetEventAndAttendeeId(int eventId, int attendeeId, bool noTracking = true, bool noIncludes = false);
        
    }
}
