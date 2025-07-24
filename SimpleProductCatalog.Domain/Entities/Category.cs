using SimpleProductCatalog.Abstraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleProductCatalog.Domain.Entities
{
    public class Category : Entity
    {
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
