using ContractMonthlyClaimsSystem.Areas.Identity.Data;
using ContractMonthlyClaimsSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContractMonthlyClaimsSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClaimsController(
        IWebHostEnvironment hostEnvironment,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
        {
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Claims()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Claims(Claims claims)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Challenge(); 
                }

               
                claims.LecturerID = user.Id;
                

                
                    
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + claims.Upload.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await claims.Upload.CopyToAsync(fileStream);
                    }

                    
                    claims.FilePath = "/uploads/" + uniqueFileName;

                
                claims.TotalAmount=claims.HoursWorked*claims.RatePerHour;
                _context.Add(claims);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Claims));
            }
            

            return View(claims);
        }

       
        public IActionResult ClaimSubmitted()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            
            var claims = _context.Claims.Where(c => c.LecturerID == user.Id).ToList();

            return View(claims);
        }

        [Authorize(Roles = "ProgramCoordinator")]
        public async Task<IActionResult> ApproveClaimProg(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.StatusProg = "Approved";
            _context.Update(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageClaims));
        }

        [Authorize(Roles = "ProgramCoordinator")]
        public async Task<IActionResult> RejectClaimProg(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.StatusProg = "Rejected";
            _context.Update(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageClaims));
        }

        [Authorize(Roles = "AcademicManager")]
        public async Task<IActionResult> ApproveClaimAcademic(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.StatusAcademic = "Approved";
            _context.Update(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageClaims));
        }

        [Authorize(Roles = "AcademicManager")]
        public async Task<IActionResult> RejectClaimAcademic(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.StatusAcademic = "Rejected";
            _context.Update(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageClaims));
        }

        [Authorize(Roles = "ProgramCoordinator,AcademicManager")]
        public async Task<IActionResult> ManageClaims()
        {
            var claims = await _context.Claims
                            .Where(c => c.StatusAcademic == "Pending" || c.StatusProg == "Program Coordinator Approved")
                            .ToListAsync();
            return View(claims);
        }

        public async Task<IActionResult> UpdateClaim(int id, [Bind("Id,StatusProg,StatusAcademic,ClaimStatus")] Claims claim)
        {
            if (ModelState.IsValid)
            {
                
                if (claim.StatusProg == "Rejected" || claim.StatusAcademic == "Rejected")
                {
                    claim.ClaimStatus = "Rejected";
                }
                
                else if (claim.StatusProg == "Approved" && claim.StatusAcademic == "Approved")
                {
                    claim.ClaimStatus = "Approved";
                }
                
                else if (claim.StatusProg == "Approved" || claim.StatusAcademic == "Pending" || claim.StatusProg == "Pending" || claim.StatusAcademic == "Approved")
                {
                    claim.ClaimStatus = "Pending";
                }
                

                _context.Update(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }



        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var claims = await _context.Claims
                .FirstOrDefaultAsync(c => c.Id == id );

            if (claims == null)
            {
                return NotFound();
            }

            _context.Claims.Remove(claims);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
    }
}
