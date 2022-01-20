﻿using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;

        public ExpenseCategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var category = _unitOfWork.ExpenseCategoryRepository.GetAll();

            return Ok(category);
        }

        [HttpGet("getbyid")]
        public IActionResult GET(int id)
        {
            var category = _unitOfWork.ExpenseCategoryRepository.Get(id);
            return Ok(category);
        }

        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] ExpenseCategory expense)
        {
            if (expense.CategoryID == 0)
            {
                var categoryAdd = _unitOfWork.ExpenseCategoryRepository.Add(expense);
                _unitOfWork.SaveChanges();
                return Ok(categoryAdd);
            }
            else
            {
                var categoryUp = _unitOfWork.ExpenseCategoryRepository.Update(expense);
                _unitOfWork.SaveChanges();

                return Ok(categoryUp);
            }
        }

        #region HttpPost-Delete
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] ExpenseVModel category)
        {
            var categoryInDb = _unitOfWork.ExpenseCategoryRepository.Get(category.CategoryID);
            _unitOfWork.ExpenseCategoryRepository.Delete(categoryInDb);
            _unitOfWork.SaveChanges();

            return Ok();
        }
        #endregion
    }
}
