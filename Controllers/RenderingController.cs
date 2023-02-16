using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Web.Helpers.ApplicationHelper;

namespace Web.Controllers
{
	public class RenderingController : BaseController
	{

        [HttpGet("[controller]/Add")]
		public IActionResult Add(int Id)
		{
			return View("Form", GetRecord(Id, 0));
		}


        public DynamicFormMasterMeta GetRecord(int FormId, int RowId)
        {
            DynamicFormMasterMeta Record = new DynamicFormMasterMeta();
            Record.DynamicForm = dbContext.DynamicForms.FirstOrDefault(x => x.Id == FormId && x.Status == EnumStatus.Enable && x.IsDeleted == false).ToJSON();
            Record.DynamicFormMaster = dbContext.DynamicFormMasters.FirstOrDefault(x => x.Id == RowId).ToJSON();
            
            return Record;
        }

        [HttpGet("[controller]/View")]
        public IActionResult View(int FormId, int RowId)
        {
            ViewBag.PageType = EnumPageType.View;
            return View("Form", GetRecord(FormId, RowId));
        }

        [HttpGet("[controller]/Edit")]
        public IActionResult Edit(int FormId, int RowId)
        {
            ViewBag.PageType = EnumPageType.Edit;
            return View("Form", GetRecord(FormId, RowId));
        }

        [HttpPost]
		public IActionResult Save()
		{
            AjaxResponse ajaxResponse = new AjaxResponse();
            ajaxResponse.Success = false;
            ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            ajaxResponse.Message = "Post Data Not Found";
			try
			{
                int RowId = string.IsNullOrEmpty(Request.Form["RowId"]) ? 0 : Convert.ToInt32(Request.Form["RowId"]);
                DynamicFormMaster dynamicFormMaster;
                if (RowId > 0)
                {
                    dynamicFormMaster = dbContext.DynamicFormMasters.FirstOrDefault(x => x.Id == RowId);
                    dynamicFormMaster.UpdatedDateTime = GetDateTime();
                    dynamicFormMaster.UpdatedBy = 1;
                    
                }
                else
                {
                    dynamicFormMaster = new DynamicFormMaster()
                    {
                        DynamicFormId = Convert.ToInt32(Request.Form["FormId"]),
                        CreatedBy = 1,
                        CreatedDateTime = GetDateTime(),
                        Status = EnumStatus.Enable,
                        IsDeleted = false,
                    };
                    dbContext.DynamicFormMasters.Add(dynamicFormMaster);
                    dbContext.SaveChanges();
                }

                var Keys = Request.Form.Keys.ToList();
                Keys = Keys.Where(x => x.StartsWith("d_")).ToList();
                foreach (var key in Keys)
                {
                    var duplicateRecord = dbContext.DynamicFormMasterDetails
                        .FirstOrDefault(x => x.DynamicFormMasterId == RowId && x.DynamicFormInputId == Convert.ToInt32(key.Replace("d_", "")));
                    if (duplicateRecord == null)
                    {
                        DynamicFormMasterDetail dynamicFormMasterDetail = new DynamicFormMasterDetail();
                        dynamicFormMasterDetail.DynamicFormMasterId = dynamicFormMaster.Id;
                        dynamicFormMasterDetail.DynamicFormInputId = Convert.ToInt32(key.Replace("d_", ""));
                        dynamicFormMasterDetail.DynamicFormInputValue = Request.Form[key];
                        dbContext.DynamicFormMasterDetails.Add(dynamicFormMasterDetail);
                    }
                    else
                    {
                        duplicateRecord.DynamicFormInputValue = Request.Form[key];
                        dbContext.DynamicFormMasterDetails.Update(duplicateRecord);

                    }

                }
                dbContext.SaveChanges();
                ajaxResponse.Success = true;
                if (Request.Form["button"] == "S")
                {
                    ajaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                    ajaxResponse.TargetURL = ViewBag.WebsiteURL + "rendering/index?Id=" + dynamicFormMaster.DynamicFormId;
                }
                else if (Request.Form["button"] == "S-N")
                    ajaxResponse.Type = EnumJQueryResponseType.MessageAndReloadWithDelay;
                ajaxResponse.Message = "Record submitted successfully";
            }
			catch (Exception ex)
			{
                ajaxResponse.Message = $"{ex.Message}";

            }
           
           
            return Json(ajaxResponse);
		}


        [HttpGet("[controller]/index")]
        public IActionResult Index(int Id)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append(JsonConvert.SerializeObject(new Grid()));
            var DynamicFormRecords = dbContext.DynamicFormInputs.Where(x => x.DynamicFormId == Id).ToList();
            List<string> dynamicColumns = new List<string>();
            DynamicFormRecords.ForEach(x =>
            {
                jsonBuilder.Append(",{ \"data\": \"d_" + x.Id + "\"}");
                dynamicColumns.Add(x.Name);
            });
            ViewBag.DynamicColumns = dynamicColumns;
            string columns = JsonConvert.SerializeObject(jsonBuilder.ToString());
            ViewBag.DataTableColumns = JsonConvert.DeserializeObject(columns);
            ViewBag.DataTableActionColumn = DynamicFormRecords.Count;
            return View(nameof(Index), Id);
        }


        [HttpPost]
      
        public IActionResult Listener(int FormId)
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
              
                List<object> DynamicDataList = new List<object>();
             
                var DynamicFormMasterData = dbContext.DynamicFormMasters.Where(x => x.DynamicFormId == FormId && x.Status == EnumStatus.Enable && !x.IsDeleted)
                    .OrderByDescending(x => x.CreatedDateTime).AsQueryable();
                if (!string.IsNullOrEmpty(searchValue))
                {
					DynamicFormMasterData = DynamicFormMasterData.Where(m =>
												   m.DynamicFormMasterDetails.Where(x => x.DynamicFormInputValue.Contains(searchValue)).Count() > 0
												|| m.Status.Contains(searchValue)
												|| m.CreatedDateTime.ToString().Contains(searchValue)
												|| m.UpdatedDateTime.ToString().Contains(searchValue));
				}
				DynamicFormMasterData.ToList().ForEach(x => 
                {
                    Dictionary<string, string> DynamicColumns = new Dictionary<string, string>();

                    if (x.DynamicFormMasterDetails.Count > 0)
                        DynamicColumns.Add( nameof(x.Id) , x.Id.ToString());

                    x.DynamicFormMasterDetails.ToList().ForEach(m =>
                    {
                        DynamicColumns.Add("d_" + m.DynamicFormInputId, m.DynamicFormInputValue);
                    });

                    if(DynamicColumns.Count > 0)
                    {
                        DynamicDataList.Add(DynamicColumns);
                    }
                                
                });
                
                var jsonData = new { draw = draw, recordsFiltered = DynamicDataList.Count, recordsTotal = DynamicDataList.Count, data = DynamicDataList.Skip(skip).Take(pageSize) };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult Delete(int _value)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            ajaxResponse.Success = false;
            ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            ajaxResponse.Message = "Data not found in our records";
            try
            {                
                if (_value == 0 || _value == null)
                {
                    ajaxResponse.Message = "Id is not in correct format";
                }
                else
                {
                    var RecordToDelete = dbContext.DynamicFormMasters.FirstOrDefault(o => o.Id == _value);
                    if (RecordToDelete != null)
                    {
                        RecordToDelete.IsDeleted = true;
                        dbContext.DynamicFormMasters.Update(RecordToDelete);
                        dbContext.SaveChanges();
                        ajaxResponse.Success = true;
                        ajaxResponse.Message = "Record Deleted Successfully";
                    }
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

    }
	
}
