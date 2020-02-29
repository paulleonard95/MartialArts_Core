using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Martial.Data.Models;
using Martial.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Martial.Web.Controllers
{
    public class MemberController : Controller
    {

        private DataService service;

        public MemberController()
        {
            service = new DataService(); // really should use DI
        }

        // GET: Member
        public ActionResult Index()
        {
            var members = service.GetAllMembers();
            return View(members);
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            Member existing = service.FindMemberById(id);
            return View(existing);
        }

        // GET: Member/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // need some authentication to hide create from none admin users
        public ActionResult Create(Member obj )
        {
            if (ModelState.IsValid)
            {
                //check valid state
                service.AddMember(obj);
                return RedirectToAction(nameof(Index));
            }
            else // not valid so redisplay
            {
                return View(obj);
            }
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            Member existing = service.FindMemberById(id);
            return View(existing);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member obj, IFormCollection collection)
        {
            if(ModelState.IsValid)
            {
                // check valid state
                service.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
            /*try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }*/
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            Member existing = service.FindMemberById(id);
            return View(existing);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            service.DeleteMember(id);
            return RedirectToAction(nameof(Index));
            /*try
            {
                // TODO: Add delete logic here
                service.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }*/
        }
    }
}