using Microsoft.EntityFrameworkCore;
namespace WebServiceAPI.Models
{
    public class Product
    {
        public Guid ID { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
