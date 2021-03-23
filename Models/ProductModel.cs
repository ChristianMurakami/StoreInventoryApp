
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StoreInventoryApplication.DataModels;




namespace StoreInventoryApplication.Models
{
    public class ProductModel
    {
        [Display(Name = "ID")]
        [Required]
        [Editable(false)]
        [Range(100000, 999999, ErrorMessage = "Please enter a valid Id")]
        public int Id { set; get; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { set; get; }
        [Required]
        [Display(Name = "Vendor")]
        public string Vendor { set; get; }
        [Required]
        [Display(Name = "Inventory Cost")]
        [Range(.01, 99999.99, ErrorMessage = "Please enter a valid Cost")]
        public decimal Cost { set; get;}
        [Required]
        [Display(Name = "Sale Price")]
        [Range(.01, 99999.99, ErrorMessage = "Please enter a valid Price")]
        public decimal Price { set; get; }
        [Required]
        [Display(Name = "Height")]
        [Range(.01, 99.99, ErrorMessage = "Please enter a valid Height")]
        public double Height { set; get; }
        [Required]
        [Display(Name = "Width")]
        [Range(.01, 99.99, ErrorMessage = "Please enter a valid Width")]
        public double Width { set; get; }
        [Required]
        [Display(Name = "Holding Area")]
        public HoldingConstraint Constraint { set; get; }
        [Required]
        [Display(Name = "Count per case")]
        public int CountPer { set; get; }
    }
}
