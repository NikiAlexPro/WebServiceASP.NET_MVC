using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace WebServiceASP.NET_MVC.Models
{
    public class Product
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Укажите название продукта")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Укажите описание продукта")]
        public string? Description { get; set; }
    }
}
