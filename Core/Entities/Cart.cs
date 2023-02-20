using System.Collections.Generic;

namespace Core.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
