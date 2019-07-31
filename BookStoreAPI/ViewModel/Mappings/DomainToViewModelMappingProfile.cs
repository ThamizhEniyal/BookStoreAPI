using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Model.Entities;

namespace BookStoreAPI.ViewModel.Mappings
{
    public class DomainToViewModelMappingProfile:Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Books, BooksViewModel>()
                .ForMember(vm => vm.Author,
                    map => map.MapFrom(s => s.Author.Name))
               .ForMember(vm => vm.Orderlist,
               map => map.MapFrom(s => s.Orders.Select(a => a.BookId)));


            CreateMap<Books, BooksDetailViewModel>()
                .ForMember(vm => vm.Author,
                    map => map.MapFrom(s => s.Author.Name))
               // .ForMember(vm => vm.o, map =>
                //    map.MapFrom(src => new List<UserViewModel>()))
                .ForMember(vm => vm.Isbn, map =>
                    map.MapFrom(s => (s.Isbn).ToString()))
                .ForMember(vm => vm.Title, map =>
                    map.MapFrom(s => (s.Title).ToString()))
                .ForMember(vm => vm.Price, map =>
                    map.MapFrom(s => s.Price))
                .ForMember(vm => vm.AvailableQuantity, map =>
                    map.MapFrom(s => s.AvailableQuantity));

            CreateMap<Author, AuthorViewModel>()
                .ForMember(vm => vm.BooksCreated,
                    map => map.MapFrom(u => u.BooksCreated.Count()));

            CreateMap<Order, OrderViewModel>()
                .ForMember(vm => vm.Book,
                    map => map.MapFrom(s => s.Books.Title))
                .ForMember(vm => vm.TotalQuantity,
                   map => map.MapFrom(s => s.Books.AvailableQuantity))
                .ForMember(vm => vm.BookId, map =>
                    map.MapFrom(s => (s.BookId).ToString()))
                .ForMember(vm => vm.OrderQuantity, map =>
                    map.MapFrom(s => (s.OrderQuantity).ToString()))
                .ForMember(vm => vm.Cost, map =>
                    map.MapFrom(s => s.Cost))
                .ForMember(vm => vm.OrderDate, map =>
                    map.MapFrom(s => s.OrderDate))
                 .ForMember(vm => vm.CreateDate, map =>
                    map.MapFrom(s => s.CreateDate)    
                    );

        }
    }
}
