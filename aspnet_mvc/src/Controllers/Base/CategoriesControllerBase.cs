using System;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using north;
using north.BusinessObject;
using north.Models;
using north.ViewModels;
using north.Domain;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace north.Controllers.Base
{ 
     /// <summary>
     /// Base class for CategoriesController.  Do not make changes to this class,
     /// instead, put additional code in the CategoriesController class 
     /// </summary>
     public class CategoriesControllerBase : Controller
     { 

         /// <summary>
         /// GET: /Categories/
         /// </summary>
         public ActionResult Index()
         {
             return View();
         }

         /// <summary>
         /// GET: /Categories/Add
         /// </summary>
         public ActionResult Add()
         {
             return GetAddViewModel();
         }

         /// <summary>
         /// POST: /Categories/Add
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Add(CategoriesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditCategories(viewModel, CrudOperation.Add);

                     if (Url.IsLocalUrl(returnUrl))
                         return Redirect(returnUrl);
                     else
                         return RedirectToAction("Index");
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             // if we got this far, something failed, redisplay form
             return GetAddViewModel();
         }

         private ActionResult GetAddViewModel()
         {
             CategoriesViewModel viewModel = new CategoriesViewModel();
             viewModel.CategoriesModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Categories";
             viewModel.ViewActionName = "Add";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Categories/Update/5
         /// </summary>
         public ActionResult Update(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// <summary>
         /// POST: /Categories/Update/5
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Update(int id, CategoriesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditCategories(viewModel, CrudOperation.Update);

                     if (Url.IsLocalUrl(returnUrl))
                         return Redirect(returnUrl);
                     else
                         return RedirectToAction("Index");
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             // if we got this far, something failed, redisplay form
             return GetUpdateViewModel(id);
         }

         public ActionResult GetUpdateViewModel(int id)
         {
             Models.CategoriesModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             CategoriesViewModel viewModel = new CategoriesViewModel();
             viewModel.CategoriesModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Categories";
             viewModel.ViewActionName = "Update";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// POST: /Categories/Delete/5
         /// </summary>
         [HttpPost]
         public ActionResult Delete(int id, CategoriesViewModel viewModel, string returnUrl)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.DeleteAsync("api/CategoriesApi/Delete/" + id).Result;

                 if (!response.IsSuccessStatusCode)
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());

                 return Json(true);
             }
         }

         /// <summary>
         /// GET: /Categories/Details/5
         /// </summary>
         public ActionResult Details(int id)
         {
             Models.CategoriesModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             CategoriesViewModel viewModel = new CategoriesViewModel();
             viewModel.CategoriesModel = model;

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Categories/ListCrudRedirect
         /// </summary>
         public ActionResult ListCrudRedirect()
         {
             return View();
         }

         /// <summary>
         /// GET: /Categories/ListReadOnly
         /// </summary>
         public ActionResult ListReadOnly()
         {
             return View();
         }

         /// <summary>
         /// GET: /Categories/ListCrud
         /// </summary>
         public ActionResult ListCrud()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Categories/ListCrud
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ListCrud(string inputSubmit, CategoriesViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 CrudOperation operation = CrudOperation.Add;

                 if (inputSubmit == "Update")
                     operation = CrudOperation.Update;

                 try
                 {
                     AddEditCategories(viewModel, operation);
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Categories/ListSearch
         /// </summary>
         public ActionResult ListSearch()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Categories/ListScrollLoad
         /// </summary>
         public ActionResult ListScrollLoad()
         {
             return View();
         }

         /// <summary>
         /// GET: /Categories/ListInline
         /// </summary>
         public ActionResult ListInline()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Categories/ListInlineAdd
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineAdd(Models.CategoriesModel model)
         {
             CategoriesViewModel viewModel = new CategoriesViewModel();
             viewModel.CategoriesModel = model;

             AddEditCategories(viewModel, CrudOperation.Add);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// POST: /Categories/ListInlineUpdate
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineUpdate(Models.CategoriesModel model)
         {
             CategoriesViewModel viewModel = new CategoriesViewModel();
             viewModel.CategoriesModel = model;

             AddEditCategories(viewModel, CrudOperation.Update);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// GET: /Categories/ListForeach
         /// </summary>
         public ActionResult ListForeach(string sidx, string sord, int? page)
         {
             int rows = Convert.ToInt32(ConfigurationManager.AppSettings["GridNumberOfRows"]);
             int numberOfPagesToShow = Convert.ToInt32(ConfigurationManager.AppSettings["GridNumberOfPagesToShow"]);
             int currentPage = page == null ? 1 : Convert.ToInt32(page);
             int startRowIndex = ((currentPage * rows) - rows) + 1;
             int totalRecords = 0;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/CategoriesApi/GetRecordCount/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     totalRecords = JsonConvert.DeserializeObject<int>(responseBody);
                 }
             }

             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
             CategoriesCollection objCategoriesCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/CategoriesApi/SelectSkipAndTake/?rows=" + rows + "&startRowIndex=" + startRowIndex + "&sortByExpression=" + sidx + " " + sord).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objCategoriesCol = JsonConvert.DeserializeObject<CategoriesCollection>(responseBody);
                 }
             }

             // fields and titles
             string[,] fieldNames = new string[,] {
                 {"CategoryID", "Category ID"},
                 {"CategoryName", "Category Name"},
                 {"Description", "Description"}
             };

             // view model
             CategoriesForeachViewModel viewModel = new CategoriesForeachViewModel();
             viewModel.CategoriesData = objCategoriesCol;
             viewModel.CategoriesFieldNames = fieldNames;
             viewModel.TotalPages = totalPages;
             viewModel.CurrentPage = currentPage;
             viewModel.FieldToSort = String.IsNullOrEmpty(sidx) ? "CategoryID" : sidx;
             viewModel.FieldSortOrder = String.IsNullOrEmpty(sord) ? "asc" : sord;
             viewModel.FieldToSortWithOrder = String.IsNullOrEmpty(sidx) ? "CategoryID" : (sidx + " " + sord).Trim();
             viewModel.NumberOfPagesToShow = numberOfPagesToShow;
             viewModel.StartPage = Functions.GetPagerStartPage(currentPage, numberOfPagesToShow, totalPages);
             viewModel.EndPage = Functions.GetPagerEndPage(viewModel.StartPage, currentPage, numberOfPagesToShow, totalPages);

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Categories/Unbound
         /// </summary>
         public ActionResult Unbound()
         {
             return View(GetUnboundViewModel());
         }

         /// <summary>
         /// POST: /Categories/Unbound
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Unbound(CategoriesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 // do something here before redirecting

                 if (Url.IsLocalUrl(returnUrl))
                     return Redirect(returnUrl);
                 else
                     return RedirectToAction("/");
             }

             // if we got this far, something failed, redisplay form
             return View(GetUnboundViewModel());
         }

         private CategoriesViewModel GetUnboundViewModel()
         {
             CategoriesViewModel viewModel = new CategoriesViewModel();
             viewModel.CategoriesModel = null;
             viewModel.ViewControllerName = "Categories";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "/";

             return viewModel;
         }

         /// <summary>
         /// GET: /Categories/AddEditCategories
         /// </summary>
         private void AddEditCategories(CategoriesViewModel viewModel, CrudOperation operation)
         {
             Models.CategoriesModel model = viewModel.CategoriesModel;
             string serializedModel = JsonConvert.SerializeObject(model);

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response;

                 if (operation == CrudOperation.Add)
                     response = client.PostAsync("api/CategoriesApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
                 else
                     response = client.PostAsync("api/CategoriesApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
             }
         }

         private CategoriesViewModel GetViewModel()
         {
             CategoriesViewModel viewModel = new CategoriesViewModel();
             viewModel.CategoriesModel = null;

             return viewModel;
         }

         private Models.CategoriesModel GetModelByPrimaryKey(int categoryID)
         {
             Models.CategoriesModel model = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/CategoriesApi/SelectByPrimaryKey/" + categoryID).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     Categories objCategories = JsonConvert.DeserializeObject<Categories>(responseBody);

                     // assign values to the model
                     model = new Models.CategoriesModel();
                     model.CategoryID = objCategories.CategoryID;
                     model.CategoryName = objCategories.CategoryName;
                     model.Description = objCategories.Description;
                 }
             }

             return model;
         }
     } 
} 
