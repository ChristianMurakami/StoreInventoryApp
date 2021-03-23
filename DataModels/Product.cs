using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace StoreInventoryApplication.DataModels
{
    public enum HoldingConstraint
    {
        Dry,
        Frozen,
        WetProduce,
        Deli,
        ColdProduce
    }

    public class Product
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public bool Assigned { set; get; }
        public string Vendor { set; get; }
        public int CountPerCase { set; get; }
        public HoldingConstraint Constraint { set; get; }
        public decimal InventoryCost { set; get; }
        public decimal PricePerItem { set; get; }
        public double Height { set; get; }
        public double Width { set; get; }

        public Product() { }
        public Product(int id, string name, HoldingConstraint constraint, decimal cost, double width, double height,string vendor,decimal price, int countper)
        {
            Id = id;
            Name = name;
            Constraint = constraint;
            InventoryCost = cost;
            Height = height;
            Width = width;
            Vendor = vendor;
            PricePerItem = price;
            CountPerCase = countper;
        }
    }
}
