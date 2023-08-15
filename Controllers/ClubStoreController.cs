using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using SportClubSite.Models;

namespace SportClubSite.Controllers
{
    
    public class ClubStoreController : Controller

    {

        SportSiteEntities40 db = new SportSiteEntities40();

        //
        // GET: /ClubStore/
        public ActionResult Index()
        {
            List<Name> hh = db.Names.ToList();
            string ii = "hh";
            foreach(Name i in hh)
            {
                ii= i.Name1;

            }
            List<User> hh1 = db.Users.ToList();
            int i1= 777777;
            foreach (User i in hh1)
            {
               if(i.UserName.Equals(ii))
               {
                   i1 = i.UserId;
               }

            }
            Session["UserId"] = i1;
            
            if (TempData["cart"] != null)
            {
                float x = 0;
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.Bill;

                }

                TempData["total"] = x;
            }
            TempData.Keep();
            return View(db.Products.OrderByDescending(x => x.ProductId).ToList());
        }
        public ActionResult Adtocart(int? Id)
        {

            Product p = db.Products.Where(x => x.ProductId == Id).SingleOrDefault();
            return View(p);
        }
         
        List<Cart> li = new List<Cart>();
        [HttpPost]
        public ActionResult Adtocart(Product pi, string quantity, int Id)
        {
            Product p = db.Products.Where(x => x.ProductId == Id).SingleOrDefault();

            Cart c = new Cart();
            c.ProductId = p.ProductId;
            c.Price = (float)p.Price;
            c.Quantity = Convert.ToInt32(quantity);
            c.Bill = c.Price * c.Quantity;
            c.Description = p.Description;
            c.ProductName = p.ProductName;
            c.UserName = c.UserName;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
                if (p.quntity == 1)
                {
                    db.Products.Remove(p);
                    db.SaveChanges();
                }
                else
                {
                    p.quntity -= c.Quantity;
                    db.SaveChanges();
                }
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.ProductId == c.ProductId)
                    {
                        item.Quantity += c.Quantity;
                        item.Bill += c.Bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                    
                }
                TempData["cart"] = li2;
                }

            TempData.Keep();
           
            



            return RedirectToAction("Index");
        }
        public ActionResult remove(int? id)
        {
            List<Cart> li2 = TempData["cart"] as List<Cart>;
            Cart c = li2.Where(x => x.ProductId == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.Bill;
            }
            TempData["total"] = h;
            return RedirectToAction("Index");
        }
        
        public ActionResult checkout()
        {
            TempData.Keep();


            return View();
        }
        [HttpPost]
        public ActionResult checkout(Order o , User u)
        {
            List<Cart> li = TempData["cart"] as List<Cart>;

            invoice iv = new invoice();

            iv.in_user = (int)Session["UserId"];
            iv.invoiceDate = System.DateTime.Now;
            iv.TotalBill = (float)TempData["total"];
            db.invoices.Add(iv);
            db.SaveChanges();

            foreach (var item in li)
            {
                Order od = new Order();
                od.OrderId = item.ProductId;
                od.ProductId = item.ProductId;
                od.UserId = (int)Session["UserId"];
                
                od.in_id = iv.in_id;
                od.OrderDate = System.DateTime.Now;
                od.quantity = item.Quantity;
               od.UnitPrice = (int)item.Price;
                od.bill = item.Bill;
                db.Orders.Add(od);
                db.SaveChanges();
            }
            TempData.Remove("total");
            TempData.Remove("cart");
            TempData["msg"] = "Transation Completed ...........";
            TempData.Keep();
            return RedirectToAction("Index");
        }
        public ActionResult Add()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Product product)
        {
            if (!string.IsNullOrWhiteSpace(product.ProductName))
            {
                Product p = new Product();

                
                    p.Add(product);
                    p.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            else
            {
                ModelState.AddModelError("ProductName", "Please Enter Product Name");
            }



            return View(product);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Description,Image")] Product p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product pp = db.Products.Find(id);
            db.Products.Remove(pp);
            db.SaveChanges();
            return Redirect("/ClubStore");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
 
       
	}
}