using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        public readonly IUnitOfWork  _unitOfWork;
        public ExpenseCategoryController(IUnitOfWork unitOfWorl)
        {
            _unitOfWork = unitOfWorl;
        }
        
        [HttpGet]
       public  IActionResult GetAll()
        {
            var category = _unitOfWork.ExpenseCategoryRepository.GetAll();

            return Ok(category);
        }
        [HttpGet("getbyid")]
        public IActionResult Get(int id)
        {
            var category = _unitOfWork.ExpenseCategoryRepository.Get(id);
            return Ok(category);
        }
        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] ExpenseCategory categoryexpense)
        {
            if (categoryexpense.CategoryID == 0)
            {
                var expensecategoryAdd = _unitOfWork.ExpenseCategoryRepository.Add(categoryexpense);
                _unitOfWork.SaveChanges();
                return Ok(expensecategoryAdd);
            }
            else
            {
                var categoryUp = _unitOfWork.ExpenseCategoryRepository.Update(categoryexpense);
                _unitOfWork.SaveChanges();

                return Ok(categoryUp);
            }
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] ExpenseCategory categoryexpense)
        {
            var CategoryInDb = _unitOfWork.ExpenseCategoryRepository.Get(categoryexpense.CategoryID);

            _unitOfWork.ExpenseCategoryRepository.Delete(CategoryInDb);
            _unitOfWork.SaveChanges();
            return Ok();
        }
    }
}
