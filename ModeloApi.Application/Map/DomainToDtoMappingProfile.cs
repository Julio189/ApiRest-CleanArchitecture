
using AutoMapper;
using ModeloApi.Application.DTOs.Person;
using ModeloApi.Application.DTOs.Product;
using ModeloApi.Application.DTOs.Purchase;
using ModeloApi.Application.DTOs.User;
using ModeloApi.Domain.Entities;

namespace ModeloApi.Application.Map;
public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Person, CreatePersonDto>().ReverseMap();
        CreateMap<Person, UpdatePersonDto>().ReverseMap();
        CreateMap<Person, ReadPersonDto>().ReverseMap();

        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ReadProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();

        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, ReadUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();

        CreateMap<Purchase, UpdatePurchaseDto>().ReverseMap();
        CreateMap<Purchase, CreatePurchaseDto>().ReverseMap();
        CreateMap<Purchase, ReadPurchaseDto>()
            .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person.Name))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Name))
            .ReverseMap();

        //Outra opção de mapear o ReadPurchase, esse método é mais complexo, mas é mais completo, onde você tem total controle de como você quer mapear

        //CreateMap<Purchase, ReadPurchaseDto>()
        //    .ForMember(x => x.Person, opt => opt.Ignore())
        //    .ForMember(x => x.Product, opt => opt.Ignore())
        //    .ConstructUsing((model, context) =>
        //    {
        //        var dto = new ReadPurchaseDto
        //        {
        //            Product = model.Product.Name,
        //            Id = model.Id,
        //            Date = (DateTime)model.Date,
        //            Person = model.Person.Name

        //        };
        //        return dto;
        //    });


     
    }
}
