//using App.Contracts.DAL;
//using App.Contracts.DAL.IAppRepositories;
//using App.DAL.EF.Mappers;
//using App.DAL.EF.Mappers.Identity;
//using App.DAL.EF.Repositories;
//using AutoMapper;
//using Base.DAL.EF;

//namespace App.DAL.EF;

//public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
//{
//    private readonly IMapper _mapper;
    
//    private ICountryRepository? _countries;
   

//    public AppUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
//    {
//        _mapper = mapper;
//    }

//    public virtual ICountryRepository Countries => _countries ??=
//        new CountryRepository(UOWDbContext, new CountryMapper(_mapper));
    
//}