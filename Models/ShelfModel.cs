using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using StoreInventoryApplication.DataModels;

namespace StoreInventoryApplication.Models
{
    public class ShelfModel
    {
        [Display(Name = "Name")]
        [Editable(false)]
        public int Id;
        [Display(Name = "Aisle")]
        [Editable(false)]
        public string AisleName;
        [Display(Name = "ProductId")]
        [Editable(false)]
        public int ProductId;
        [Display(Name = "Holding Area")]
        [Editable(false)]
        public HoldingConstraint HoldingConstraint;
    }
}
