using StoreInventoryApplication.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StoreInventoryApplication.Models
{
    public class SaleModel
    {
        [Display(Name = "ID")]
        [Required]
        [Range(100000, 999999, ErrorMessage = "Please enter a valid Id")]
        public int Id { set; get; }

        [Display(Name = "Date")]
        [Required]
        public DateTime Date { set; get; }

        [Display(Name = "Purchases")]
        [Required]
        public List<ProdId_N_Count> prodId_N_Counts { set; get; }

        [Display(Name = "Subtotal")]
        [Required]     
        public decimal Subtotal { set; get;}

        [Display(Name = "Tax")]
        [Required]
        public double Tax { private set; get; }

        [Display(Name = "Final Total")]
        [Required]
        public decimal FinalTotal { private set; get; }               
            
    }
}
