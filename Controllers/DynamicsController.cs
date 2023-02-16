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
        public DynamicForm GetRecord(int? id)
        {
            return dbContext.DynamicForms.FirstOrDefault(o => o.Id == id && o.IsDeleted == false);
        }
        public IActionResult Add()
        {
            return View("Form", new DynamicForm());
        }
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
        [HttpPost]
        public JsonResult Save(DynamicFormMeta modelRecord)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            ajaxResponse.Success = false;
            ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            ajaxResponse.Message = "Post Data Not Found";
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    bool isAbleToAddOrUpdate = true;
                    var Record = dbContext.DynamicForms.FirstOrDefault(o => o.Id != modelRecord.Id && o.Title.ToLower()
                    .Equals(modelRecord.Title.ToLower()) && o.IsDeleted == false);
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
                            var dynamicFormModel = new DynamicForm()
                            {
                                Title = modelRecord.Title,
                                UtccreatedDateTime = GetUtcDateTime(),
                                CreatedDateTime = GetDateTime(),
                                CreatedBy = 1,
                                Status = EnumStatus.Enable,
                                IsDeleted = false,

                            };
                            isRecordWillAdded = true;
                            dbContext.DynamicForms.Add(dynamicFormModel);
                            dbContext.SaveChanges();

                            foreach (var input in modelRecord.Inputs)
                            {

                                var inputModel = new DynamicFormInput()
                                {
                                    DynamicFormId = dynamicFormModel.Id,
                                    Name = input.Name,
                                    SequenceOrder = input.SequenceOrder,
                                    Type = input.Type,
                                    IsRequired = input.IsRequired,
                                    CreatedBy = 1,
                                    CreatedDateTime = GetDateTime(),
                                    UtccreatedDateTime = GetUtcDateTime(),
                                    Status = EnumStatus.Enable,
                                    IsDeleted = false,
                                };
                                dbContext.DynamicFormInputs.Add(inputModel);
                                dbContext.SaveChanges();
                                foreach (var attribute in input.Attributes)
                                {
                                    var attributeModel = new DynamicFormInputAttribute()
                                    {
                                        DynamicFormInputId = inputModel.Id,
                                        AttrKey = attribute.AttrKey,
                                        AttrValue = attribute.AttrValue,

                                    };
                                    dbContext.DynamicFormInputAttributes.Add(attributeModel);
                                    
                                }
                                if (input.DropdownOpt.Count > 0)
                                {
                                    foreach (var item in input.DropdownOpt)
                                    {
                                        var dropdownOptModel = new DynamicFormInputDataSource()
                                        {
                                            DynamicFormInputId = inputModel.Id,
                                            Key = item.Key,
                                            Value = item.Value,
                                        };
                                        dbContext.DynamicFormInputDataSources.Add(dropdownOptModel);
                                       
                                    }
                                }

                            }

                            dbContext.SaveChanges();

                        }
                        else
                        {
                            //modelRecord.UpdatedDateTime = GetDateTime(dbContext);
                            //modelRecord.UtcupdatedDateTime = GetUtcDateTime();
                            //modelRecord.UpdatedBy = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); ;
                            //dbContext.DynamicForms.Update(modelRecord);
                        }
                        //dbContext.SaveChanges();
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
        public JsonResult GetStates(int? Id)
        {
            return Json(dbContext.States.Where(x => x.CountryId == Id && x.Status.Equals(EnumStatus.Enable) && !x.IsDeleted).Select(x => new { x.Id, x.Title }).ToList());
        }

        [HttpPost]
        public IActionResult Listener()
		
        {
			try
			{
				//string RoleName = User.FindFirstValue("RoleName").ToLower();
				var draw = Request.Form["draw"].FirstOrDefault();
				var start = Request.Form["start"].FirstOrDefault();
				var length = Request.Form["length"].FirstOrDefault();
				var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
				var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
				var searchValue = Request.Form["search[value]"].FirstOrDefault();
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;

				

				var DynamicFormMasterData = dbContext.DynamicForms.Where(x => x.Status == EnumStatus.Enable && !x.IsDeleted)
					.OrderByDescending(x => x.CreatedDateTime).AsQueryable();
				//if (!string.IsNullOrEmpty(searchValue))
				//{
				//	DynamicFormMasterData = DynamicFormMasterData.Where(m =>
				//								   m.DynamicFormMasterDetails.Where(x => x.DynamicFormInputValue.Contains(searchValue)).Count() > 0
				//								|| m.Status.Contains(searchValue)
				//								|| m.CreatedDateTime.ToString().Contains(searchValue)
				//								|| m.UpdatedDateTime.ToString().Contains(searchValue));
				//}
				var DynamicDataList = DynamicFormMasterData.Select(x => new
                {
                    Id = x.Id,
                    FormName = x.Title,
                    NumberOfInputs = x.DynamicFormInputs.Count,
                }).ToList();

				var jsonData = new { draw = draw, recordsFiltered = DynamicDataList.Count, recordsTotal = DynamicDataList.Count, data = DynamicDataList.Skip(skip).Take(pageSize) };
				return Ok(jsonData);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
