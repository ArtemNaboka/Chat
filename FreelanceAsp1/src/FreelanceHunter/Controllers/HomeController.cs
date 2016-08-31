using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreelanceHunter.Data;
using FreelanceAsp.Models.FreelanceViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FreelanceAsp.Models.FreelanceViewModels;
using FreelanceHunter.Models.FreelanceViewModels;
using FreelanceHunter.Models.FreelanceViewModels.ViewModels;

namespace FreelanceAsp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            /////////////////////////////////
            //foreach(var advert in _db.Adverts.Include(a => a.Offers))
            //{
            //    _db.Adverts.Remove(advert);
            //}
            //foreach(var offer in _db.Offers)
            //{
            //    _db.Offers.Remove(offer);
            //}
            //foreach(var profile in _db.Profiles.Include(p => p.ProfileComments))
            //{
            //    _db.Remove(profile);
            //}
            //foreach(var com in _db.ProfileComments)
            //{
            //    _db.Remove(com);
            //}
            //foreach(var perm in _db.UsersPerformers)
            //{
            //    _db.UsersPerformers.Remove(perm);
            //}
            //_db.SaveChanges();
            //////////////////////////////////

            //foreach(var task in _db.CompletedTasks)
            //{
            //    _db.Remove(task);
            //}
            //_db.SaveChanges();

            return View(await _db.Adverts.Where(a => a.IsRelevant == false).ToListAsync());
        }

        [HttpGet]
        public IActionResult GetAdvert(int id)
        {
            var advert = _db.Adverts.Include(a => a.Offers).Single(a => a.Id == id);
            ViewBag.IsOwner = isOwner(advert);
            ViewBag.IsRelevant = advert.IsRelevant;
            if(advert.IsRelevant)
            {
                var performer = _db.UsersPerformers.SingleOrDefault(u => u.AdvertId == id);
                if (performer != null)
                {
                    ViewData["Performer"] = performer.User;
                }
            }
            return View(advert);
        }

        private bool isOwner(Advert advert)
        {
            return User.Identity.Name == advert.Author;
        }


        [HttpPost]
        public async Task<IActionResult> MakeOffer(Offer offer, int advertId = -1)
        {
            offer.DateOfCreate = DateTime.Now.ToLocalTime().ToString();
            var advert = _db.Adverts.Include(a => a.Offers).Single(a => a.Id == advertId);
            offer.Advert = advert;
            _db.Offers.Add(offer);
            await _db.SaveChangesAsync();
            return Redirect("http://localhost:49896/Home/GetAdvert/" + advertId);
        }

        [Authorize]
        [HttpGet]
        public IActionResult MyAdverts(bool isRelevant = false)
        {
            var name = User.Identity.Name;
            var adverts = _db.Adverts.Where(a => a.Author == name && a.IsRelevant == isRelevant).ToList();
            ViewBag.AbleToEdit = !isRelevant;
            return View(adverts);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdvert(int id, bool completed = false)
        {
            var advertToDelete = _db.Adverts.Include(a => a.Offers).Single(a => a.Id == id);
            _db.Adverts.Remove(advertToDelete);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(MyAdverts), new { isRelevant = completed });
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditAdvert(int? id)
        {
            if (id == 0) return NotFound();
            var advertToEdit = _db.Adverts.Single(a => a.Id == id);
            if (!User.Identity.Name.Equals(advertToEdit.Author)) return NotFound();
            return View(advertToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdvert(Advert advert)
        {
            if (ModelState.IsValid)
            {
                _db.Adverts.Update(advert);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(MyAdverts));
            }
            return View(advert);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateAdvert()
        {
            if (!hasProfile(User.Identity.Name))
            {
                return NotFound();
            }
            Advert advert = new Advert();
            return View(advert);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvert(Advert advert)
        {
            if (ModelState.IsValid)
            {
                advert.DateOfCreate = DateTime.Now.ToLocalTime().ToString();
                _db.Adverts.Add(advert);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advert);
        }


        [HttpGet]
        public IActionResult GetProfile(string name)
        {
            Profile profile = _db.Profiles.Include(p => p.ProfileComments).Include(p => p.CompletedTasks).SingleOrDefault(p => p.Name == name);
            char[] seps = new char[1] { ' ' };
            ViewBag.Likers = profile.Likers != null ? profile.Likers.Trim().Split(seps, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            ViewBag.Dislikers = profile.Dislikers != null ? profile.Dislikers.Trim().Split(seps, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            ViewBag.IsOwner = name == User.Identity.Name;
            ViewBag.Posted = _db.Adverts.Where(a => a.Author == name).ToList().Count;
            return View(profile);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MakeLike(int id, string user, string liker)
        {
            var userProfile = _db.Profiles.Single(p => p.Name == user);
            if (userProfile.Dislikers != null && userProfile.Dislikers.Contains(liker))
            {
                _db.Profiles.Single(p => p.Name == user).Dislikers = userProfile.Dislikers.Replace(liker + " ", "");
            }
            if (userProfile.Likers == null) userProfile.Likers = "";
            if (!userProfile.Likers.Contains(liker))
            {
                _db.Profiles.Single(p => p.Name == user).Likers = userProfile.Likers + liker + " ";
            }
            else
            {
                _db.Profiles.Single(p => p.Name == user).Likers = userProfile.Likers.Replace(liker + " ", "");
            }
            await _db.SaveChangesAsync();
            return Redirect("http://localhost:49896/Home/GetProfile?name=" + user);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MakeDislike(int id, string user, string disliker)
        {
            var userProfile = _db.Profiles.Single(p => p.Name == user);
            if(userProfile.Likers != null && userProfile.Likers.Contains(disliker))
            {
                _db.Profiles.Single(p => p.Name == user).Likers = userProfile.Likers.Replace(disliker + " ", "");
            }
            if (userProfile.Dislikers == null) userProfile.Dislikers = "";
            if (!userProfile.Dislikers.Contains(disliker))
            {
                _db.Profiles.Single(p => p.Name == user).Dislikers = userProfile.Dislikers + disliker + " ";
            }
            else
            {
                _db.Profiles.Single(p => p.Name == user).Dislikers = userProfile.Dislikers.Replace(disliker + " ", "");
            }
            await _db.SaveChangesAsync();
            return Redirect("http://localhost:49896/Home/GetProfile?name=" + user);
        }


        [HttpPost]
        public async Task<IActionResult> MakeComment(ProfileComment profileComment, int userId)
        {
            var user = _db.Profiles.Include(p => p.ProfileComments).Single(p => p.Id == userId);
            profileComment.Profile = user;
            _db.ProfileComments.Add(profileComment);
            await _db.SaveChangesAsync();
            return Redirect("http://localhost:49896/Home/GetProfile?name=" + user.Name);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private bool hasProfile(string userName)
        {
            var profile = _db.Profiles.SingleOrDefault(p => p.Name == userName);
            return profile != null ? true : false;
        }


        [HttpPost]
        public void MakePerformer(UserPerformer model)
        {
            _db.UsersPerformers.Add(model);
            _db.Adverts.Single(a => a.Id == model.AdvertId).IsRelevant = true;
            var user = _db.Profiles.Single(p => p.Name == model.User);
            var completedTask = new CompletedTask()
            {
                AdvertId = model.AdvertId,
                Profile = user
            };
            _db.CompletedTasks.Add(completedTask);
            user.CompletedTasks.Add(completedTask);
            _db.SaveChanges();
        }


        [HttpGet]
        public IActionResult GetPerfomanced(string performer)
        {
            var profile = _db.Profiles.Include(p => p.CompletedTasks).Single(p => p.Name == performer);
            List<Advert> adverts = new List<Advert>();
            foreach(var task in profile.CompletedTasks)
            {
                var advert = _db.Adverts.Single(a => a.Id == task.AdvertId);
                adverts.Add(advert);
            }
            ViewData["Performer"] = performer;
            return View(adverts);
        }


        [HttpPost]
        public async Task ChangeUserInformation(UserInformation userInf)
        {
            _db.Profiles.Single(p => p.Name == userInf.UserName).Information = userInf.Information;
            await _db.SaveChangesAsync();
        }


        public async Task<IActionResult> GetPosted(string userName)
        {
            var postedAdverts = await _db.Adverts.Where(a => a.Author == userName).ToListAsync();
            return View(postedAdverts);
        }
    }
}
