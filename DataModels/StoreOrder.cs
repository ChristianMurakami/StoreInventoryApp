using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StoreInventoryApplication.DataModels
{
    public class StoreOrder
    {
        [Key]
        public string Id { set; get; }
        public string StoreId { set; get; }  
        public decimal InventoryCost { set; get; }
        public bool Submitted { set; get; }
        public DateTime DateToSubmit { set; get; }

        public StoreOrder() { }
        public StoreOrder(string id,string store, decimal inventoryCost,DateTime subdate)
        {
            Id = id;
            StoreId = store;
            InventoryCost = inventoryCost;
            DateToSubmit = subdate;
        }      
    }
}
