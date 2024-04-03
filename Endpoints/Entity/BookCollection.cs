namespace Endpoints.Entity
{
    public class BookCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Book> Books { get; set;}
    }
    
    public class BookCollectionCreate
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
