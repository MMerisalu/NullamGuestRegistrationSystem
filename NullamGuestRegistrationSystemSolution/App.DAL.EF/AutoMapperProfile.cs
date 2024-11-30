
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
        CreateMap<Event, EventDTO>()
            .ForMember(dto => dto.NumberOfAttendees, db => db.MapFrom(d => d.Attendees.Sum(a => a.NumberOfPeople)))
            .ReverseMap();
        CreateMap<Attendee, AttendeeDTO>().ReverseMap();
        CreateMap<EventAndAttendee, EventAndAttendeeDTO>().ReverseMap();
    }





}

    
