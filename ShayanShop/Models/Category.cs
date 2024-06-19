﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShayanShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<CategoryToProduct> CategoryToProducts { get; set; }
    }
}