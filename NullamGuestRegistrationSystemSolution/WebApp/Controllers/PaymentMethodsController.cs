using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using WebApp.ViewModels;
using App.DAL.EF.Repositories;
using App.Contracts.DAL.IAppRepositories;
using App.Contracts.DAL;
using App.DAL.DTO;

namespace WebApp.Controllers
{
    public class PaymentMethodsController : Controller
    {
        private readonly IAppUnitOfWork _uow;
       
        public PaymentMethodsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PaymentMethods
        public async Task<IActionResult> Index()
        {
            return View(await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync());
        }


        // GET: PaymentMethods/Create
        public IActionResult Create()
        {
            var vm = new CreateEditPaymentMethodVM();
            return View(vm);
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditPaymentMethodVM vm)
        {
            var paymentMethod = new PaymentMethodDTO();
            paymentMethod.Name = vm.Name;

            if (ModelState.IsValid)
            {
                _uow.PaymentMethods.Add(paymentMethod);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: PaymentMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var vm = new CreateEditPaymentMethodVM();

            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.GetPaymentMethodByIdAsync(id.Value);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            vm.Id = paymentMethod.Id;
            vm.Name = paymentMethod.Name;
            return View(vm);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditPaymentMethodVM vm)
        {

            var paymentMethod = await _uow.PaymentMethods.GetPaymentMethodByIdAsync(id);

            if( paymentMethod == null)
            {
                return NotFound();
            }

            paymentMethod.Name = vm.Name;


            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PaymentMethods.Update(paymentMethod);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.PaymentMethods.Exists(paymentMethod.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: PaymentMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var vm = new DeletePaymentMethodVM();
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _uow.PaymentMethods.FirstOrDefaultAsync(id.Value);


           if (paymentMethod == null)
            {
                return NotFound();
            }
           vm.Id = paymentMethod.Id;
           vm.Name = paymentMethod.Name;

            return View(vm);
        }

        //// POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.PaymentMethods.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(int id)
        {
            return _uow.PaymentMethods.Exists(id);
        }

    }
}
