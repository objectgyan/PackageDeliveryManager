using DeliveryManager.Core.Interfaces;
using DeliveryManager.Core.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeliveryManager.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBarCodeGenerator _barCodeGenerator;

        public PackageController(IUnitOfWork unitOfWork, IBarCodeGenerator barCodeGenerator)
        {
            _unitOfWork = unitOfWork;
            _barCodeGenerator = barCodeGenerator;
        }

        [HttpGet]
        public async Task<IEnumerable<Package>> GetAllPackages()
        {
            return await _unitOfWork.PackageRepository.GetAllAsync();
        }

        [HttpGet("delivered")]
        public async Task<IEnumerable<Package>> GetAllDeliveredPackages()
        {
            return await _unitOfWork.PackageRepository.GetAsync(p => p.Status == "DELIVERED");
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage([FromBody] Package package)
        {
            package.Status = "RECEIVED";
            package.LastUpdated = DateTime.UtcNow;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var name = nameClaim?.Value;
            package.CreatedBy = name;
            await _unitOfWork.PackageRepository.AddAsync(package);
            await _unitOfWork.SaveChangesAsync();

            return Ok(package);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPackage(int id, [FromBody] Package package)
        {
            var existingPackage = await _unitOfWork.PackageRepository.GetByIdAsync(id);

            if (existingPackage == null)
            {
                return NotFound();
            }

            existingPackage.Name = package.Name;
            existingPackage.Description = package.Description;
            existingPackage.Status = package.Status;
            existingPackage.RecipientId = package.RecipientId;
            existingPackage.LastUpdated = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return Ok(existingPackage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(int id)
        {
            var package = await _unitOfWork.PackageRepository.GetByIdAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        [HttpGet("recipient/{recipientId}")]
        public async Task<IEnumerable<Package>> GetPackagesByRecipientId(int recipientId)
        {
            return await _unitOfWork.PackageRepository.GetAsync(p => p.RecipientId == recipientId);
        }

        [HttpGet("{id}/barcode")]
        public async Task<IActionResult> GetBarcodeById(int id)
        {
            var package = await _unitOfWork.PackageRepository.GetByIdAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            var packageIdentifier = package.PackageIdentifier;
            var barcodeImage = _barCodeGenerator.GenerateCode39Barcode(packageIdentifier);
            return File(barcodeImage, "image/jpeg", $"{packageIdentifier}.jpg");
        }
    }
}
