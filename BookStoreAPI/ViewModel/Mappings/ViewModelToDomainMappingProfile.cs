using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Model.Entities;
namespace BookStoreAPI.ViewModel.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<BooksViewModel, Books>()
                .ForMember(s => s.Author, map => map.MapFrom(src => default(Author)));
                //.ForMember(s => s.Orders, map => map.MapFrom(src => default(Order)));
                
                
                
          

            CreateMap<AuthorViewModel, Author>();
            CreateMap<OrderViewModel, Order>()
                .ForMember(o => o.Books, map => map.MapFrom(src => default(Books)));
        }
    } 
    
}
