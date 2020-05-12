using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoFarm.DataAccess.Data.Repository.IRepository;
using GoFarm.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GoFarm.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public ServiceViewModel serviceViewModel { get; set; }

        public ServiceController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
                serviceViewModel = new ServiceViewModel() {

                Service = new Models.Service(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),


            };

            if (id != null)
            {
                serviceViewModel.Service = _unitOfWork.Service.Get(id.GetValueOrDefault());
            }


            return View(serviceViewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert() {

            if (ModelState.IsValid)
            {

                //getting path of wwwroot and files from HttpContext
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (serviceViewModel.Service.Id == 0)
                {
                    //new service
                    //new file name, random generated string
                    string fileName = Guid.NewGuid().ToString();

                    //getting path of directory where file will be uploaded
                    var uploads = Path.Combine(webRootPath,@"images\services");

                    //getting extension of a file
                    var extension = Path.GetExtension(files[0].FileName);

                    //using file stream to create a new file and copy content of uploaded file to a new file
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create)) {
                        files[0].CopyTo(filesStreams);
                    }
                    //setting ImageUrl property to reflect path of a new file
                    serviceViewModel.Service.ImageUrl = @"\images\services\" + fileName + extension;

                    //adding new service to a db context
                    _unitOfWork.Service.Add(serviceViewModel.Service);

                }
                else
                {
                    //Edit Service
                    var serviceFromDb = _unitOfWork.Service.Get(serviceViewModel.Service.Id);

                    if (files.Count > 0)
                    {
                        //new file name, random generated string
                        string fileName = Guid.NewGuid().ToString();

                        //getting path of directory where file will be uploaded
                        var uploads = Path.Combine(webRootPath, @"images\services");

                        //getting extension of a file
                        var extension_new = Path.GetExtension(files[0].FileName);

                        //deleting original file
                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        //using file stream to create a new file and copy content of uploaded file to a new file
                        using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(filesStreams);
                        }
                        //setting ImageUrl property to reflect path of a new file
                        serviceViewModel.Service.ImageUrl = @"\images\services\" + fileName + extension_new;



                    }
                    else
                    {
                        serviceViewModel.Service.ImageUrl = serviceFromDb.ImageUrl;
                    }

                    _unitOfWork.Service.Update(serviceViewModel.Service);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }
            else
            return View(serviceViewModel);
        }

		#region API CALLS

        public IActionResult GetAll()
        {

            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties:"Category,Frequency") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.Service.Get(id);

            string webRootPath = _hostEnvironment.WebRootPath;

            var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (serviceFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _unitOfWork.Service.Remove(serviceFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Service successfully deleted." });
        }
		#endregion
	}
}