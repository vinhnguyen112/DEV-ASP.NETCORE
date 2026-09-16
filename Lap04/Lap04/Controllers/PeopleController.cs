using Lap04.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lap04.Controllers
{
    public class PeopleController : Controller
    {
        // GET: PeopleController
        public ActionResult Index()
        {
            var _people = DataLocal.GetPeoples();
            return View(_people);
        }

        // GET: PeopleController/Details/5
        public ActionResult Details(int id)
        {
            var people = DataLocal.GetPeopleById(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // GET: PeopleController/Create
        public ActionResult Create()
        {

            People people = new People();
            return View(people);
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(People model, IFormFile? AvatarFile)
        {
            try
            {
                var file = AvatarFile ?? (HttpContext.Request.Form.Files.Count > 0 ? HttpContext.Request.Form.Files[0] : null);
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var avatarFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar");
                    if (!Directory.Exists(avatarFolder))
                    {
                        Directory.CreateDirectory(avatarFolder);
                    }

                    var path = Path.Combine(avatarFolder, fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    model.Avatar = "/images/avatar/" + fileName;
                }
                else if (string.IsNullOrEmpty(model.Avatar))
                {
                    model.Avatar = "/images/avatar/06.png";
                }

                if (model.Id <= 0)
                {
                    model.Id = DataLocal._people.Any() ? DataLocal._people.Max(p => p.Id) + 1 : 1;
                }

                DataLocal._people.Add(model);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) 
            {
                ViewBag.error = ex.Message;
                return View(model);
            }
        }

        // GET: PeopleController/Edit/5
        public ActionResult Edit(int id)
        {
            var people = DataLocal.GetPeopleById(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, People model)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count()>0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var FileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        model.Avatar = "/images/avatar/" + FileName;
                    }
                }

                for(int i = 0;i< DataLocal._people.Count; i++)
                {
                    if(DataLocal._people[i].Id == id)
                    {
                        DataLocal._people[i] = model;
                        break;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: PeopleController/Delete/5
        public ActionResult Delete(int id)
        {
            var peoples = DataLocal.GetPeopleById(id);
            return View(peoples);
        }

        // POST: PeopleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, People model)
        {
            try
            {
                for(int i = 0;i< DataLocal._people.Count; i++)
                {
                    if(DataLocal._people[i].Id == id)
                    {
                        DataLocal._people.RemoveAt(i);
                        break;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
