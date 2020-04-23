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
    public class CashierController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private CashierRepository cashierRepo = new CashierRepository();
        private AdminRepository adminRepo = new AdminRepository();

        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(User user)
        {
            //var checkCashier = db.User.Where(u => u.Username == user.Username && u.Roles == Roles.Cashier).SingleOrDefault();
            var checkCashier = cashierRepo.CheckCashier(user);

            if (checkCashier != null)
            {
                if (checkCashier.Password != user.Password)
                {
                    ViewBag.Error = "Invalid Username or Password";
                    return View();
                }
                else
                {
                    Session["UserId"] = checkCashier.Id;
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
        // GET: Cashier
        public ActionResult Index()
        {
            //var table = db.Table.Include(t => t.User);
            //return View(table.ToList());
            return View(cashierRepo.GetTables());
        }

        // GET: Cashier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Table table = db.Table.Find(id);
            Table table = cashierRepo.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Cashier/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.User, "Id", "Username");
            return View();
        }

        // POST: Cashier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TableId,TableStatus,TableNumber,UserId")] Table table)
        {
            if (ModelState.IsValid)
            {
                //db.Table.Add(table);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.User, "Id", "Username", table.UserId);
            return View(table);
        }

        // GET: Cashier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Table table = db.Table.Find(id);
            Table table = cashierRepo.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.User, "Id", "Username", table.UserId);
            ViewBag.UserId = new SelectList(adminRepo.GetUsers(), "Id", "Username", table.UserId);
            return View(table);
        }

        // POST: Cashier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TableId,TableStatus,TableNumber,UserId")] Table table)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(table).State = EntityState.Modified;
                //db.SaveChanges();
                if (table.TableStatus == TableStatus.Empty)
                {
                    table.UserId = null;
                }
                cashierRepo.UpdateTable(table);
                return RedirectToAction("Index");
                
            }
            //ViewBag.UserId = new SelectList(db.User, "Id", "Username", table.UserId);
            ViewBag.UserId = new SelectList(adminRepo.GetUsers(), "Id", "Username", table.UserId);
            return View(table);
        }

        // GET: Cashier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Table table = db.Table.Find(id);
            Table table = cashierRepo.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Cashier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Table table = db.Table.Find(id);
            //db.Table.Remove(table);
            //db.SaveChanges();
            Table table = cashierRepo.GetTable(id);
            cashierRepo.RemoveTable(table);
            return RedirectToAction("Index");
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
