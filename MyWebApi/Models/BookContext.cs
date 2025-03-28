using Microsoft.EntityFrameworkCore;
using MyModels;

namespace MyWebApi.Models
{
    public class BookContext(DbContextOptions<BookContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new()
                {
                    Id = 1,
                    Title = " Essential Programming Language ",
                    Price = 250,
                    PublishDate = new DateTime(2019, 1, 2),
                    InStock = true,
                    Description = "Essential Programming Language "
                },
                new()
                {
                    Id = 2,
                    Title = " Telling Arts ",
                    Price = 245,
                    PublishDate = new DateTime(2019, 4, 15),
                    InStock = true,
                    Description = " Telling Arts "
                },
                new()
                {
                    Id = 3,
                    Title = " Marvel ",
                    Price = 150,
                    PublishDate = new DateTime(2019, 2, 21),
                    InStock = true,
                    Description = " Marvel "
                },
                 new()
                 {
                     Id = 4,
                     Title = " The Beauty of Cook",
                     Price = 450,
                     PublishDate = new DateTime(2019, 12, 2),
                     InStock = true,
                     Description = " The Beauty of Cook "
                 },
                 new()
                 {
                     Id = 5,
                     Title = " Learning how to Cook ",
                     Price = 450,
                     PublishDate = new DateTime(2020, 1, 20),
                     InStock = true,
                     Description = " Learning how to Cook  "
                 }
            );
        }
    }
}
