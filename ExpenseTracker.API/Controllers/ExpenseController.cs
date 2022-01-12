using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExpenseController(IUnitOfWork unitOfWork ,IRepository<Expense> expRepo)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var expense = _unitOfWork.ExpenseRepository.GetAll();

            return Ok(expense);
        }

        [HttpPost]
        public IActionResult AddExpenseAndCategory()
        {

            var Expense = new Expense
            {
                Amount = 3400
            };
            var ExpenseCategory = new ExpenseCategory
            {
                CategoryName = "abc"
            };
            _unitOfWork.ExpenseRepository.Add(Expense);
            _unitOfWork.ExpenseCategoryRepository.Add(ExpenseCategory);
            _unitOfWork.SaveChanges();
            return Ok();
        }



    }
}
