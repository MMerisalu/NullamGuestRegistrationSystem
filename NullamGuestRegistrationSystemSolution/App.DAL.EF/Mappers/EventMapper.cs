using AutoMapper;
using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.EF.Mappers
{
    public class EventMapper : BaseMapper<App.Domain.Event, App.DAL.DTO.EventDTO>
    {
        public EventMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
