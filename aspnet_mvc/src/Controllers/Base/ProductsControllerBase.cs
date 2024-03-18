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
     /// Base class for ProductsController.  Do not make changes to this class,
     /// instead, put additional code in the ProductsController class 
     /// </summary>
     public class ProductsControllerBase : Controller
     { 

         /// <summary>
         /// GET: /Products/
         /// </summary>
         public ActionResult Index()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/Add
         /// </summary>
         public ActionResult Add()
         {
             return GetAddViewModel();
         }

         /// <summary>
         /// POST: /Products/Add
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Add(ProductsViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditProducts(viewModel, CrudOperation.Add);

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
             ProductsViewModel viewModel = new ProductsViewModel();
             viewModel.ProductsModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Products";
             viewModel.ViewActionName = "Add";
             viewModel.SuppliersDropDownListData = GetSuppliersDropDownListData();
             viewModel.CategoriesDropDownListData = GetCategoriesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Products/Update/5
         /// </summary>
         public ActionResult Update(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// <summary>
         /// POST: /Products/Update/5
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Update(int id, ProductsViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditProducts(viewModel, CrudOperation.Update);

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
             Models.ProductsModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             ProductsViewModel viewModel = new ProductsViewModel();
             viewModel.ProductsModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Products";
             viewModel.ViewActionName = "Update";
             viewModel.SuppliersDropDownListData = GetSuppliersDropDownListData();
             viewModel.CategoriesDropDownListData = GetCategoriesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// POST: /Products/Delete/5
         /// </summary>
         [HttpPost]
         public ActionResult Delete(int id, ProductsViewModel viewModel, string returnUrl)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.DeleteAsync("api/ProductsApi/Delete/" + id).Result;

                 if (!response.IsSuccessStatusCode)
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());

                 return Json(true);
             }
         }

         /// <summary>
         /// GET: /Products/Details/5
         /// </summary>
         public ActionResult Details(int id)
         {
             Models.ProductsModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             ProductsViewModel viewModel = new ProductsViewModel();
             viewModel.ProductsModel = model;
             viewModel.SuppliersDropDownListData = GetSuppliersDropDownListData();
             viewModel.CategoriesDropDownListData = GetCategoriesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Products/ListCrudRedirect
         /// </summary>
         public ActionResult ListCrudRedirect()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListReadOnly
         /// </summary>
         public ActionResult ListReadOnly()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListCrud
         /// </summary>
         public ActionResult ListCrud()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Products/ListCrud
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ListCrud(string inputSubmit, ProductsViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 CrudOperation operation = CrudOperation.Add;

                 if (inputSubmit == "Update")
                     operation = CrudOperation.Update;

                 try
                 {
                     AddEditProducts(viewModel, operation);
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Products/ListTotals
         /// </summary>
         public ActionResult ListTotals()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListSearch
         /// </summary>
         public ActionResult ListSearch()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Products/ListScrollLoad
         /// </summary>
         public ActionResult ListScrollLoad()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListInline
         /// </summary>
         public ActionResult ListInline()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Products/ListInlineAdd
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineAdd(Models.ProductsModel model)
         {
             ProductsViewModel viewModel = new ProductsViewModel();
             viewModel.ProductsModel = model;

             AddEditProducts(viewModel, CrudOperation.Add);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// POST: /Products/ListInlineUpdate
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineUpdate(Models.ProductsModel model)
         {
             ProductsViewModel viewModel = new ProductsViewModel();
             viewModel.ProductsModel = model;

             AddEditProducts(viewModel, CrudOperation.Update);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// GET: /Products/ListForeach
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
                 HttpResponseMessage response = client.GetAsync("api/ProductsApi/GetRecordCount/").Result;

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
             ProductsCollection objProductsCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/ProductsApi/SelectSkipAndTake/?rows=" + rows + "&startRowIndex=" + startRowIndex + "&sortByExpression=" + sidx + " " + sord).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objProductsCol = JsonConvert.DeserializeObject<ProductsCollection>(responseBody);
                 }
             }

             // fields and titles
             string[,] fieldNames = new string[,] {
                 {"ProductID", "Product ID"},
                 {"ProductName", "Product Name"},
                 {"SupplierID", "Supplier ID"},
                 {"CategoryID", "Category ID"},
                 {"QuantityPerUnit", "Quantity Per Unit"},
                 {"UnitPrice", "Unit Price"},
                 {"UnitsInStock", "Units In Stock"},
                 {"UnitsOnOrder", "Units On Order"},
                 {"ReorderLevel", "Reorder Level"},
                 {"Discontinued", "Discontinued"}
             };

             // view model
             ProductsForeachViewModel viewModel = new ProductsForeachViewModel();
             viewModel.ProductsData = objProductsCol;
             viewModel.ProductsFieldNames = fieldNames;
             viewModel.TotalPages = totalPages;
             viewModel.CurrentPage = currentPage;
             viewModel.FieldToSort = String.IsNullOrEmpty(sidx) ? "ProductID" : sidx;
             viewModel.FieldSortOrder = String.IsNullOrEmpty(sord) ? "asc" : sord;
             viewModel.FieldToSortWithOrder = String.IsNullOrEmpty(sidx) ? "ProductID" : (sidx + " " + sord).Trim();
             viewModel.NumberOfPagesToShow = numberOfPagesToShow;
             viewModel.StartPage = Functions.GetPagerStartPage(currentPage, numberOfPagesToShow, totalPages);
             viewModel.EndPage = Functions.GetPagerEndPage(viewModel.StartPage, currentPage, numberOfPagesToShow, totalPages);

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Products/ListMasterDetailGridBySupplierID
         /// </summary>
         public ActionResult ListMasterDetailGridBySupplierID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListMasterDetailGridByCategoryID
         /// </summary>
         public ActionResult ListMasterDetailGridByCategoryID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListMasterDetailSubGridBySupplierID
         /// </summary>
         public ActionResult ListMasterDetailSubGridBySupplierID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListMasterDetailSubGridByCategoryID
         /// </summary>
         public ActionResult ListMasterDetailSubGridByCategoryID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/Unbound
         /// </summary>
         public ActionResult Unbound()
         {
             return View(GetUnboundViewModel());
         }

         /// <summary>
         /// POST: /Products/Unbound
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Unbound(ProductsViewModel viewModel, string returnUrl)
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

         private ProductsViewModel GetUnboundViewModel()
         {
             ProductsViewModel viewModel = new ProductsViewModel();
             viewModel.ProductsModel = null;
             viewModel.ViewControllerName = "Products";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "/";

             return viewModel;
         }

         /// <summary>
         /// GET: /Products/AddEditProducts
         /// </summary>
         private void AddEditProducts(ProductsViewModel viewModel, CrudOperation operation)
         {
             Models.ProductsModel model = viewModel.ProductsModel;
             string serializedModel = JsonConvert.SerializeObject(model);

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response;

                 if (operation == CrudOperation.Add)
                     response = client.PostAsync("api/ProductsApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
                 else
                     response = client.PostAsync("api/ProductsApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
             }
         }

         private ProductsViewModel GetViewModel()
         {
             ProductsViewModel viewModel = new ProductsViewModel();
             viewModel.ProductsModel = null;
             viewModel.SuppliersDropDownListData = GetSuppliersDropDownListData();
             viewModel.CategoriesDropDownListData = GetCategoriesDropDownListData();

             return viewModel;
         }

         /// <summary>
         /// GET: /Products/ListGroupedBySupplierID
         /// </summary>
         public ActionResult ListGroupedBySupplierID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListGroupedByCategoryID
         /// </summary>
         public ActionResult ListGroupedByCategoryID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListTotalsGroupedBySupplierID
         /// </summary>
         public ActionResult ListTotalsGroupedBySupplierID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Products/ListTotalsGroupedByCategoryID
         /// </summary>
         public ActionResult ListTotalsGroupedByCategoryID()
         {
             return View();
         }

         private Models.ProductsModel GetModelByPrimaryKey(int productID)
         {
             Models.ProductsModel model = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/ProductsApi/SelectByPrimaryKey/" + productID).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     Products objProducts = JsonConvert.DeserializeObject<Products>(responseBody);

                     // assign values to the model
                     model = new Models.ProductsModel();
                     model.ProductID = objProducts.ProductID;
                     model.ProductName = objProducts.ProductName;
                     model.SupplierID = objProducts.SupplierID;
                     model.CategoryID = objProducts.CategoryID;
                     model.QuantityPerUnit = objProducts.QuantityPerUnit;
                     model.UnitPrice = objProducts.UnitPrice;
                     model.UnitsInStock = objProducts.UnitsInStock;
                     model.UnitsOnOrder = objProducts.UnitsOnOrder;
                     model.ReorderLevel = objProducts.ReorderLevel;
                     model.Discontinued = objProducts.Discontinued;
                 }
             }

             return model;
         }

         private SuppliersCollection GetSuppliersDropDownListData()
         {
             SuppliersCollection objSuppliersCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/SuppliersApi/SelectSuppliersDropDownListData/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objSuppliersCol = JsonConvert.DeserializeObject<SuppliersCollection>(responseBody);
                 }
             }

             return objSuppliersCol;
         }

         private CategoriesCollection GetCategoriesDropDownListData()
         {
             CategoriesCollection objCategoriesCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/CategoriesApi/SelectCategoriesDropDownListData/").Result;

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

             return objCategoriesCol;
         }
     } 
} 
