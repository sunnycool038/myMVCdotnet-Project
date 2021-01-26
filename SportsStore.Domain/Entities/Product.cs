using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue =false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Please Enter a Product Name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Please Enter a description")]
        public string  Description { get; set; }
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Please Enter a positive Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="Please enter a category")]
        public string Category { get; set; }
        public Byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
