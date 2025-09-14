using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Application.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
