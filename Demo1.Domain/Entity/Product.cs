using Demo1.Domain.Common;

namespace Demo1.Domain.Entity
{
    public class Product:BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        

        // relation 
        public Category? Category { get; set; }
        public  int CategoryId { get; set; }

    }
}
