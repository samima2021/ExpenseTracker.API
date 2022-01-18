using ExpenseTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace ExpenseTracker.Web.Controllers
{
    public class ExpenseUIController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ExpenseDTO> dTOs = new List<ExpenseDTO>();
            var expense = dTOs;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/Expense");

                string result = response.Content.ReadAsStringAsync().Result;
                expense = JsonConvert.DeserializeObject<List<ExpenseDTO>>(result);
            }
            return View(expense);
        }
        public async Task<IActionResult> Create(string id)
        {
            var ExpenseCategory = new List<ExpenseCategoryDTO>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/ExpenseCategory");

                string result = response.Content.ReadAsStringAsync().Result;
                ExpenseCategory = JsonConvert.DeserializeObject<List<ExpenseCategoryDTO>>(result);
            }
            var expense = new ExpenseDTO();
            expense.DropCategoryList = ExpenseCategory;
            return View(expense);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExpenseDTO expense)
        {
            var expenseJson = JsonConvert.SerializeObject(expense);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/Expense", httpContent);

                string result = response.Content.ReadAsStringAsync().Result;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
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

            expenseD.DropCategoryList = ExpenseCategory;
            return View(expenseD);
        }

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
            return RedirectToAction("Index");
        }

       
    }
}
