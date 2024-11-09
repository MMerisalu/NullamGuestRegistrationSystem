using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Base.Contracts.DAL;
using App.Contracts.DAL;
using App.DAL.DTO;

namespace WebApp.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentMethodsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethodDTO>>> GetPaymentMethods()
        {
            return Ok(await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync());
        }

        

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, PaymentMethodDTO paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }

            var paymentMethodDTO = await _uow.PaymentMethods.GetPaymentMethodByIdAsync(id);

            try
            {
                if (paymentMethodDTO == null) 
                {
                    return NotFound();
                }
                paymentMethodDTO.Name = paymentMethod.Name;
                _uow.PaymentMethods.Update(paymentMethodDTO);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_uow.PaymentMethods.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PaymentMethods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentMethodDTO>> PostPaymentMethod(PaymentMethodDTO paymentMethod)
        {
      
            _uow.PaymentMethods.Add(paymentMethod);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var paymentMethod = await _uow.PaymentMethods.GetPaymentMethodByIdAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            await _uow.PaymentMethods.RemoveAsync(paymentMethod.Id);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PaymentMethodExists(int id)
        {
            return (await _uow.PaymentMethods.ExistsAsync(id)); 
        }
    }
}
