using AutoMapper;
using BL.DTO.Entities;
using Domains.Entities;
using Domains.Entities.Product;
using Domains.Order;
using Microsoft.Extensions.Options;

namespace BL.Mapper
{
    // Main mapping profile file (MappingProfile.cs)
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<TbAuthor, AuthorDto>().ReverseMap();
            //CreateMap<TbRefreshToken, RefreshTokenDto>().ReverseMap();
            //CreateMap<ApplicationUser, UserRegistrationDto>().ReverseMap();
            //CreateMap<VwBook, BookViewDto>().ReverseMap();
            //CreateMap<VwUserProfile, UserProfileViewDto>().ReverseMap();

            #region Product
            CreateMap<Product, GetProductDTO>()
            .ForMember(des => des.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(des => des.CreatedDateUtc, opt => opt.MapFrom(src => src.CreatedDateUtc))

            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();



            CreateMap<ProductDTO, Product>().ReverseMap();
            #endregion

            #region Category 
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion

            #region Settings 
            CreateMap<Settings, SettingsDTO>().ReverseMap();
            #endregion

            #region Orders
            CreateMap<Orders, OrderToReturnDTO>();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            #endregion

            #region Comment
            CreateMap<Comment, CommentDTO>().ReverseMap();

            CreateMap<Comment, CommentPublicDTO>()
        
                .ReverseMap();

            #endregion

            #region Offer  
            CreateMap<Offer, OfferDTO>().ReverseMap();

            CreateMap<Offer, GetOfferDTO>()
                    .ForMember(des => des.Products, options => options.MapFrom(src => src.OfferProducts))
                    .ReverseMap();

            CreateMap<OfferProduct, GetProductDTO>()
                .ForMember(des => des.Id, options => options.MapFrom(src => src.ProductId))
                .ForMember(des => des.Name, options => options.MapFrom(src => src.Product.Name))
                .ForMember(des => des.Description, options => options.MapFrom(src => src.Product.Description))
                .ForMember(des => des.Price, options => options.MapFrom(src => src.Product.Price))
                .ForMember(des => des.ImagePath, options => options.MapFrom(src => src.Product.ImagePath))
                .ReverseMap();
            #endregion


    }
    }
}
