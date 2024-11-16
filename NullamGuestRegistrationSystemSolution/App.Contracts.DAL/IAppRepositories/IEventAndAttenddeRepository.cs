using App.DAL.DTO;
using Base.Contracts.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.DAL.IAppRepositories
{
    public interface IEventAndAttendeRepository : IEntityRepository<EventAndAttendeeDTO, int>
    {
    }
}
