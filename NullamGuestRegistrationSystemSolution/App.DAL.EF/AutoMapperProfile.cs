
using App.DAL.DTO;
using App.Domain;
using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile: Profile
{
    
    public AutoMapperProfile()
    {
        CreateMap<PaymentMethod, PaymentMethodDTO>()
                .ReverseMap();
        CreateMap<Event, EventDTO>().ReverseMap();
        CreateMap<Attendee, AttendeeDTO>().ReverseMap();
        CreateMap<EventAndAttendee, EventAndAttendeeDTO>().ReverseMap();
    }





}

    
