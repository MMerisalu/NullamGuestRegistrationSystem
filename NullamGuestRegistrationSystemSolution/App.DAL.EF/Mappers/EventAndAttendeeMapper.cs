using AutoMapper;
using Base.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EventAndAttendeeMapper : BaseMapper<App.Domain.EventAndAttendee, App.DAL.DTO.EventAndAttendeeDTO>
{
    public EventAndAttendeeMapper(IMapper mapper) : base(mapper)
    {
    }
}