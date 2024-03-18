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
     /// Base class for SuppliersController.  Do not make changes to this class,
     /// instead, put additional code in the SuppliersController class 
     /// </summary>
     public class SuppliersControllerBase : Controller
     { 

         /// <summary>
         /// GET: /Suppliers/
         /// </summary>
         public ActionResult Index()
         {
             return View();
         }

         /// <summary>
         /// GET: /Suppliers/Add
         /// </summary>
         public ActionResult Add()
         {
             return GetAddViewModel();
         }

         /// <summary>
         /// POST: /Suppliers/Add
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Add(SuppliersViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditSuppliers(viewModel, CrudOperation.Add);

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
             SuppliersViewModel viewModel = new SuppliersViewModel();
             viewModel.SuppliersModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Suppliers";
             viewModel.ViewActionName = "Add";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Suppliers/Update/5
         /// </summary>
         public ActionResult Update(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// <summary>
         /// POST: /Suppliers/Update/5
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Update(int id, SuppliersViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditSuppliers(viewModel, CrudOperation.Update);

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
             Models.SuppliersModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             SuppliersViewModel viewModel = new SuppliersViewModel();
             viewModel.SuppliersModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Suppliers";
             viewModel.ViewActionName = "Update";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// POST: /Suppliers/Delete/5
         /// </summary>
         [HttpPost]
         public ActionResult Delete(int id, SuppliersViewModel viewModel, string returnUrl)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.DeleteAsync("api/SuppliersApi/Delete/" + id).Result;

                 if (!response.IsSuccessStatusCode)
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());

                 return Json(true);
             }
         }

         /// <summary>
         /// GET: /Suppliers/Details/5
         /// </summary>
         public ActionResult Details(int id)
         {
             Models.SuppliersModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             SuppliersViewModel viewModel = new SuppliersViewModel();
             viewModel.SuppliersModel = model;

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Suppliers/ListCrudRedirect
         /// </summary>
         public ActionResult ListCrudRedirect()
         {
             return View();
         }

         /// <summary>
         /// GET: /Suppliers/ListReadOnly
         /// </summary>
         public ActionResult ListReadOnly()
         {
             return View();
         }

         /// <summary>
         /// GET: /Suppliers/ListCrud
         /// </summary>
         public ActionResult ListCrud()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Suppliers/ListCrud
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ListCrud(string inputSubmit, SuppliersViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 CrudOperation operation = CrudOperation.Add;

                 if (inputSubmit == "Update")
                     operation = CrudOperation.Update;

                 try
                 {
                     AddEditSuppliers(viewModel, operation);
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Suppliers/ListSearch
         /// </summary>
         public ActionResult ListSearch()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Suppliers/ListScrollLoad
         /// </summary>
         public ActionResult ListScrollLoad()
         {
             return View();
         }

         /// <summary>
         /// GET: /Suppliers/ListInline
         /// </summary>
         public ActionResult ListInline()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Suppliers/ListInlineAdd
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineAdd(Models.SuppliersModel model)
         {
             SuppliersViewModel viewModel = new SuppliersViewModel();
             viewModel.SuppliersModel = model;

             AddEditSuppliers(viewModel, CrudOperation.Add);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// POST: /Suppliers/ListInlineUpdate
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineUpdate(Models.SuppliersModel model)
         {
             SuppliersViewModel viewModel = new SuppliersViewModel();
             viewModel.SuppliersModel = model;

             AddEditSuppliers(viewModel, CrudOperation.Update);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// GET: /Suppliers/ListForeach
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
                 HttpResponseMessage response = client.GetAsync("api/SuppliersApi/GetRecordCount/").Result;

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
             SuppliersCollection objSuppliersCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/SuppliersApi/SelectSkipAndTake/?rows=" + rows + "&startRowIndex=" + startRowIndex + "&sortByExpression=" + sidx + " " + sord).Result;

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

             // fields and titles
             string[,] fieldNames = new string[,] {
                 {"SupplierID", "Supplier ID"},
                 {"CompanyName", "Company Name"},
                 {"ContactName", "Contact Name"},
                 {"ContactTitle", "Contact Title"},
                 {"Address", "Address"},
                 {"City", "City"},
                 {"Region1", "Region1"},
                 {"PostalCode", "Postal Code"},
                 {"Country", "Country"},
                 {"Phone", "Phone"},
                 {"Fax", "Fax"},
                 {"HomePage", "Home Page"}
             };

             // view model
             SuppliersForeachViewModel viewModel = new SuppliersForeachViewModel();
             viewModel.SuppliersData = objSuppliersCol;
             viewModel.SuppliersFieldNames = fieldNames;
             viewModel.TotalPages = totalPages;
             viewModel.CurrentPage = currentPage;
             viewModel.FieldToSort = String.IsNullOrEmpty(sidx) ? "SupplierID" : sidx;
             viewModel.FieldSortOrder = String.IsNullOrEmpty(sord) ? "asc" : sord;
             viewModel.FieldToSortWithOrder = String.IsNullOrEmpty(sidx) ? "SupplierID" : (sidx + " " + sord).Trim();
             viewModel.NumberOfPagesToShow = numberOfPagesToShow;
             viewModel.StartPage = Functions.GetPagerStartPage(currentPage, numberOfPagesToShow, totalPages);
             viewModel.EndPage = Functions.GetPagerEndPage(viewModel.StartPage, currentPage, numberOfPagesToShow, totalPages);

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Suppliers/Unbound
         /// </summary>
         public ActionResult Unbound()
         {
             return View(GetUnboundViewModel());
         }

         /// <summary>
         /// POST: /Suppliers/Unbound
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Unbound(SuppliersViewModel viewModel, string returnUrl)
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

         private SuppliersViewModel GetUnboundViewModel()
         {
             SuppliersViewModel viewModel = new SuppliersViewModel();
             viewModel.SuppliersModel = null;
             viewModel.ViewControllerName = "Suppliers";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "/";

             return viewModel;
         }

         /// <summary>
         /// GET: /Suppliers/AddEditSuppliers
         /// </summary>
         private void AddEditSuppliers(SuppliersViewModel viewModel, CrudOperation operation)
         {
             Models.SuppliersModel model = viewModel.SuppliersModel;
             string serializedModel = JsonConvert.SerializeObject(model);

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response;

                 if (operation == CrudOperation.Add)
                     response = client.PostAsync("api/SuppliersApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
                 else
                     response = client.PostAsync("api/SuppliersApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
             }
         }

         private SuppliersViewModel GetViewModel()
         {
             SuppliersViewModel viewModel = new SuppliersViewModel();
             viewModel.SuppliersModel = null;

             return viewModel;
         }

         private Models.SuppliersModel GetModelByPrimaryKey(int supplierID)
         {
             Models.SuppliersModel model = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/SuppliersApi/SelectByPrimaryKey/" + supplierID).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     Suppliers objSuppliers = JsonConvert.DeserializeObject<Suppliers>(responseBody);

                     // assign values to the model
                     model = new Models.SuppliersModel();
                     model.SupplierID = objSuppliers.SupplierID;
                     model.CompanyName = objSuppliers.CompanyName;
                     model.ContactName = objSuppliers.ContactName;
                     model.ContactTitle = objSuppliers.ContactTitle;
                     model.Address = objSuppliers.Address;
                     model.City = objSuppliers.City;
                     model.Region1 = objSuppliers.Region1;
                     model.PostalCode = objSuppliers.PostalCode;
                     model.Country = objSuppliers.Country;
                     model.Phone = objSuppliers.Phone;
                     model.Fax = objSuppliers.Fax;
                     model.HomePage = objSuppliers.HomePage;
                 }
             }

             return model;
         }
     } 
} 
