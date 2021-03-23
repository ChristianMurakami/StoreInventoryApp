using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreInventoryApplication.DataModels
{
    public class shelf
    {
        [Key]
        public string Id { set; get; }
        public string AisleId { set; get; }
        public int ProductId { set; get; }
        public double Width { set; get; }
        public double Height { set; get; }
        public int ProdCount { set; get; }    
        public HoldingConstraint Constraint { set; get; }

        public shelf() { }
        public shelf(string id, string aisle, HoldingConstraint constraint, int count,double height, double width) 
        {
            Id = id;
            AisleId = aisle;
            Height = height;
            Width = width;
            ProdCount = count;
            Constraint = constraint;
            ProductId = 0;              //unassigned
        }
       
    }
}
