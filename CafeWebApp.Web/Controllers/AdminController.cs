using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeWebApp.DomainModelEntity;
using CafeWebApp.InfrastructurePersistence.Repositories;

namespace CafeWebApp.Web.Controllers
{
    public class AdminController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private AdminRepository adminRepo = new AdminRepository();

        // GET: Admin
        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(User user)
        {
            //var checkAdmin = db.User.Where(u => u.Username == user.Username && u.Roles == Roles.Admin).SingleOrDefault();
            var checkAdmin = adminRepo.CheckAdmin(user);
            if (checkAdmin != null)
            {
                if (checkAdmin.Password != user.Password)
                {
                    ViewBag.Error = "Invalid Username or Password";
                    return View();
                }
                else
                {
                    Session["UserId"] = checkAdmin.Id;
                    return RedirectToAction("Index", user);
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
        public ActionResult Index()
        {
            //return View(db.User.ToList());
            return View(adminRepo.GetUsers());
        }
        public ActionResult FoodMenu()
        {
            return View(adminRepo.GetFoods());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //User user = db.User.Find(id);
            User user = adminRepo.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult FoodDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Food food = db.Food.Find(id);
            Food food = adminRepo.GetFood(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Roles")] User user)
        {
            if (ModelState.IsValid)
            {
                //var filterTable = db.Table.ToList();
                //var filterUsername = db.User.Where(u => u.Username == user.Username).SingleOrDefault();
                var filterTable = adminRepo.GetTables();
                var filterUsername = adminRepo.CheckUsername(user);
                if (user.Roles == Roles.Cashier && filterTable.Count() < 10)
                {
                    for (int i = 1; i < 11; i++)
                    {
                        Table table = new Table();
                        table.TableNumber = i;
                        table.TableStatus = TableStatus.Empty;
                        table.UserId = null;
                        //db.Table.Add(table);
                        adminRepo.AddTable(table);
                    }
                }
                if (filterUsername != null)
                {
                    ViewBag.Msg = "This username is already exist";
                    return View();
                }
                //db.User.Add(user);
                //db.SaveChanges();
                adminRepo.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //User user = db.User.Find(id);
            User user = adminRepo.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Roles")] User user)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(user).State = EntityState.Modified;
                //db.SaveChanges();
                adminRepo.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult FoodCreate()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FoodCreate([Bind(Include = "FoodId,Category,FoodName,FoodPrice,Remarks")] Food food)
        {
            if (ModelState.IsValid)
            {
                //var filterFoodName = db.Food.Where(f => f.FoodName == food.FoodName).SingleOrDefault();
                var filterFoodName = adminRepo.CheckFoodName(food);
                if (filterFoodName != null)
                {
                    ViewBag.Msg = "This Food is already exist";
                    return View();
                }
                //db.Food.Add(food);
                //db.SaveChanges();
                adminRepo.AddFood(food);
                return RedirectToAction("FoodMenu");
            }
            return View(food);
        }

        public ActionResult FoodEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Food food = db.Food.Find(id);
            Food food = adminRepo.GetFood(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }
        [HttpPost]
        public ActionResult FoodEdit([Bind(Include = "FoodId,Category,FoodName,FoodPrice,Remarks")] Food food)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(food).State = EntityState.Modified;
                //db.SaveChanges();
                adminRepo.UpdateFood(food);
                return RedirectToAction("FoodMenu");
            }
            return View(food);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //User user = db.User.Find(id);
            User user = adminRepo.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //User user = db.User.Find(id);
            //db.User.Remove(user);
            //db.SaveChanges();
            User user = adminRepo.GetUser(id);
            adminRepo.RemoveUser(user);
            return RedirectToAction("Index");
        }

        public ActionResult FoodDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Food food = db.Food.Find(id);
            Food food = adminRepo.GetFood(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        [HttpPost, ActionName("FoodDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult FoodDeleteConfirmed(int id)
        {
            //Food food = db.Food.Find(id);
            //db.Food.Remove(food);
            //db.SaveChanges();
            Food food = adminRepo.GetFood(id);
            adminRepo.RemoveFood(food);
            return RedirectToAction("FoodMenu");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
