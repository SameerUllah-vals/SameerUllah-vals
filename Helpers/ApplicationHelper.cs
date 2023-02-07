using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Helpers
{
    public static class ApplicationHelper
    {
        #region "Website Value Helper"
        public const string Cookie_Admin_Area_Authentication = "Cookie_Admin_Area_Authentication";
        public const string Cookie_Admin_Authentication = "Cookie_Admin_Authentication";
        public const string Cookie_Admin_Authentication_Scheme = "Cookie_Admin_Authentication_Scheme";
        public const string Cookie_Admin_Area_Authentication_Scheme = "Cookie_Admin_Area_Authentication_Scheme";
        public const string Cookie_Admin_Area_Email_Address = "Cookie_Admin_Area_Email_Address";
        public const string Cookie_Admin_Area_Password = "Cookie_Admin_Area_Password";
        public const string Session_Admin_Area_User_Login = "Session_Admin_Area_User_Login";
        public const string Cookie_Member_Login = "Cookie_Member_Login";
        public const string Session_Member_Login = "Session_Member_Login";
        public const string jQuery_Date_Format = "DD/MM/YYYY";
        public const string jQuery_Date_Time_Format = "DD/MM/YYYY HH:mm:ss";
        public const string Website_Date_Format = "dd/MM/yyyy";
        public const string Website_Date_Format_With_Month_Letter = "dd/MMM/yyyy";
        public const string Website_Date_Format_With_Month_Only_Letter = "MMMM, yyyy";
        public const string Website_Date_Time_Format = "dd/MM/yyyy HH:mm:ss";
        public const string EncryptKey = "Jobport1";
        public static string[] allowedImageExtensions = { ".jpg", ".png", ".gif", ".jpeg" };
        public static string[] allowedExcelExtensions = { ".xls", ".xlsx" };
        public static string[] allowedCVExtensions = { ".pdf", ".doc", ".docx" };
        public static string[] DayNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public static Dictionary<int, string> DayDictionary = new Dictionary<int, string>() { { 1, "Monday" }, { 2, "Tuesday" }, { 3, "Wednesday" }, { 4, "Thursday" }, { 5, "Friday" }, { 6, "Saturday" }, { 7, "Sunday" } };
        public static string[] JQueryDayNamesIndex = { "1", "2", "3", "4", "5", "6", "0" };
        public static string[] MonthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        #endregion
        #region "Setting Keys"
        public const string Website_Name_Key = "Website Name";
        public const string Website_URL_Key = "Website URL";
        public const string Website_DateTime_Zone_key = "Website DateTime Zone";
        public const string Website_Email_Sender_Key = "Website Email Sender";
        public const string Website_Email_Server_Detail_Key = "Website Email Server Detail";
        public const string Website_Email_Encrypted_Password = "Website Email Encrypted Password";


        #endregion
        #region "Enum Helper"


        public static class EnumClaimTypes
        {
            public const string SubscriptionId = "SubscriptionId";
            public const string Token = "Token";
            public const string LoginType = "LoginType";
            public const string ProfileImage = "ProfileImage";
            public const string VerificationDateTime = "VerificationDateTime";
        }

        public static class EnumFileUploadFolder
        {
            public const string ProfileImage = "ProfileImage";
            public const string Documents = "uploads\\Documents";
        }
        public static class EnumPlatform
        {
            public const string SMS = "SMS";
            public const string Email = "Email";
            public const string Push_Notification = "Notification";
        }
        public static class EnumMenuType
        {
            public const string Parent = "P";
            public const string Children = "C";
        }

        public static class EnumSMSStatusCode
        {
            public const string OK = "300";
        }
        public static class EnumNotificationAction
        {
            public const string SHORTLISTED = "SHORTLISTED";
            public const string REVIEWED = "REVIEWED";
            public const string APPROVED = "APPROVED";
            public const string REJECTED = "REJECTED";
        }

        public static class EnumBackendMenuType
        {
            public const string Admin = "Admin";
            public const string Member = "Member";
            public const string Both = "Both";
        }


        public static class EnumBackendMenuDetailType
        {
            public const string None = "none";
            public const string All = "all";
            public const string Add = "add";
            public const string Edit = "edit";
            public const string View = "view";
            public const string Delete = "delete";
        }
        public static class EnumYesNo
        {
            public const string No = "No";
            public const string Yes = "Yes";
        }

        public static class EnumRegistrationFrom
        {
            public const string Mobile = "Mobile";
            public const string Desktop = "Desktop";
        }

        public static class EnumAccountType
        {
            public const string SuperAdmin = "SuperAdmin";
        }

        public static class EnumStatus
        {
            public const string Enable = "Enable";
            public const string Disable = "Disable";
            public const string Pending = "Pending";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
            public const string Closed = "Closed";
            public const string Expired = "Expired";
            public const string Sent = "Sent";
            public const string Unsent = "Unsent";
            public const string Read = "Read";
            public const string Unread = "Unread";
        }
        public static class EnumWebsiteStatus
        {
            public const string Online = "Online";
            public const string Offline = "Offline";
        }
        public static class EnumCalendarUnits
        {
            public const string Day = "Day";
            public const string Month = "Month";
            public const string Year = "Year";
        }
        public static class EnumJQueryResponseType
        {
            public const string DataOnly = "D";
            public const string MessageOnly = "M";
            public const string RedirectOnly = "T";
            public const string RefreshOnly = "R";
            public const string ReloadOnly = "RL";
            public const string MessageAndRedirect = "M-T";
            public const string MessageAndRedirectWithDelay = "M-TD";
            public const string MessageAndRefresh = "M-R";
            public const string MessageRefreshRedirect = "M-R-T";
            public const string MessageRefreshRedirectWithDelay = "M-R-TD";
            public const string RefreshAndRedirect = "R-T";
            public const string RefreshAndRedirectWithDelay = "R-TD";
            public const string RedirectWithDelay = "TD";
            public const string MessageAndReloadWithDelay = "M-RLD";
        }
        public static class EnumPageType
        {
            public const string Index = "Index";
            public const string Add = "Add";
            public const string Edit = "Edit";
            public const string View = "View";
            public const string Sorting = "Sorting";
        }

        public static class EnumLoginWith
        {
            public const string LinkedIn = "LinkedIn";
            public const string Web = "Web";
            public const string Facebook = "Facebook";
            public const string Google = "Google";
        }
        public static class EnumRegisterWith
        {
            public const string LinkedIn = "LinkedIn";
            public const string Web = "Web";
            public const string Rider = "Rider";
            public const string Facebook = "Facebook";
            public const string Google = "Google";
            public const string Employer = "Employer";
            public const string SuperAdmin = "Employer";
            public const string Fulcrum = "Fulcrum";
        }
        #endregion
        #region Core Functions
        public static string GetSettingContentByName(ValsTechnologiesContext dbContext, string _Title)
        {
            string ReturnContent = "#";
            var SettingRecord = dbContext.Settings.FirstOrDefault(o => o.Title.Equals(_Title));
            if (SettingRecord != null)
            {
                ReturnContent = SettingRecord.Content;
            }
            return ReturnContent;
        }
        public static DateTime GetDateTime(ValsTechnologiesContext Database = null, string timeZoneId = "", bool isDefault = true)
        {
            if (isDefault)
            {
                timeZoneId = "Pakistan Standard Time";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(timeZoneId))
                {
                    timeZoneId = GetSettingContentByName(Database, Website_DateTime_Zone_key);
                }
            }

            TimeZoneInfo time_zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, time_zone);
        }
        public static DateTime GetUtcDateTime()
        {
            return DateTime.UtcNow;
        }

        public static bool IsAdminLogin(Controller _controller)
        {
            return _controller.User.Identity.IsAuthenticated;
        }
        #endregion
        public class AjaxResponse
        {
            public bool Success { get; set; }
            public string Type { get; set; }
            public string FieldName { get; set; }
            public string Message { get; set; }
            public string TargetURL { get; set; }
            public object Data { get; set; }
        }

        public static string Serialize<T>(this List<T> List) where T : class
        {
            return JsonConvert.SerializeObject(List, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
        }


        public static string Serialize<T>(this T Object) where T : class
        {
            return JsonConvert.SerializeObject(Object, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
        }

        public static List<T> ToJSON<T>(this List<T> genList) where T : class
        {
            var serializedData = JsonConvert.SerializeObject(genList, Formatting.Indented,
                             new JsonSerializerSettings
                             {
                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                             });
            return JsonConvert.DeserializeObject<List<T>>(serializedData);
        }

        public static T ToJSON<T>(this T Record) where T : class
        {
            var serializedData = JsonConvert.SerializeObject(Record, Formatting.Indented,
                             new JsonSerializerSettings
                             {
                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                             });
            return JsonConvert.DeserializeObject<T>(serializedData);
        }
    }


}
