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
     /// Base class for EmployeesController.  Do not make changes to this class,
     /// instead, put additional code in the EmployeesController class 
     /// </summary>
     public class EmployeesControllerBase : Controller
     { 

         /// <summary>
         /// GET: /Employees/
         /// </summary>
         public ActionResult Index()
         {
             return View();
         }

         /// <summary>
         /// GET: /Employees/Add
         /// </summary>
         public ActionResult Add()
         {
             return GetAddViewModel();
         }

         /// <summary>
         /// POST: /Employees/Add
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Add(EmployeesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditEmployees(viewModel, CrudOperation.Add);

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
             EmployeesViewModel viewModel = new EmployeesViewModel();
             viewModel.EmployeesModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Employees";
             viewModel.ViewActionName = "Add";
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Employees/Update/5
         /// </summary>
         public ActionResult Update(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// <summary>
         /// POST: /Employees/Update/5
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Update(int id, EmployeesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditEmployees(viewModel, CrudOperation.Update);

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
             Models.EmployeesModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             EmployeesViewModel viewModel = new EmployeesViewModel();
             viewModel.EmployeesModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Employees";
             viewModel.ViewActionName = "Update";
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// POST: /Employees/Delete/5
         /// </summary>
         [HttpPost]
         public ActionResult Delete(int id, EmployeesViewModel viewModel, string returnUrl)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.DeleteAsync("api/EmployeesApi/Delete/" + id).Result;

                 if (!response.IsSuccessStatusCode)
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());

                 return Json(true);
             }
         }

         /// <summary>
         /// GET: /Employees/Details/5
         /// </summary>
         public ActionResult Details(int id)
         {
             Models.EmployeesModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             EmployeesViewModel viewModel = new EmployeesViewModel();
             viewModel.EmployeesModel = model;
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Employees/ListCrudRedirect
         /// </summary>
         public ActionResult ListCrudRedirect()
         {
             return View();
         }

         /// <summary>
         /// GET: /Employees/ListReadOnly
         /// </summary>
         public ActionResult ListReadOnly()
         {
             return View();
         }

         /// <summary>
         /// GET: /Employees/ListCrud
         /// </summary>
         public ActionResult ListCrud()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Employees/ListCrud
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ListCrud(string inputSubmit, EmployeesViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 CrudOperation operation = CrudOperation.Add;

                 if (inputSubmit == "Update")
                     operation = CrudOperation.Update;

                 try
                 {
                     AddEditEmployees(viewModel, operation);
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Employees/ListSearch
         /// </summary>
         public ActionResult ListSearch()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Employees/ListScrollLoad
         /// </summary>
         public ActionResult ListScrollLoad()
         {
             return View();
         }

         /// <summary>
         /// GET: /Employees/ListInline
         /// </summary>
         public ActionResult ListInline()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Employees/ListInlineAdd
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineAdd(Models.EmployeesModel model)
         {
             EmployeesViewModel viewModel = new EmployeesViewModel();
             viewModel.EmployeesModel = model;

             AddEditEmployees(viewModel, CrudOperation.Add);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// POST: /Employees/ListInlineUpdate
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineUpdate(Models.EmployeesModel model)
         {
             EmployeesViewModel viewModel = new EmployeesViewModel();
             viewModel.EmployeesModel = model;

             AddEditEmployees(viewModel, CrudOperation.Update);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// GET: /Employees/ListForeach
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
                 HttpResponseMessage response = client.GetAsync("api/EmployeesApi/GetRecordCount/").Result;

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
             EmployeesCollection objEmployeesCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/EmployeesApi/SelectSkipAndTake/?rows=" + rows + "&startRowIndex=" + startRowIndex + "&sortByExpression=" + sidx + " " + sord).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objEmployeesCol = JsonConvert.DeserializeObject<EmployeesCollection>(responseBody);
                 }
             }

             // fields and titles
             string[,] fieldNames = new string[,] {
                 {"EmployeeID", "Employee ID"},
                 {"LastName", "Last Name"},
                 {"FirstName", "First Name"},
                 {"Title", "Title"},
                 {"TitleOfCourtesy", "Title Of Courtesy"},
                 {"BirthDate", "Birth Date"},
                 {"HireDate", "Hire Date"},
                 {"Address", "Address"},
                 {"City", "City"},
                 {"Region1", "Region1"},
                 {"PostalCode", "Postal Code"},
                 {"Country", "Country"},
                 {"HomePhone", "Home Phone"},
                 {"Extension", "Extension"},
                 {"Notes", "Notes"},
                 {"ReportsTo", "Reports To"},
                 {"PhotoPath", "Photo Path"}
             };

             // view model
             EmployeesForeachViewModel viewModel = new EmployeesForeachViewModel();
             viewModel.EmployeesData = objEmployeesCol;
             viewModel.EmployeesFieldNames = fieldNames;
             viewModel.TotalPages = totalPages;
             viewModel.CurrentPage = currentPage;
             viewModel.FieldToSort = String.IsNullOrEmpty(sidx) ? "EmployeeID" : sidx;
             viewModel.FieldSortOrder = String.IsNullOrEmpty(sord) ? "asc" : sord;
             viewModel.FieldToSortWithOrder = String.IsNullOrEmpty(sidx) ? "EmployeeID" : (sidx + " " + sord).Trim();
             viewModel.NumberOfPagesToShow = numberOfPagesToShow;
             viewModel.StartPage = Functions.GetPagerStartPage(currentPage, numberOfPagesToShow, totalPages);
             viewModel.EndPage = Functions.GetPagerEndPage(viewModel.StartPage, currentPage, numberOfPagesToShow, totalPages);

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Employees/ListMasterDetailGridByReportsTo
         /// </summary>
         public ActionResult ListMasterDetailGridByReportsTo()
         {
             return View();
         }

         /// <summary>
         /// GET: /Employees/ListMasterDetailSubGridByReportsTo
         /// </summary>
         public ActionResult ListMasterDetailSubGridByReportsTo()
         {
             return View();
         }

         /// <summary>
         /// GET: /Employees/Unbound
         /// </summary>
         public ActionResult Unbound()
         {
             return View(GetUnboundViewModel());
         }

         /// <summary>
         /// POST: /Employees/Unbound
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Unbound(EmployeesViewModel viewModel, string returnUrl)
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

         private EmployeesViewModel GetUnboundViewModel()
         {
             EmployeesViewModel viewModel = new EmployeesViewModel();
             viewModel.EmployeesModel = null;
             viewModel.ViewControllerName = "Employees";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "/";

             return viewModel;
         }

         /// <summary>
         /// GET: /Employees/AddEditEmployees
         /// </summary>
         private void AddEditEmployees(EmployeesViewModel viewModel, CrudOperation operation)
         {
             Models.EmployeesModel model = viewModel.EmployeesModel;
             string serializedModel = JsonConvert.SerializeObject(model);

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response;

                 if (operation == CrudOperation.Add)
                     response = client.PostAsync("api/EmployeesApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
                 else
                     response = client.PostAsync("api/EmployeesApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
             }
         }

         private EmployeesViewModel GetViewModel()
         {
             EmployeesViewModel viewModel = new EmployeesViewModel();
             viewModel.EmployeesModel = null;
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();

             return viewModel;
         }

         /// <summary>
         /// GET: /Employees/ListGroupedByReportsTo
         /// </summary>
         public ActionResult ListGroupedByReportsTo()
         {
             return View();
         }

         private Models.EmployeesModel GetModelByPrimaryKey(int employeeID)
         {
             Models.EmployeesModel model = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/EmployeesApi/SelectByPrimaryKey/" + employeeID).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     Employees objEmployees = JsonConvert.DeserializeObject<Employees>(responseBody);

                     // assign values to the model
                     model = new Models.EmployeesModel();
                     model.EmployeeID = objEmployees.EmployeeID;
                     model.LastName = objEmployees.LastName;
                     model.FirstName = objEmployees.FirstName;
                     model.Title = objEmployees.Title;
                     model.TitleOfCourtesy = objEmployees.TitleOfCourtesy;
                     model.BirthDate = objEmployees.BirthDate;
                     model.HireDate = objEmployees.HireDate;
                     model.Address = objEmployees.Address;
                     model.City = objEmployees.City;
                     model.Region1 = objEmployees.Region1;
                     model.PostalCode = objEmployees.PostalCode;
                     model.Country = objEmployees.Country;
                     model.HomePhone = objEmployees.HomePhone;
                     model.Extension = objEmployees.Extension;
                     model.Notes = objEmployees.Notes;
                     model.ReportsTo = objEmployees.ReportsTo;
                     model.PhotoPath = objEmployees.PhotoPath;
                 }
             }

             return model;
         }

         private EmployeesCollection GetEmployeesDropDownListData()
         {
             EmployeesCollection objEmployeesCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/EmployeesApi/SelectEmployeesDropDownListData/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objEmployeesCol = JsonConvert.DeserializeObject<EmployeesCollection>(responseBody);
                 }
             }

             return objEmployeesCol;
         }
     } 
} 
