using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoFarm.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GoFarm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;

            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_unitOfWork.User.GetAll(u=>u.Id!=claims.Value));
        }

        public IActionResult Lock(string Id)
        {
            if (Id == null)
                return NotFound();

            _unitOfWork.User.LockUser(Id);

            return RedirectToAction(nameof(Index));

        }
        public IActionResult UnLock(string Id)
        {
            if (Id == null)
                return NotFound();

            _unitOfWork.User.UnlockUser(Id);

            return RedirectToAction(nameof(Index));
        }
    }
}