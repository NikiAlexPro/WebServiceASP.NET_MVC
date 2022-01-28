﻿using Microsoft.EntityFrameworkCore;
namespace WebServiceASP.NET_MVC.Models
{
    public class Product
    {
        public Guid ID { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
