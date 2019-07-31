using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Model.Entities;

namespace BookStore.Data
{
    public class BooksDbInitializer
    {
        private static BookContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BookContext>();
                InitializeBookStore(context);
            }
        }

        private static void InitializeBookStore(BookContext context)


        {
            if (!context.Author.Any())
            {

                Author auth_01 = new Author { Name = "Chris Sakellarios", ContactNumber = "1234", Address = "5th avenue,NCY", CreateDate = DateTime.Now };

                Author auth_02 = new Author { Name = "Charlene Campbell", ContactNumber = "1234", Address = "5th avenue,NCY", CreateDate = DateTime.Now };

                Author auth_03 = new Author { Name = "Mattie Lyons", ContactNumber = "1234", Address = "5th avenue,NCY", CreateDate = DateTime.Now };

                Author auth_04 = new Author { Name = "Kelly Alvarez", ContactNumber = "1234", Address = "5th avenue,NCY", CreateDate = DateTime.Now };


                context.Author.Add(auth_01);
                context.Author.Add(auth_02);
                context.Author.Add(auth_03);
                context.Author.Add(auth_04);

                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                Books book_01 = new Books
                {
                    Title = "Meeting",
                    Isbn = "234-456-678",
                    Price = 50,
                    AvailableQuantity = 10,
                    CreateDate = DateTime.Now,
                    AuthorId = 1


                };

                Books book_02 = new Books
                {
                    Title = "Comics",
                    Isbn = "231-246-678",
                    Price = 100,
                    AvailableQuantity = 10,
                    CreateDate = DateTime.Now,
                    AuthorId = 2


                };
                Books book_03 = new Books
                {
                    Title = "Business",
                    Isbn = "674-456-645",
                    Price = 500,
                    AvailableQuantity = 10,
                    CreateDate = DateTime.Now,
                    AuthorId = 1


                };

                Books book_04 = new Books
                {
                    Title = "Crime",
                    Isbn = "534-456-678",
                    Price = 150,
                    AvailableQuantity = 10,
                    CreateDate = DateTime.Now,
                    AuthorId = 3


                };

                //Schedule schedule_05 = new Schedule
                //{
                //    Title = "Friends",
                //    Description = "Friends giving day",
                //    Location = "Home",
                //    CreatorId = 5,
                //    Status = ScheduleStatus.Cancelled,
                //    Type = ScheduleType.Other,
                //    TimeStart = DateTime.Now.AddHours(5),
                //    TimeEnd = DateTime.Now.AddHours(7),
                //    DateCreated = DateTime.Now,
                //    DateUpdated = DateTime.Now,
                //    Attendees = new List<Attendee>
                //    {
                //        new Attendee() { ScheduleId = 4, UserId = 1 },
                //        new Attendee() { ScheduleId = 4, UserId = 2 },
                //        new Attendee() { ScheduleId = 4, UserId = 3 },
                //        new Attendee() { ScheduleId = 4, UserId = 4 },
                //        new Attendee() { ScheduleId = 4, UserId = 5 }
                //    }
                //};


                context.Books.Add(book_01);
                context.Books.Add(book_02);
                context.Books.Add(book_03);
                context.Books.Add(book_04);

            }


            if (!context.Orders.Any())
            {
                Order order_01 = new Order
                {
                    BookId = 10,
                    OrderQuantity = 3,
                    Cost = 150,
                    CreateDate = DateTime.Now,
                    OrderDate = Convert.ToDateTime("2017-02-02")
                };


                Order order_02 = new Order
                {
                    BookId = 11,
                    OrderQuantity = 2,
                    Cost = 100,
                    CreateDate = DateTime.Now,
                    OrderDate = Convert.ToDateTime("2018-01-02")
                };

                Order order_03 = new Order
                {
                    BookId = 12,
                    OrderQuantity = 2,
                    Cost = 300,
                    CreateDate = DateTime.Now,
                    OrderDate = Convert.ToDateTime("2019-01-02")
                };
                Order order_04 = new Order
                {
                    BookId = 13,
                    OrderQuantity = 2,
                    Cost = 1000,
                    CreateDate = DateTime.Now,
                    OrderDate = Convert.ToDateTime("2017-01-02")
                };

                context.Orders.Add(order_01);
                context.Orders.Add(order_02);
                context.Orders.Add(order_03);
                context.Orders.Add(order_04);



                context.SaveChanges();
            }
        }
    }
}
