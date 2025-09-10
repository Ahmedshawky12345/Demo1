using Demo1.Domain.Common;

namespace Demo1.Domain.Entity
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Descrption { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }

        // relation 
        public Category? department { get; set; }
        public  int department_id { get; set; }

    }
}
