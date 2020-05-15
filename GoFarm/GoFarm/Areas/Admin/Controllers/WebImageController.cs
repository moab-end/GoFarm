using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoFarm.DataAccess.Data;
using GoFarm.DataAccess.Data.Repository.IRepository;
using GoFarm.Models;
using GoFarm.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFarm.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class WebImageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WebImageController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {

            WebImages imageObj = new WebImages();

            if (id == null)
            {

                return View(imageObj);
            }

            imageObj = _db.WebImages.SingleOrDefault(m=>m.Id==id);

            if (imageObj == null)
            {
                return NotFound();
            }

            return View(imageObj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int id,WebImages imageObj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    byte[] picture = null;

                    using(var fileStream = files[0].OpenReadStream())
                    {
                        using (var memoryStream=new MemoryStream())
                        {
                            fileStream.CopyTo(memoryStream);
                            picture = memoryStream.ToArray();
                        }

                    }

                    imageObj.Picture = picture;
                }

                if (imageObj.Id == 0)
                {

                    _db.WebImages.Add(imageObj);

                }
                else
                {

                    var imageFromDb = _db.WebImages.Where(c => c.Id == id).FirstOrDefault();
                    imageFromDb.Name = imageObj.Name;

                    if (files.Count > 0)
                    {
                        imageFromDb.Picture = imageObj.Picture;
                    }

                }

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(imageObj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {

            
            return Json(new { data = _db.WebImages.ToList()});

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _db.WebImages.Find(id);

            if (objFromDb == null) 
                return Json(new { success = false, message = "Error while deleting." });

            _db.WebImages.Remove(objFromDb);
            _db.SaveChanges();

            return Json(new { success = true, message = "Image deleted successfully." });
        }
		#endregion
	}
}