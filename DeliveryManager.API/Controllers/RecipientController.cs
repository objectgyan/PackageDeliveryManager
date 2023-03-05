using DeliveryManager.Core.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeliveryManager.Core.Interfaces;

namespace DeliveryManager.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class RecipientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipient([FromBody] Recipient recipient)
        {
            await _unitOfWork.RecipientRepository.AddAsync(recipient);
            await _unitOfWork.SaveChangesAsync();

            return Ok(recipient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRecipient(int id, [FromBody] Recipient recipient)
        {
            var existingRecipient = await _unitOfWork.RecipientRepository.GetByIdAsync(id);

            if (existingRecipient == null)
            {
                return NotFound();
            }

            existingRecipient.Name = recipient.Name;
            existingRecipient.Address = recipient.Address;

            await _unitOfWork.SaveChangesAsync();

            return Ok(existingRecipient);
        }
    }
}