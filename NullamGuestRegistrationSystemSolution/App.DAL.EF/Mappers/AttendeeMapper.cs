using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class AttendeeMapper : BaseMapper<App.Domain.Attendee, App.DAL.DTO.AttendeeDTO>
{
    public AttendeeMapper(IMapper mapper) : base(mapper)
    {
    }
}
