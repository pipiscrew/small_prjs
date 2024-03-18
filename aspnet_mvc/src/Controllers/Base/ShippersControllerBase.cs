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
     /// Base class for ShippersController.  Do not make changes to this class,
     /// instead, put additional code in the ShippersController class 
     /// </summary>
     public class ShippersControllerBase : Controller
     { 

         /// <summary>
         /// GET: /Shippers/
         /// </summary>
         public ActionResult Index()
         {
             return View();
         }

         /// <summary>
         /// GET: /Shippers/Add
         /// </summary>
         public ActionResult Add()
         {
             return GetAddViewModel();
         }

         /// <summary>
         /// POST: /Shippers/Add
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Add(ShippersViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditShippers(viewModel, CrudOperation.Add);

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
             ShippersViewModel viewModel = new ShippersViewModel();
             viewModel.ShippersModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Shippers";
             viewModel.ViewActionName = "Add";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Shippers/Update/5
         /// </summary>
         public ActionResult Update(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// <summary>
         /// POST: /Shippers/Update/5
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Update(int id, ShippersViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditShippers(viewModel, CrudOperation.Update);

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
             Models.ShippersModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             ShippersViewModel viewModel = new ShippersViewModel();
             viewModel.ShippersModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Shippers";
             viewModel.ViewActionName = "Update";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// POST: /Shippers/Delete/5
         /// </summary>
         [HttpPost]
         public ActionResult Delete(int id, ShippersViewModel viewModel, string returnUrl)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.DeleteAsync("api/ShippersApi/Delete/" + id).Result;

                 if (!response.IsSuccessStatusCode)
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());

                 return Json(true);
             }
         }

         /// <summary>
         /// GET: /Shippers/Details/5
         /// </summary>
         public ActionResult Details(int id)
         {
             Models.ShippersModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             ShippersViewModel viewModel = new ShippersViewModel();
             viewModel.ShippersModel = model;

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Shippers/ListCrudRedirect
         /// </summary>
         public ActionResult ListCrudRedirect()
         {
             return View();
         }

         /// <summary>
         /// GET: /Shippers/ListReadOnly
         /// </summary>
         public ActionResult ListReadOnly()
         {
             return View();
         }

         /// <summary>
         /// GET: /Shippers/ListCrud
         /// </summary>
         public ActionResult ListCrud()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Shippers/ListCrud
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ListCrud(string inputSubmit, ShippersViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 CrudOperation operation = CrudOperation.Add;

                 if (inputSubmit == "Update")
                     operation = CrudOperation.Update;

                 try
                 {
                     AddEditShippers(viewModel, operation);
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Shippers/ListSearch
         /// </summary>
         public ActionResult ListSearch()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Shippers/ListScrollLoad
         /// </summary>
         public ActionResult ListScrollLoad()
         {
             return View();
         }

         /// <summary>
         /// GET: /Shippers/ListInline
         /// </summary>
         public ActionResult ListInline()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Shippers/ListInlineAdd
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineAdd(Models.ShippersModel model)
         {
             ShippersViewModel viewModel = new ShippersViewModel();
             viewModel.ShippersModel = model;

             AddEditShippers(viewModel, CrudOperation.Add);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// POST: /Shippers/ListInlineUpdate
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineUpdate(Models.ShippersModel model)
         {
             ShippersViewModel viewModel = new ShippersViewModel();
             viewModel.ShippersModel = model;

             AddEditShippers(viewModel, CrudOperation.Update);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// GET: /Shippers/ListForeach
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
                 HttpResponseMessage response = client.GetAsync("api/ShippersApi/GetRecordCount/").Result;

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
             ShippersCollection objShippersCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/ShippersApi/SelectSkipAndTake/?rows=" + rows + "&startRowIndex=" + startRowIndex + "&sortByExpression=" + sidx + " " + sord).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objShippersCol = JsonConvert.DeserializeObject<ShippersCollection>(responseBody);
                 }
             }

             // fields and titles
             string[,] fieldNames = new string[,] {
                 {"ShipperID", "Shipper ID"},
                 {"CompanyName", "Company Name"},
                 {"Phone", "Phone"}
             };

             // view model
             ShippersForeachViewModel viewModel = new ShippersForeachViewModel();
             viewModel.ShippersData = objShippersCol;
             viewModel.ShippersFieldNames = fieldNames;
             viewModel.TotalPages = totalPages;
             viewModel.CurrentPage = currentPage;
             viewModel.FieldToSort = String.IsNullOrEmpty(sidx) ? "ShipperID" : sidx;
             viewModel.FieldSortOrder = String.IsNullOrEmpty(sord) ? "asc" : sord;
             viewModel.FieldToSortWithOrder = String.IsNullOrEmpty(sidx) ? "ShipperID" : (sidx + " " + sord).Trim();
             viewModel.NumberOfPagesToShow = numberOfPagesToShow;
             viewModel.StartPage = Functions.GetPagerStartPage(currentPage, numberOfPagesToShow, totalPages);
             viewModel.EndPage = Functions.GetPagerEndPage(viewModel.StartPage, currentPage, numberOfPagesToShow, totalPages);

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Shippers/Unbound
         /// </summary>
         public ActionResult Unbound()
         {
             return View(GetUnboundViewModel());
         }

         /// <summary>
         /// POST: /Shippers/Unbound
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Unbound(ShippersViewModel viewModel, string returnUrl)
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

         private ShippersViewModel GetUnboundViewModel()
         {
             ShippersViewModel viewModel = new ShippersViewModel();
             viewModel.ShippersModel = null;
             viewModel.ViewControllerName = "Shippers";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "/";

             return viewModel;
         }

         /// <summary>
         /// GET: /Shippers/AddEditShippers
         /// </summary>
         private void AddEditShippers(ShippersViewModel viewModel, CrudOperation operation)
         {
             Models.ShippersModel model = viewModel.ShippersModel;
             string serializedModel = JsonConvert.SerializeObject(model);

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response;

                 if (operation == CrudOperation.Add)
                     response = client.PostAsync("api/ShippersApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
                 else
                     response = client.PostAsync("api/ShippersApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
             }
         }

         private ShippersViewModel GetViewModel()
         {
             ShippersViewModel viewModel = new ShippersViewModel();
             viewModel.ShippersModel = null;

             return viewModel;
         }

         private Models.ShippersModel GetModelByPrimaryKey(int shipperID)
         {
             Models.ShippersModel model = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/ShippersApi/SelectByPrimaryKey/" + shipperID).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     Shippers objShippers = JsonConvert.DeserializeObject<Shippers>(responseBody);

                     // assign values to the model
                     model = new Models.ShippersModel();
                     model.ShipperID = objShippers.ShipperID;
                     model.CompanyName = objShippers.CompanyName;
                     model.Phone = objShippers.Phone;
                 }
             }

             return model;
         }
     } 
} 
