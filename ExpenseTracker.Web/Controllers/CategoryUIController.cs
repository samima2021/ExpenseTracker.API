using ExpenseTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.Controllers
{
    public class CategoryUIController : Controller
    {

        #region Index
        public async Task<IActionResult> Index()
        {
                var category = new List<ExpenseCategoryDTO>();

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("http://localhost:24217/api/ExpenseCategory");

                    string result = response.Content.ReadAsStringAsync().Result;
                    category = JsonConvert.DeserializeObject<List<ExpenseCategoryDTO>>(result);
                }
                return View(category);
            
            #endregion
        }
        #region HttpGet-Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var expense= new List<ExpenseDTO>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/Expense");

                string result = response.Content.ReadAsStringAsync().Result;
                expense = JsonConvert.DeserializeObject<List<ExpenseDTO>>(result);
            }

            var category= new ExpenseCategoryDTO();
           

            return View(category);
        }
        #endregion

        #region HttpPost-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCategoryDTO categoryexpense)
        {
            var categoryJson = JsonConvert.SerializeObject(categoryexpense);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/ExpenseCategory", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    categoryexpense.IsDuplicateFound = true;
                    ModelState.AddModelError("CategoryName", "Duplicate found");
                    return View(categoryexpense);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var Expense = new List<ExpenseDTO>();
            var expenseD = new ExpenseCategoryDTO();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/Expense");
                string result = response.Content.ReadAsStringAsync().Result;
                Expense = JsonConvert.DeserializeObject<List<ExpenseDTO>>(result);

                var exResponse = await client.GetAsync("http://localhost:24217/api/ExpenseCategory/getbyid?id=" + id);
                string result2 = exResponse.Content.ReadAsStringAsync().Result;
                expenseD = JsonConvert.DeserializeObject<ExpenseCategoryDTO>(result2);
            }

            
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
                var response = await client.PostAsync("http://localhost:24217/api/ExpenseCategory", httpContent);

                string result = response.Content.ReadAsStringAsync().Result;
            }
            return RedirectToAction("Index");
        }

        #endregion
        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var category = new ExpenseCategoryDTO();
        //    using (var client = new HttpClient())
        //    {

        //        var response = await client.GetAsync("http://localhost:24217/api/ExpenseCategory/getbyid?id=" + id);
        //        string result = response.Content.ReadAsStringAsync().Result;
        //        category = JsonConvert.DeserializeObject<ExpenseCategoryDTO>(result);
        //    }
        //    return View(category);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Delete(ExpenseCategoryDTO categoryexpense)
        //{
        //    var categoryJson = JsonConvert.SerializeObject(categoryexpense);
        //    using (var client = new HttpClient())
        //    {
        //        HttpContent httpContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync("http://localhost:24217/api/ExpenseCategory/delete", httpContent);

        //        string result = response.Content.ReadAsStringAsync().Result;
        //    }
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(ExpenseCategoryDTO dto)
        {
            var expenseJson = JsonConvert.SerializeObject(dto);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/ExpenseCategory/delete", httpContent);

                string result = response.Content.ReadAsStringAsync().Result;
            }
            return RedirectToAction("Index");
        }

    }
}
