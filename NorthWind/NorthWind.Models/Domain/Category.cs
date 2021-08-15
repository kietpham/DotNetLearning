using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NorthWind.Models.Domain.Base;

#nullable disable

namespace NorthWind.Models.Domain
{
    [Index(nameof(CategoryName), Name = "CategoryName")]
    public partial class Category : EntityBase
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        // Create instance, based on the EntityBase
        public static Category CreateInstance(State state = State.Added) {
            return CreateInstance<Category>(state);
        }

        //[Key]
        //[Column("CategoryID")]
        //public int CategoryId { get; set; }

        [Required]
        [StringLength(15)]
        [Column("CategoryName")]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
