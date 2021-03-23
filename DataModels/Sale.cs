using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StoreInventoryApplication.DataModels
{ 
    public class Sale
    {
        [Key]
        public string Id { set; get; }//id set as Date + count during that day
        public DateTime Date { set; get; }       
        public decimal Subtotal { set; get;}
        public double Tax { set; get; }//percentage
        public decimal FinalTotal { set; get; }

        public Sale() { }
        public Sale(string id, DateTime date,double tax, decimal sub, decimal final) 
        {
            Id = id;
            Date = date;
            Tax = tax;           
            Subtotal = sub;
            FinalTotal = final;
        }              
    }
}
