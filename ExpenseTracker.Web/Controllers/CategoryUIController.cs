using ExpenseTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.Controllers
{
    public class CategoryUIController : Controller
    {
        #region Index

        //public async Task<IActionResult> Index()
        //{
        //    var category = new List<ExpenseCategoryDTO>();

        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("http://localhost:24217/api/ExpenseCategory");

        //        string result = response.Content.ReadAsStringAsync().Result;
        //        category = JsonConvert.DeserializeObject<List<ExpenseCategoryDTO>>(result);
        //    }
        //    TempData["Success"] = "Data Created Successful";
        //    return View(category);
        //}
        public async Task<IActionResult> Index(string filter, int pageIndex = 1, string sortExpression = "CategoryName")
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:24217/api/ExpenseCategory");

                string result = response.Content.ReadAsStringAsync().Result;
                var category = JsonConvert.DeserializeObject<List<ExpenseCategoryDTO>>(result);
                category = category.OrderBy(i => i.ExpenseID).ToList();


                if (!string.IsNullOrWhiteSpace(filter))
                {

                    category = category.Where(p => p.CategoryName.Contains(filter)).ToList();

                }
                var pagedExpense = PagingList.Create(category, 3, pageIndex, sortExpression, "CompanyName");

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
            var category = new ExpenseCategoryDTO();
            return View(category);
        }
        #endregion

        #region HttpPost-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCategoryDTO category)
        {
            var expenseJson = JsonConvert.SerializeObject(category);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/ExpenseCategory", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    category.IsDuplicateFound = true;
                    ModelState.AddModelError("CategoryName", "Duplicate Found!");
                    return View(category);
                }

            }
            TempData["Success"] = "Data Updated Successful";
            return RedirectToAction("Index");
        }
        #endregion
        #region HttpGet-Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var expenseD = new ExpenseCategoryDTO();
            using (var client = new HttpClient())
            {
                var exResponse = await client.GetAsync("http://localhost:24217/api/ExpenseCategory/getbyid?id=" + id);
                string result2 = exResponse.Content.ReadAsStringAsync().Result;
                expenseD = JsonConvert.DeserializeObject<ExpenseCategoryDTO>(result2);
            }

            return View(expenseD);
        }
        #endregion

        #region HttpPost-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpenseCategoryDTO category)
        {
            var expenseJson = JsonConvert.SerializeObject(category);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/ExpenseCategory", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    category.IsDuplicateFound = true;
                    ModelState.AddModelError("CategoryName", "Duplicate Found!");
                    return View(category);
                }

            }
            return RedirectToAction("Index");
        }
        #endregion
        #region HttpPost-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ExpenseCategoryDTO dto)
        {
            var expenseJson = JsonConvert.SerializeObject(dto);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(expenseJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:24217/api/ExpenseCategory/delete", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    ModelState.AddModelError("CategoryID", "You can't Delete this!");
                    TempData["DeleteError"] = "You can't Delete!";

                    return RedirectToAction("Index");
                }
            }
            TempData["Success"] = "Data Deleted Successful";
            return RedirectToAction("Index");
        }
        #endregion
    }
}

    

