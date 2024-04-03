using Microsoft.EntityFrameworkCore;

namespace Endpoints.Entity
{
    public class MyDBContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCollection> BooksCollections { get; set; }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
            //optionsBuilder.UseSqlServer(ConfigurationBinder.GetValue(typeof(string), "SQLServer"));
        //}

    }
}
