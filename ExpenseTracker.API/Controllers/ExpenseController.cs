using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/Expense")]
    public class ExpenseController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
       
        public ExpenseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var expense = _unitOfWork.ExpenseRepository.GetAllExpense();
            return Ok(expense);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Expense expense)
        {
            var expenseInDb = _unitOfWork.ExpenseRepository.Get(expense.ExpenseID)
;
            _unitOfWork.ExpenseRepository.Delete(expenseInDb);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpGet("getbyid")]
        public IActionResult GET(int id)
        {
            var expense = _unitOfWork.ExpenseRepository.Get(id)
;
            return Ok(expense);
        }


        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] Expense expense)
        {
            if (expense.ExpenseID == 0)
            {
                var expenseAdd = _unitOfWork.ExpenseRepository.Add(expense);
                _unitOfWork.SaveChanges();
                return Ok(expenseAdd);
            }
            else
            {
                var expenseUp = _unitOfWork.ExpenseRepository.Update(expense);
                _unitOfWork.SaveChanges();
                return Ok(expenseUp);
            }
        }
    }
}
