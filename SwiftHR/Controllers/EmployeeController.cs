using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwiftHR.Models;
using SwiftHR.Utility;

namespace SwiftHR.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public EmployeeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }

        // GET: EmployeeController
        public ActionResult AddEmployee()
        {
            EmpMasters empMasters = new EmpMasters();
            empMasters  = GetEmpMasterDetails();
           
            if (IsAllowPageAccess("AddEmployee"))
                return View("AddEmployee", empMasters);
            else
                return RedirectToAction("AccessDenied", "Home");
        }

        public ActionResult UpdateEmployee()
        {
            EmpMasters empMasters = new EmpMasters();
            empMasters = GetEmpMasterDetails();

            if (IsAllowPageAccess("AddEmployee"))
                return View("EditEmployeeDetails", empMasters);
            else
                return RedirectToAction("AccessDenied", "Home");
        }

        private EmpMasters GetEmpMasterDetails()
        {
            EmpMasters empMasters = new EmpMasters();

            List<MasterDataItem> empMasterData = new List<MasterDataItem>();
            empMasterData = _context.MasterDataItems.Where(x => x.ItemTypeId >= 18 && x.ItemTypeId <= 28).ToList();

            List<UserDetail> reportingMgrList = new List<UserDetail>();
            reportingMgrList = _context.UserDetails.Where(e => e.RoleId == Convert.ToInt32("6")).ToList();

            empMasters.empMasterDataItems = empMasterData;
            empMasters.reportingMgrList = reportingMgrList;

            return empMasters;
        }

        public ActionResult EmployeeList()
        {
            if (IsAllowPageAccess("AddEmployee"))
            {
                List<Employee> empData = new List<Employee>();
                empData = _context.Employees.ToList();

                return View("EmployeeList", empData);
            }
            else
                return RedirectToAction("AccessDenied", "Home");
        }

        public ActionResult EmployeeDetails(string empId)
        {
            Employee empData = new Employee();
            empData = _context.Employees.Where(o => o.EmployeeId == Convert.ToInt32(empId)).SingleOrDefault();
            return PartialView("EmployeeDetails", empData);
        }

        public ActionResult EditEmployeeDetails(string empId)
        {
            return RedirectToAction("EmployeeList", "Employee");
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployeeDetails(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsAllowPageAccess("AddEmployee"))
                    {
                        int id = 0;
                        string CompanyId = "2";

                        Employee emp = new Employee();
                        emp.EmployeeId = 0;

                        if (!string.IsNullOrEmpty(CompanyId))
                            emp.CompanyId = Convert.ToInt32(CompanyId);


                        if (!string.IsNullOrEmpty(collection["EmployeeNumber"].ToString()))
                        {
                            emp.EmployeeNumber = Convert.ToInt32(collection["EmployeeNumber"]);
                            emp.FirstName = collection["FirstName"];
                            emp.MiddleName = collection["MiddleName"];
                            emp.LastName = collection["LastName"];
                            emp.ContactNumber = collection["ContactNumber"];
                            emp.Email = collection["Email"];

                            if (!string.IsNullOrEmpty(collection["ReportingManager"]) && collection["ReportingManager"] != "0")
                                emp.ReportingManager = collection["ReportingManager"];

                            emp.DateOfJoining = collection["DateOfJoining"];
                            emp.ConfirmationDate = collection["ConfirmationDate"];

                            if (!string.IsNullOrEmpty(collection["EmployeeStatus"]) && collection["EmployeeStatus"] != "0")
                                emp.EmployeeStatus = collection["EmployeeStatus"];

                            emp.ProbationPeriod = collection["ProbationPeriod"];

                            if (!string.IsNullOrEmpty(collection["Department"]) && collection["Department"] != "0")
                                emp.Department = collection["Department"];

                            if (!string.IsNullOrEmpty(collection["Designation"]) && collection["Designation"] != "0")
                                emp.Designation = collection["Designation"];

                            if (!string.IsNullOrEmpty(collection["Grade"]) && collection["Grade"] != "0")
                                emp.Grade = collection["Grade"];

                            if (!string.IsNullOrEmpty(collection["FunctionalGrade"]) && collection["FunctionalGrade"] != "0")
                                emp.FunctionalGrade = collection["FunctionalGrade"];

                            if (!string.IsNullOrEmpty(collection["Level"]) && collection["Level"] != "0")
                                emp.Level = collection["Level"];

                            if (!string.IsNullOrEmpty(collection["SubLevel"]) && collection["SubLevel"] != "0")
                                emp.SubLevel = collection["SubLevel"];

                            if (!string.IsNullOrEmpty(collection["CostCenter"]) && collection["CostCenter"] != "0")
                                emp.CostCenter = collection["CostCenter"];

                            if (!string.IsNullOrEmpty(collection["Location"]) && collection["Location"] != "0")
                                emp.Location = collection["Location"];

                            emp.EmployeeProfilePhoto = "default-avatar.png";
                            emp.Pfnumber = collection["Pfnumber"];
                            emp.Uannumber = collection["Uannumber"];
                            emp.IncludeEsi = Convert.ToBoolean(collection["IncludeEsi"]);
                            emp.IncludeLwf = Convert.ToBoolean(collection["IncludeLwf"]);

                            if (!string.IsNullOrEmpty(collection["PaymentMethod"]) && collection["PaymentMethod"] != "0")
                                emp.PaymentMethod = collection["PaymentMethod"];

                            if (!string.IsNullOrEmpty(collection["IsSelfOnboarding"]))
                                emp.IsSelfOnboarding = Convert.ToBoolean(collection["IsSelfOnboarding"]);
                            else
                                emp.IsSelfOnboarding = false;

                            emp.IsActive = true;
                            emp.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                            emp.CreatedBy = 1;

                            using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                            {
                                entities.Employees.Add(emp);
                                entities.SaveChanges();
                                id = emp.EmployeeId;
                            }


                            if (id > 0)
                            {
                                UserDetail user = new UserDetail();

                                if (!string.IsNullOrEmpty(emp.EmployeeNumber.ToString()))
                                {
                                    user.UserName = emp.EmployeeNumber.ToString();
                                    user.UserPassword = emp.EmployeeNumber.ToString();

                                    if (!string.IsNullOrEmpty(emp.EmployeeId.ToString()))
                                        user.EmployeeId = emp.EmployeeId;

                                    user.RoleId = 4;

                                    user.IsPwdChangeFt = false;

                                    if (!string.IsNullOrEmpty(emp.FirstName))
                                        user.FirstName = emp.FirstName;
                                    if (!string.IsNullOrEmpty(emp.LastName))
                                        user.LastName = emp.LastName;
                                    if (!string.IsNullOrEmpty(emp.Email))
                                        user.Email = emp.Email;
                                    if (!string.IsNullOrEmpty(emp.ContactNumber))
                                        user.Contact = emp.ContactNumber;

                                    user.ProfilePicturePath = "default-avatar.png";

                                    using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                                    {
                                        entities.UserDetails.Add(user);
                                        entities.SaveChanges();
                                    }

                                    ViewBag.Message = string.Format("Successfully Added Employee {0}.\\n Date: {1}", emp.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

                                    if (Convert.ToBoolean(emp.IsSelfOnboarding))
                                    {
                                        MailMessage m = new MailMessage();
                                        SmtpClient sc = new SmtpClient();

                                        string baseUrl = _configuration["AppData:BaseUrlLocal"];

                                        string callUrl = baseUrl + "Employee/EmpSetPassword?eid=" + DataSecurity.Encode(emp.EmployeeId.ToString());

                                        string htmlText = _context.EmailTemplates.Where(x => x.EmailTemplateTitle == "EmployeeOnboardingTemplate").Select(x => x.EmailTemplateHtml).SingleOrDefault();

                                        htmlText = htmlText.Replace("#FullName", emp.FirstName + " " + emp.MiddleName + " " + emp.LastName);

                                        htmlText = htmlText.Replace("#CallUrl", callUrl);

                                        htmlText = htmlText.Replace("#EmployeeNumber", emp.EmployeeNumber.ToString());

                                        string ToName = string.Empty;

                                        if (!string.IsNullOrEmpty(emp.MiddleName)) ToName = emp.FirstName + "" + emp.MiddleName + "" + emp.LastName;
                                        else ToName = emp.FirstName + "" + emp.LastName;

                                        m.From = new MailAddress(_configuration["AppData:EmailAccessName"], "Human Resource");
                                        m.To.Add(new MailAddress(emp.Email, ToName));

                                        m.Subject = "Employee Self-Onboarding";
                                        m.IsBodyHtml = true;
                                        m.Body = htmlText;

                                        sc.Host = "smtpout.asia.secureserver.net";
                                        sc.Port = 3535;
                                        sc.Credentials = new
                                        System.Net.NetworkCredential(_configuration["AppData:EmailAccessName"], _configuration["AppData:EmailAccessPwd"]);
                                        sc.EnableSsl = true;
                                        sc.Send(m);
                                    }

                                }


                            }
                        }

                    }

                }

                EmpMasters empMasters = new EmpMasters();
                empMasters = GetEmpMasterDetails();

                return View("AddEmployee", empMasters);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("AddEmployee", "Employee");
            }
        }

        [HttpGet]
        public ActionResult EmployeeNumberExists(string empNumber)
        {
            bool IsExists = _context.Employees.Any(e => e.EmployeeNumber == Convert.ToInt32(empNumber));

            string result = IsExists.ToString();

            return new JsonResult(result);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        #region Common

        [HttpGet]
        public IActionResult GetUserDetails()
        {
            int userId = GetLoggedInUserId();

            EmployeeUserDetails employeeUserDetails = new EmployeeUserDetails();

            UserDetail userData = _context.UserDetails.Where(i => i.UserId == userId).SingleOrDefault();

            int empId = 0;

            if (userData.UserId > 0)
            {
                if (string.IsNullOrEmpty(userData.ProfilePicturePath))
                    userData.ProfilePicturePath = "default-avatar.png";

                userData.UserPassword = string.Empty;

                employeeUserDetails.userDetails = userData;

                if (userData.EmployeeId > 0)
                {
                    empId = Convert.ToInt32(userData.EmployeeId);

                    Employee employee = _context.Employees.Where(i => i.EmployeeId == empId).SingleOrDefault();

                    if (employee.EmployeeId > 0)
                        employeeUserDetails.empDetails = employee;
                }
            }

            string result = JsonConvert.SerializeObject(employeeUserDetails);

            return new JsonResult(result);
        }

        private Employee GetEmployeeDetailsByEmpNumber(string userName)
        {
            Employee empData = _context.Employees.Where(i => i.EmployeeNumber == Convert.ToInt32(userName)).Single();
            return empData;
        }

        private UserDetail GetUserByUserName(string userName)
        {
            UserDetail userData = _context.UserDetails.Where(i => i.UserName == userName).Single();
            return userData;
        }

        protected void SetCookies(string key, string value)
        {
            int minutes = 30;

            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }

        public string GetCookies(string key)
        {
            string cookieValue = string.Empty;
            cookieValue = Request.Cookies[key];
            return cookieValue;
        }

        public int GetLoggedInEmpId()
        {
            int employeeId = 0;
            string eid = GetCookies("eid");
            string empId = DataSecurity.DecryptString(eid);

            if (!string.IsNullOrEmpty(empId))
                employeeId = Convert.ToInt32(empId);

            return employeeId;
        }

        public int GetLoggedInUserId()
        {
            int userId = 0;
            string uid = GetCookies("uid");

            if (!string.IsNullOrEmpty(uid))
            {
                string usrId = DataSecurity.DecryptString(uid);

                if (!string.IsNullOrEmpty(usrId))
                    userId = Convert.ToInt32(usrId);
            }

            return userId;
        }

        public int GetLoggedInUserRoleId()
        {
            int roleId = 0;
            string rid = GetCookies("rid");

            if (!string.IsNullOrEmpty(rid))
            {
                string rlId = DataSecurity.DecryptString(rid);

                if (!string.IsNullOrEmpty(rlId))
                    roleId = Convert.ToInt32(rlId);
            }

            return roleId;
        }

        private bool IsAllowPageAccess(string pageName)
        {
            bool IsAllowAccess = false;
            int roleId = GetLoggedInUserRoleId();

            if (roleId > 0)
                IsAllowAccess = IsPageAccessAllowed(roleId, pageName);

            return IsAllowAccess;
        }

        public bool IsPageAccessAllowed(int roleId, string pageName)
        {
            bool IsAllowedAccess = false;

            IsAllowedAccess = Convert.ToBoolean((from a in _context.PageAccessSetups
                                                 join c in _context.PageModules on a.PageModuleId equals c.PageModuleId
                                                 where a.RoleId == roleId & c.PageModuleName == pageName
                                                 select a.IsAllow).SingleOrDefault());

            return IsAllowedAccess;
        }
        #endregion
    }
}
