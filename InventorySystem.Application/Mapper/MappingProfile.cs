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
                               context.Items.ContainsKey("RoleNames") ? context.Items["RoleNames"] : new List<string>()));

            //Category mapping
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryGetDto>();

            //Inventory Movement mapping
            CreateMap<InventoryMovementCreateDto,InventoryMovement>();
            CreateMap<InventoryMovement,InventoryMovementGetDto>();

            //Product mapping
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductGetDto>();

            //Supplier mapping
            CreateMap<SupplierCreateDto, Supplier>();
            CreateMap<Supplier,SupplierGetDto>();

            //ProductWarehouse mapping
            CreateMap<ProductWarehouseCreateDto, ProductWarehouse>();
            CreateMap<ProductWarehouse,ProductWarehouseGetDto>();

            //Warehouse
            CreateMap<WarehouseCreateDto, Warehouse>();
            CreateMap<Warehouse, WarehouseGetDto>();
        }
    }
}
