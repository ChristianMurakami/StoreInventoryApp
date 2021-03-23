using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StoreInventoryApplication.DataModels
{
    public enum LocationType
    {
        ShipmentRecieved,
        StoreOrder,//order in whole cases sent to distribution center 
        Sale, //decrements count
        Inventory, //whole count
        Snapshot, //takes whole count after received order
        StatisticalDifferences, // helps track discrepancy
        NewProducts
    }
    public class ProdId_N_Count
    {
        [Key]
        public int Id { set; get; } 
        public int ProductId { set; get; }
        public int ProdCount { set; get; }
        public string ProdName { set; get; }
        public string LocationKey { set; get; }
        public LocationType LocationType { set; get;}

        public ProdId_N_Count() { }
        public ProdId_N_Count(int prodid,string prodName, int count, string location, LocationType locationType) 
        {
            ProductId = prodid;
            ProdName = prodName;
            ProdCount = count;
            LocationKey = location;
            LocationType = locationType;
        }
    }
}
