using AutoMapper;
using InventorySystem.Domain.Entities;

using InventorySystem.Application.DTOs.ApllicationUserDto;
using InventorySystem.Application.DTOs.CategoryDto;
using InventorySystem.Application.DTOs.InventoryMovementDto;
using InventorySystem.Application.DTOs.ProductDto;
using InventorySystem.Application.DTOs.SupplierDto;
using InventorySystem.Application.DTOs.ProductWarehouseDto;
using InventorySystem.Application.DTOs.WarehouseDto;

namespace InventorySystem.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // For creating users (input from frontend/API)
            CreateMap<ApplicationUser, UserCreateDto>();
            CreateMap<UserCreateDto, ApplicationUser>();

            // For getting users (output to frontend)
            CreateMap<ApplicationUser, UserGetDto>()
           .ForMember(dest => dest.Roles, opt => opt.MapFrom((src, dest, _, context) =>
               context.Items.TryGetValue("RoleNames", out var value) && value is List<string> roles
                   ? roles
                   : new List<string>()));

            //Category mapping
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryUpdateDto, Category>()
                 .ForMember(dest => dest.CategoryId, opt => opt.Ignore());

            //Inventory Movement mapping
            CreateMap<InventoryMovementCreateDto,InventoryMovement>();
            CreateMap<InventoryMovement,InventoryMovementGetDto>();

            //Product mapping
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Product, ProductGetDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
               .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : null));


            //Supplier mapping

            CreateMap<SupplierCreateDto, Supplier>()
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<SupplierUpdateDto, Supplier>()
               .ForMember(dest => dest.SupplierId, opt => opt.Ignore());
            //.ForMember(dest => dest.CreatedAt, opt => opt.Ignore());



            CreateMap<Supplier, SupplierGetDto>()
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail));


            //ProductWarehouse mapping
            CreateMap<ProductWarehouseCreateDto, ProductWarehouse>();
            CreateMap<ProductWarehouse,ProductWarehouseGetDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse.Name));

            CreateMap<ProductWarehouseUpdateDto,ProductWarehouse>();

            //Warehouse
            CreateMap<WarehouseCreateDto, Warehouse>();
            //CreateMap<Warehouse, WarehouseGetDto>();

            CreateMap<Warehouse, WarehouseGetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WarehouseId));


        }
    }
}
