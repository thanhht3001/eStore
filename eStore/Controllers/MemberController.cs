using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {

        IMemberRepository memberRepository = new MemberRepository();
        // GET: MemberController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Members()
        {
            int? memberID = HttpContext.Session.GetInt32("MemberID");
            if (memberID != null)
            {
                var members = memberRepository.GetMembers();
                return View(members);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Home");
            }
        }
        public ActionResult NewMember()
        {
            return View();
        }

        // POST: AdminController/NewMember
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewMember(Member member)
        {
            try
            {
                int? memberID = HttpContext.Session.GetInt32("MemberID");
                if (memberID != null)
                {
                    memberRepository.AddMember(member);
                }
                return RedirectToAction(nameof(Members));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(member);
            }
        }
        public ActionResult MemberDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMemberByID(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        public ActionResult DeleteMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pro = memberRepository.GetMemberByID(id.Value);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMember(int id)
        {
            try
            {
                memberRepository.DeleteMember(id);
                return RedirectToAction(nameof(Members));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }

        public ActionResult EditMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = memberRepository.GetMemberByID(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMember(int id, Member member)
        {
            try
            {
                if (id != member.MemberId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    memberRepository.UpdateMember(member);
                }
                return RedirectToAction(nameof(Members));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
