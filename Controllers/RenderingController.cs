using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Web.Models;
using static Web.Helpers.ApplicationHelper;

namespace Web.Controllers
{
	public class RenderingController : BaseController
	{
		[HttpGet]
		public IActionResult Add(int Id)
		{

			var data = dbContext.DynamicForms.FirstOrDefault(x => x.Id == Id && x.Status == EnumStatus.Enable && x.IsDeleted == false);
			var formData = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented,
							new JsonSerializerSettings
							{
								ReferenceLoopHandling = ReferenceLoopHandling.Ignore
							});
			return View("Form", Newtonsoft.Json.JsonConvert.DeserializeObject<DynamicForm>(formData));
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
                DynamicFormMaster dynamicFormMaster = new DynamicFormMaster()
                {
                    DynamicFormId = Convert.ToInt32(Request.Form["FormId"]),
                    CreatedBy = 1,
                    CreatedDateTime = GetDateTime(),
                    Status = EnumStatus.Enable,
                    IsDeleted = false,
                };
                dbContext.DynamicFormMasters.Add(dynamicFormMaster);
                dbContext.SaveChanges();

                var Keys = Request.Form.Keys.ToList();
                Keys = Keys.Where(x => x.StartsWith("d_")).ToList();
                foreach (var key in Keys)
                {
                    DynamicFormMasterDetail dynamicFormMasterDetail = new DynamicFormMasterDetail();
                    dynamicFormMasterDetail.DynamicFormMasterId = dynamicFormMaster.Id;
                    dynamicFormMasterDetail.DynamicFormInputId = Convert.ToInt32(key.Replace("d_", ""));
                    dynamicFormMasterDetail.DynamicFormInputValue = Request.Form[key];
                    dbContext.DynamicFormMasterDetails.Add(dynamicFormMasterDetail);

                }
                dbContext.SaveChanges();
                ajaxResponse.Success = true;
                ajaxResponse.Type = EnumJQueryResponseType.MessageAndReloadWithDelay;
                ajaxResponse.Message = "Record submitted successfully";
            }
			catch (Exception ex)
			{
                ajaxResponse.Message = $"{ex.Message}";

            }
           
           
            return Json(ajaxResponse);
		}


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

	}
	
}
