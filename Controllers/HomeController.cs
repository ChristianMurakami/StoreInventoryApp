using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreInventoryApplication.Models;
using StoreInventoryApplication.DataModels;
using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreInventoryApplication.Controllers
{
    public class HomeController : Controller
    {
        private EntityContext EContext;                
        public string StoreId = "1234";
        public string CurrentOrderId;
        public int DaysbetweenOrder = 7;
        public int OrderLimit = 4;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, EntityContext econtext)
        {
            _logger = logger;
            EContext = econtext;
        }
        public IActionResult Index()
        {
            IEnumerable<StoreOrder> Orders = EContext.StoreOrders;           
            return View(Orders);
        }
        public IActionResult FinalyzeOrder(string Id) 
        {                       
            AddOrder(Id,StoreId);
            return RedirectToAction("Index");
        }
        public IActionResult EditOrder(string Id) 
        {
            IEnumerable<ProdId_N_Count> Products = EContext.ProdId_N_Counts.Where(c => c.LocationKey == Id);                  
            return View(Products);
        }
        public IActionResult ChangeQuantity(int Id) 
        {
            ProdId_N_Count model = EContext.ProdId_N_Counts.Find(Id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrderPost(ProdId_N_Count prodId_N_Count)
        {
            if (ModelState.IsValid)
            {
                EContext.ProdId_N_Counts.Find(prodId_N_Count.Id).ProdCount = prodId_N_Count.ProdCount;
                EContext.SaveChanges();
                return RedirectToAction("EditOrder",new {id = $"{prodId_N_Count.LocationKey}" });
            }
            else { return RedirectToAction("Error"); }
        }
        public IActionResult ProductPlacement() 
        {
            IEnumerable<shelf> shelves = EContext.Shelves;          
            return View(shelves);
        }
        public IActionResult ProductControl()
        {
            IEnumerable<Product> Products = EContext.Products; 
            return View(Products);
        }       
        public IActionResult RemoveProduct(int Id)
        {
            Product Model = EContext.Products.Find(Id);
            if (Model != null)
            {
                return View(Model);
            }
            else 
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveProductPost(int Id) 
        {
            Product product = EContext.Products.Find(Id);
            if (product != null)
            {
                EContext.Products.Remove(product);
                EContext.SaveChanges();
            }
            else 
            {
                return NotFound();
            }
            return RedirectToAction("ProductControl");
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductModel Model)
        {
            AddProductToDataBase
                (
                Model.Id, 
                Model.Name,
                Model.Constraint,
                Model.Cost,
                Model.Height,
                Model.Width,
                Model.Vendor,
                Model.Price,
                Model.CountPer
                );
            return RedirectToAction("ProductControl");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult EditProduct(int Id)
        {
            Product Model = EContext.Products.Find(Id);
            ProductModel productModel = new ProductModel();
            productModel.Id = Model.Id;
            productModel.Name = Model.Name;
            productModel.Constraint = Model.Constraint;
            productModel.Cost = Model.InventoryCost;
            productModel.Height = Model.Height;
            productModel.Width = Model.Width;
            productModel.Vendor = Model.Vendor;
            productModel.Price = Model.PricePerItem;
            productModel.CountPer = Model.CountPerCase;

            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(ProductModel Model)
        {
            Product product = EContext.Products.Find(Model.Id);
            product.Id = Model.Id;
            product.Name = Model.Name;
            product.Constraint = Model.Constraint;
            product.InventoryCost = Model.Cost;
            product.Height = Model.Height;
            product.Width = Model.Width;
            product.Vendor = Model.Vendor;
            product.PricePerItem = Model.Price;
            product.CountPerCase = Model.CountPer;

            EContext.SaveChanges();
                     
            return RedirectToAction("ProductControl");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        ///////order methods//////
        public void AddOrder(string OldOrderId, string storeId)
        {
            StoreOrder OldOrder = EContext.StoreOrders.Find(OldOrderId); 
            string NewId = OrderIdBuilder(OldOrder.DateToSubmit);
            DateTime newDate = DateBuilder(OldOrder.DateToSubmit, DaysbetweenOrder);
            IEnumerable<ProdId_N_Count> prodIds =  OrderDataBuild(OldOrderId, NewId);
            IEnumerable<Product> products = SplitProducts(prodIds); ///need product cost for inventory cost
            decimal inventoryCost = GetCost(products);
            StoreOrder order = new StoreOrder(NewId, storeId, inventoryCost, newDate);
            EContext.StoreOrders.Add(order);
            TooManyOrders(OrderLimit);
            OldOrder.Submitted = true;
            EContext.SaveChanges();
        }
        public void TooManyOrders(int HowMany) 
        {
            if (EContext.StoreOrders.Count() > HowMany) 
            {
                var Oldest = EContext.StoreOrders.First();
                var Oldproducts = EContext.ProdId_N_Counts.Where(c => c.LocationKey == Oldest.Id);
                EContext.ProdId_N_Counts.RemoveRange(Oldproducts);
                EContext.StoreOrders.Remove(Oldest);
                EContext.SaveChanges();
            }
        }
        public IEnumerable<ProdId_N_Count> OrderDataBuild(string OldOrderId,string NewId) 
        {
            IEnumerable<ProdId_N_Count> OldProds = EContext.ProdId_N_Counts.Where(c => c.LocationKey == OldOrderId);
            List<ProdId_N_Count> NewProds = new List<ProdId_N_Count>();
            foreach (Product p in EContext.Products)
            {                                              
                ProdId_N_Count newProd = new ProdId_N_Count(p.Id, p.Name, 0, NewId, LocationType.StoreOrder);
                EContext.ProdId_N_Counts.Add(newProd);
                NewProds.Add(newProd);               
            }
            foreach (ProdId_N_Count old in OldProds)
            {
                foreach (ProdId_N_Count n in NewProds) 
                {
                    if (old.ProductId == n.ProductId) { n.ProdCount = old.ProdCount; }
                }
            }
                EContext.SaveChanges();
                return NewProds;
        }
        public DateTime DateBuilder(DateTime dateTime, int days)//builds new submission due date based on time 
        {          
            DateTime newDate = dateTime.AddDays(days);
            return newDate;
        }
        public string OrderIdBuilder(DateTime subdate)
        {
            string NewId = $"{StoreId} : {subdate.Day}.{subdate.Month}.{subdate.Year}";
            return NewId;
        }
        public decimal GetCost(IEnumerable<Product> products)
        {
            decimal cost = 0;
            foreach (Product p in products) { cost += p.InventoryCost; }
            return cost;
        }      
               
        ///Product Methods//

        public string AssignShelf(shelf shelf, Product product)
        {
            if (shelf.Width <= product.Width) { return $"Product Width will not fit in selected shelf"; }
            else if (shelf.Height <= product.Height) { return $"Product Height will not fit in selected shelf"; }
            else
            {
                shelf.ProductId = product.Id;
                return $"{product.Name} was assigned to {shelf.Id}";
            }
        }
       
        public void AddProductToDataBase(int id, string name, HoldingConstraint constraint, decimal cost, double height, double width, string vendor, decimal price, int count)
        {
            Product product = new Product(id, name, constraint, cost, width, height, vendor, price, count);           
            EContext.Products.Add(product);
            EContext.SaveChanges();
            
        }
        public IEnumerable<Product> SplitProducts(IEnumerable<ProdId_N_Count> prodId_N_Counts)
        {
            List<Product> products = new List<Product>();
            foreach (ProdId_N_Count p in prodId_N_Counts)
            {
                Product product = EContext.Products.Find(p.ProductId);
                products.Add(product);
            }
            return products;
        }

        ///Sales Methods

        public void AddSale(string id, DateTime date, double tax, IEnumerable<ProdId_N_Count> prodIds)
        {
            IEnumerable<Product> products = SplitProducts(prodIds);
            decimal sub = GetSub(products);
            decimal final = GetFinal(sub, tax);
            Sale sale = new Sale(id, date, tax, sub, final);
            foreach (ProdId_N_Count p in prodIds)
            {
                EContext.Shelves.Where(c => c.ProductId == p.ProductId).First().ProdCount -= p.ProdCount; //decrement shelf? 
                EContext.ProdId_N_Counts.Where(c => c.ProductId == p.ProductId && c.LocationKey == id).First().ProdCount += p.ProdCount; //adding to order
            }
           EContext.Sales.Add(sale);
           EContext.SaveChanges();
        }
        public void RemoveSale(Sale sale)
        {
            EContext.Sales.Remove(sale);
        }
        public decimal GetSub(IEnumerable<Product> products)
        {
            decimal sub = 0;
            foreach (Product p in products) { sub += p.PricePerItem; }
            return sub;
        }
        public decimal GetFinal(decimal sub, double tax)
        {
            decimal final = sub += sub * decimal.Parse(tax.ToString());
            return final;
        }
    }
   
}
