
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
    }





}

    
