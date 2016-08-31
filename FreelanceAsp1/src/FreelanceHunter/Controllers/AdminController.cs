using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreelanceHunter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FreelanceAsp.Models.FreelanceViewModel;
using FreelanceAsp.Models.FreelanceViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FreelanceAsp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: /<controller>/
        ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Adverts.ToList());
        }

        [HttpGet]
        public IActionResult GetAdvert(int id)
        {
            var advert = _db.Adverts.Include(a => a.Offers).Single(a => a.Id == id);
            return View(advert);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOffer(int id, int advertId)
        {
            _db.Offers.Remove(_db.Offers.Single(o => o.Id == id));
            await _db.SaveChangesAsync();
            return Redirect("http://localhost:49896/Admin/GetAdvert/" + advertId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var advertToDelete = _db.Adverts.Include(a => a.Offers).Single(a => a.Id == id);
            var completedTask = _db.CompletedTasks.Single(c => c.AdvertId == id);
            _db.Adverts.Remove(advertToDelete);
            _db.CompletedTasks.Remove(completedTask);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            return View(await _db.Profiles.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProfile(int profId)
        {
            _db.Profiles.Remove(_db.Profiles.Single(p => p.Id == profId));
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(GetProfiles));
        }

        [HttpGet]
        public IActionResult EditAdvert(int id)
        {
            var advertToEdit = _db.Adverts.Single(a => a.Id == id);
            return View(advertToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdvert(Advert advert)
        {
            _db.Adverts.Update(advert);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public IActionResult GetProfile(int id)
        //{
        //    var profile = _db.Profiles.Include(p => p.ProfileComments).Single(p => p.Id == id);
        //    return View(profile);
        //}

        [HttpGet]
        public IActionResult GetProfile(string name)
        {
            var profile = _db.Profiles.Include(p => p.ProfileComments).Single(p => p.Name == name);
            char[] seps = new char[1] { ' ' };
            ViewBag.Likers = profile.Likers != null ? profile.Likers.Trim().Split(seps, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            ViewBag.Dislikers = profile.Dislikers != null ? profile.Dislikers.Trim().Split(seps, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            return View(profile);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = _db.ProfileComments.Include(c => c.Profile).Single(c => c.Id == id);
            _db.ProfileComments.Remove(comment);
            await _db.SaveChangesAsync();
            return Redirect("http://localhost:49896/Admin/GetProfile/" + comment.Profile.Name);
        }
    }
}
