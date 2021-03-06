using ExpenseTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;


using System;
using PagedList;
using System.Linq;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace ExpenseTracker.Web.Controllers
{
    public class ExpenseUIController : Controller
    {
        #region Index
        //public async Task<IActionResult> Index(int currentPageIndex)
        //{
        //    var expense = new List<ExpenseDTO>();

        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("http://localhost:24217/api/Expense");

        //        string result = response.Content.ReadAsStringAsync().Result;
        //        expense = JsonConvert.DeserializeObject<List<ExpenseDTO>>(result);

        //    }
        //    return View(expense);

        //}
        public async Task<IActionResult> Index(string filter, int pageIndex = 1, string sortExpression = "CategoryName")
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/Expense");

                string result = response.Content.ReadAsStringAsync().Result;
                var expense = JsonConvert.DeserializeObject<List<ExpenseDTO>>(result);
                expense = expense.OrderBy(i => i.ExpenseID).ToList();


                if (!string.IsNullOrWhiteSpace(filter))
                {

                    expense = expense.Where(p => p.CategoryName.Contains(filter)).ToList();

                }
                var pagedExpense = PagingList.Create(expense, 3, pageIndex, sortExpression, "CompanyName");

                pagedExpense.RouteValue = new RouteValueDictionary {
                { "filter", filter}
                };

                return View(pagedExpense);
            }
        }
        #endregion

        #region HttpGet-Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var expenseCategory = new List<ExpenseCategoryDTO>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/ExpenseCategory");

                string result = response.Content.ReadAsStringAsync().Result;
                expenseCategory = JsonConvert.DeserializeObject<List<ExpenseCategoryDTO>>(result);
            }

            var expense = new ExpenseDTO();
            expense.CategoryDropDownList = expenseCategory;

            return View(expense);
        }
        #endregion

        #region HttpPost-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseDTO expense)
        {
            var expenseJson = JsonConvert.SerializeObject(expense);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/Expense", httpContent);

                string result = response.Content.ReadAsStringAsync().Result;
            }
            TempData["Success"] = "Data Created Successful";
            return RedirectToAction("Index");
        }
        #endregion



        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var ExpenseCategory = new List<ExpenseCategoryDTO>();
            var expenseD = new ExpenseDTO();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/ExpenseCategory");
                string result = response.Content.ReadAsStringAsync().Result;
                ExpenseCategory = JsonConvert.DeserializeObject<List<ExpenseCategoryDTO>>(result);

                var exResponse = await client.GetAsync("http://localhost:24217/api/Expense/getbyid?id=" + id);
                string result2 = exResponse.Content.ReadAsStringAsync().Result;
                expenseD = JsonConvert.DeserializeObject<ExpenseDTO>(result2);
            }

            expenseD.CategoryDropDownList = ExpenseCategory;
            return View(expenseD);
        }
        #region HttpPost-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpenseDTO expense)
        {
            var expenseJson = JsonConvert.SerializeObject(expense);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/Expense", httpContent);

                string result = response.Content.ReadAsStringAsync().Result;
            }
            TempData["Success"] = "Data Updated Successful";
            return RedirectToAction("Index");
        }

        #endregion
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = new ExpenseDTO();
            using (var client = new HttpClient())
            {

                var response = await client.GetAsync("http://localhost:24217/api/Expense/getbyid?id=" + id);
                string result = response.Content.ReadAsStringAsync().Result;
                expense = JsonConvert.DeserializeObject<ExpenseDTO>(result);
            }
            return View(expense);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ExpenseDTO expense)
        {
            var expenseJson = JsonConvert.SerializeObject(expense);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/Expense/delete", httpContent);

                string result = response.Content.ReadAsStringAsync().Result;
            }
            TempData["Success"] = "Data Deleted Successful";
            return RedirectToAction("Index");
        }
    }
}
