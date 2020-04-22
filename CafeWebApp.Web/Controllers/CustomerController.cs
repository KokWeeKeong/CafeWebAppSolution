using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeWebApp.Web.Models;

namespace CafeWebApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Customer
        public ActionResult FoodMenu()
        {
            return View(db.Food.ToList());
        }
        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(User user)
        {
            var checkCustomer = db.User.Where(u => u.Username == user.Username && u.Roles == Roles.Customer).SingleOrDefault();

            if (checkCustomer != null)
            {
                if (checkCustomer.Password != user.Password)
                {
                    ViewBag.Error = "Invalid Username or Password";
                    return View();
                }
                else
                {
                    Session["UserId"] = checkCustomer.Id;
                    return RedirectToAction("FoodMenu", user);
                }
            }
            else
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("LoginPage");
        }
        public ActionResult Cartlist()
        {
            var Id = Convert.ToInt32(Session["UserId"]);
            var filterCart = db.Carts.Where(c => c.UserId == Id).ToList();
            return View(filterCart);
        }
        public ActionResult AddtoTable()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddtoCart(int Id)
        {
            Cart cart = new Cart();
            var filterFood = db.Food.Where(f => f.FoodId == Id).SingleOrDefault();
            var userId = Convert.ToInt32(Session["UserId"]);
            var filterCart = db.Carts.Where(c => c.UserId == userId && c.FoodId == Id).SingleOrDefault();

            if (filterCart != null)
            {
                return Json(new { message = "this food is already added", JsonRequestBehavior.AllowGet });
            }
            else
            {
                cart.FoodId = Id;
                cart.FoodName = filterFood.FoodName;
                cart.TotalAmount = filterFood.FoodPrice;
                cart.FoodQuantity = 1;
                cart.UserId = userId;
                db.Carts.Add(cart);
                db.SaveChanges();
                return Json(new { message = "Food added to cartlist", JsonRequestBehavior.AllowGet });
            }
        }
        [HttpPost]
        public ActionResult UpdateCart(string Operator, int Id)
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            var filterCart = db.Carts.Where(c => c.FoodId == Id && c.UserId == userId).SingleOrDefault();
            var filterFood = db.Food.Where(f => f.FoodId == Id).SingleOrDefault();

            if (Operator == "+")
            {
                filterCart.FoodQuantity++;
                filterCart.TotalAmount = filterCart.FoodQuantity * filterFood.FoodPrice;
                db.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (filterCart.FoodQuantity == 1)
                {
                    filterCart.FoodQuantity = 1;
                }
                else
                {
                    filterCart.FoodQuantity--;
                    filterCart.TotalAmount = filterCart.FoodQuantity * filterFood.FoodPrice;
                    db.SaveChanges();
                }
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult RemoveCart(int Id)
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            var filterCart = db.Carts.Where(c => c.FoodId == Id && c.UserId == userId).SingleOrDefault();
            var filterFood = db.Food.Where(f => f.FoodId == Id).SingleOrDefault();
            db.Carts.Remove(filterCart);
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddtoTable(int tableNumber)
        {
            var userId = Convert.ToInt32(Session["UserId"]);
            var filterTable = db.Table.Where(t => t.TableNumber == tableNumber).SingleOrDefault();

            if (filterTable.TableStatus == TableStatus.Occupied)
            {
                ViewBag.Msg = "This Table is already taken";
                return View();
            }
            else
            {
                if (filterTable.UserId == null)
                {
                    filterTable.TableNumber = tableNumber;
                    filterTable.TableStatus = TableStatus.Occupied;
                    filterTable.UserId = userId;
                    db.Entry(filterTable).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Msg = "Place Order Succesful";
                    return View();
                }
            }
            return View();
        }
        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodId,Category,FoodName,FoodPrice,Remarks")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Food.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodId,Category,FoodName,FoodPrice,Remarks")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Food.Find(id);
            db.Food.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Index");
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
