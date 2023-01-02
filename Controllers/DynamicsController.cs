using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Models;
using static Web.Helpers.ApplicationHelper;

namespace Web.Controllers
{
    public class DynamicsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Listener")]
        public IActionResult Listener()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var Data = (from x in dbContext.DynamicForms.Where(x => !x.IsDeleted) select x);
                if (!string.IsNullOrEmpty(sortColumn) || string.IsNullOrEmpty(sortColumnDirection))
                {
                    //Data = Data.Where(x => !x.IsDeleted).OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    Data = Data.Where(m => m.Title.Contains(searchValue)
                                                || m.Title.Contains(searchValue)
                                                || m.Status.Contains(searchValue)
                                                || m.CreatedDateTime.ToString().Contains(searchValue));
                }
                recordsTotal = Data.Count();
                var resultList = Data.Skip(skip).Take(pageSize).ToList();
                var resultData = from x in resultList.Where(x => !x.IsDeleted)
                                 select new
                                 {
                                     x.Id,
                                     x.Title,
                                     x.Status,
                                     CreatedDateTime = x.CreatedDateTime,
                                     CreatedDateTimeString = x.CreatedDateTime.ToString(Website_Date_Time_Format),
                                     UpdatedDateTime = !x.UpdatedDateTime.HasValue ? "" : x.UpdatedDateTime.Value.ToString(Website_Date_Time_Format)
                                 };

                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = resultData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("GetRecord")]
        public DynamicForm GetRecord(int? id)
        {
            return dbContext.DynamicForms.FirstOrDefault(o => o.Id == id && o.IsDeleted == false);                        
        }
        [Route("Add")]
        public IActionResult Add()
        {
            return View("Form", new DynamicForm());
        }
        [Route("View")]
        public IActionResult View(int? id)
        {
            var Record = GetRecord(id);
            if (Record != null)
            {
                ViewBag.PageType = EnumPageType.View;
                return View("Form", Record);
            }
            else
            {
                return Redirect(ViewBag.WebsiteURL + "dynamics/add");
            }
        }
        [Route("Edit")]
        public IActionResult Edit(int? id)
        {
            var Record = GetRecord(id);
            if (Record != null)
            {

                ViewBag.PageType = EnumPageType.Edit;
                return View("Form", Record);
            }
            else
            {
                return Redirect(ViewBag.WebsiteURL + "dynamics/add");
            }
        }
        [Route("Save")]
        public JsonResult Save(DynamicForm modelRecord)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            ajaxResponse.Success = false;
            ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            ajaxResponse.Message = "Post Data Not Found";
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    bool isAbleToAddOrUpdate = true;
                    var Record = dbContext.DynamicForms.FirstOrDefault(o => o.Id != modelRecord.Id && o.Title.ToLower().Equals(modelRecord.Title.ToLower()) && o.IsDeleted == false);
                    if (Record != null)
                    {
                        ajaxResponse.Message = "Title already exist in our records";
                        isAbleToAddOrUpdate = false;
                    }
                    if (isAbleToAddOrUpdate)
                    {
                        bool isRecordWillAdded = false;
                        if (modelRecord.Id == 0)
                        {
                            isRecordWillAdded = true;
                            modelRecord.CreatedDateTime = GetDateTime(dbContext);
                            modelRecord.UtccreatedDateTime = GetUtcDateTime();
                            modelRecord.CreatedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                            dbContext.DynamicForms.Add(modelRecord);
                        }
                        else
                        {

                            modelRecord.UpdatedDateTime = GetDateTime(dbContext);
                            modelRecord.UtcupdatedDateTime = GetUtcDateTime();
                            modelRecord.UpdatedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); ;
                            dbContext.DynamicForms.Update(modelRecord);
                        }
                        dbContext.SaveChanges();
                        if (isRecordWillAdded)
                        {
                            ajaxResponse.Message = "Form Added Successfully";
                        }
                        else
                        {
                            ajaxResponse.Message = "Form Updated Successfully";
                        }
                        ajaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                        ajaxResponse.TargetURL = ViewBag.WebsiteURL + "dynamics";
                        ajaxResponse.Success = true;
                    }
                }
                else
                {
                    ajaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                    ajaxResponse.Message = "Session Expired";
                    ajaxResponse.TargetURL = ViewBag.WebsiteURL;
                }
            }
            catch (Exception ex)
            {
                string _catchMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    _catchMessage += "<br/>" + ex.InnerException.Message;
                }
                ajaxResponse.Message = _catchMessage;
            }
            return Json(ajaxResponse);
        }
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int? _value)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            ajaxResponse.Success = false;
            ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            ajaxResponse.Message = "Data not found in our records";
            try
            {
                if (IsAdminLogin(this))
                {
                    if (_value == 0 || _value == null)
                    {
                        ajaxResponse.Message = "Id is not in correct format";
                    }
                    else
                    {
                        var RecordToDelete = dbContext.Cities.FirstOrDefault(o => o.Id == _value);
                        if (RecordToDelete != null)
                        {
                            RecordToDelete.IsDeleted = true;
                            RecordToDelete.DeletedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                            RecordToDelete.UtcdeletedDateTime = GetUtcDateTime();
                            RecordToDelete.DeletedDateTime = GetDateTime(dbContext);
                            dbContext.Cities.Update(RecordToDelete);
                            dbContext.SaveChanges();
                            ajaxResponse.Success = true;
                            ajaxResponse.Message = "Record Deleted Successfully";
                        }
                    }
                }
                else
                {
                    ajaxResponse.Type = EnumJQueryResponseType.RedirectWithDelay;
                    ajaxResponse.Message = "Session Expired";
                    ajaxResponse.TargetURL = ViewBag.WebsiteURL;
                }
            }
            catch (Exception ex)
            {
                string _catchMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    _catchMessage += "<br/>" + ex.InnerException.Message;
                }
                ajaxResponse.Message = _catchMessage;
            }
            return Json(ajaxResponse);
        }
        [HttpGet]
        [Route("GetStates")]
        public JsonResult GetStates(int? Id)
        {
            return Json(dbContext.States.Where(x => x.CountryId == Id && x.Status.Equals(EnumStatus.Enable) && !x.IsDeleted).Select(x => new { x.Id, x.Title }).ToList());
        }
    }
}
