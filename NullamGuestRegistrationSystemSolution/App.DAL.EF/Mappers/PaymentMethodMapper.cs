
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PaymentMethodMapper : BaseMapper<App.Domain.PaymentMethod, App.DAL.DTO.PaymentMethodDTO>
{
    public PaymentMethodMapper(IMapper mapper) : base(mapper)
    {
    }
}