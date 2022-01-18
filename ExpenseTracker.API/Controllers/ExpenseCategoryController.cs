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
       public IActionResult GetAll()
        {
            var category = _unitOfWork.ExpenseCategoryRepository.GetAll();

            return Ok(category);
        }
    }
}
