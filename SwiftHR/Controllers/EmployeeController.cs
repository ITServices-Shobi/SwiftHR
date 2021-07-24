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
using System.IO;
using System.Collections;

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

        public ActionResult EmployeeOnbList()
        {
            if (IsAllowPageAccess("AddEmployee"))
            {
                List<EmployeeOnboardingDetails> arrayEmployeeOnboardingDetails = new List<EmployeeOnboardingDetails>();
                //empOnbData = _context.empl.Where(x => x.IsSelfOnboarding == true).ToList();

                //ArrayList arrayEmployeeOnboardingDetails = new ArrayList();
                List<Employee> empDataList = new List<Employee>();
                empDataList = _context.Employees.Where(x => x.IsSelfOnboarding == true).ToList();
                foreach (Employee emp in empDataList)
                {
                    EmployeeOnboardingDetails localEmpOnboardingDetail = new EmployeeOnboardingDetails(emp.EmployeeId.ToString());
                    if(localEmpOnboardingDetail.empOnboardingDetails.OnboardingStatus<=2)
                        arrayEmployeeOnboardingDetails.Add(localEmpOnboardingDetail);
                }

                return View("EmployeeSelfOnboarding", arrayEmployeeOnboardingDetails);
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

        public ActionResult EmployeeProfileDetails(string empId, string callingView)
        {
            String localEmpId;
            if (!string.IsNullOrEmpty(empId))
                localEmpId = empId;
            else
                localEmpId = GetLoggedInEmpId().ToString();
            

            EmployeeOnboardingDetails empOnboardingDetails = new EmployeeOnboardingDetails(localEmpId);
            empOnboardingDetails = GetEmployeeProfileDetails(localEmpId);

            //Set calling view
            if (!string.IsNullOrEmpty(callingView))
            {
                ViewBag.CallingView = callingView;
                empOnboardingDetails.CallingView = callingView;
            }
            // full path to file in temp location
            //var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot\\UploadImages", empOnboardingDetails.empDetails.EmployeeProfilePhoto);
            //empOnboardingDetails.empDetails.EmployeeProfilePhoto = filePath;
            if (string.IsNullOrEmpty(empOnboardingDetails.empDetails.EmployeeProfilePhoto))
                empOnboardingDetails.empDetails.EmployeeProfilePhoto = "default-avatar.png";
            //Convert to date formats
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.DateOfBirth))
                empOnboardingDetails.empDetails.DateOfBirth = System.Convert.ToDateTime(empOnboardingDetails.empDetails.DateOfBirth).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.DateOfJoining))
                empOnboardingDetails.empDetails.DateOfJoining = System.Convert.ToDateTime(empOnboardingDetails.empDetails.DateOfJoining).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.MarriageDate))
                empOnboardingDetails.empDetails.MarriageDate = System.Convert.ToDateTime(empOnboardingDetails.empDetails.MarriageDate).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.NomineeDob))
                empOnboardingDetails.empDetails.NomineeDob = System.Convert.ToDateTime(empOnboardingDetails.empDetails.NomineeDob).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.PassportExpiryDate))
                empOnboardingDetails.empDetails.PassportExpiryDate = System.Convert.ToDateTime(empOnboardingDetails.empDetails.PassportExpiryDate).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.ConfirmationDate))
                empOnboardingDetails.empDetails.ConfirmationDate = System.Convert.ToDateTime(empOnboardingDetails.empDetails.ConfirmationDate).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.CreatedDate))
                empOnboardingDetails.empDetails.CreatedDate = System.Convert.ToDateTime(empOnboardingDetails.empDetails.CreatedDate).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.DateOfLastWorking))
                empOnboardingDetails.empDetails.DateOfLastWorking = System.Convert.ToDateTime(empOnboardingDetails.empDetails.DateOfLastWorking).ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(empOnboardingDetails.empDetails.DateOfResignation))
                empOnboardingDetails.empDetails.DateOfResignation = System.Convert.ToDateTime(empOnboardingDetails.empDetails.DateOfResignation).ToString("yyyy-MM-dd");


            if (IsAllowPageAccess("AddEmployee"))
            {
            //    View()
            //ViewBag.Layout = "~/Views/Employee/EditEmployeeDetails.cshtml";
            //D:\ShobiProjects\SwiftHR\Main\SwiftHR\SwiftHR\Views\
            //    return View(empOnboardingDetails);
            
                return View("EditEmployeeDetails", empOnboardingDetails);
             }
            else
                return RedirectToAction("AccessDenied", "Home");

        }


        private EmployeeOnboardingDetails GetEmployeeProfileDetails(string empId)
        {
            EmployeeOnboardingDetails empOnboardingDetails = new EmployeeOnboardingDetails(empId);
            return empOnboardingDetails;

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

                            //Adding user details for new employee
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
                                        bool success = SendSelfOnboardingMail(emp,1);
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




        // POST: EmployeeController/Create
        [HttpPost("ApproveEmployeeOnboarding")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveEmployeeOnboarding(string empid)
        {
            if (!string.IsNullOrEmpty(empid))
            {
                EmployeeOnboardingDetails empOnboardingDetails;
                empOnboardingDetails = GetEmployeeProfileDetails(empid);

                //Employee empData = new Employee();
                //empData = _context.Employees.Where(o => o.EmployeeId == Convert.ToInt32(empid)).SingleOrDefault();

                if(empOnboardingDetails.empDetails.IsSelfOnboarding==true)
                {
                    if(empOnboardingDetails.empOnboardingDetails.OnboardingStatus==2)
                    {
                        int updateStatus = empOnboardingDetails.ChangeOnboardingStatus(2);
                        bool success = SendSelfOnboardingMail(empOnboardingDetails.empDetails, 2);
                    }
                }
                ArrayList arrayResponse = new ArrayList();
                //String responseObj = '{"name":"John", "age":30, "city":"New York"}';
                string msg= string.Format("Employee Successfully Onboarded ! {0}.\\n Date: {1}", empid, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                arrayResponse.Add(msg);
                //ViewBag.Message = string.Format("Successfully Updated Employee {0}.\\n Date: {1}", empid, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));

                string result = JsonConvert.SerializeObject(arrayResponse);
                return new JsonResult(result);

                //return EmployeeOnbList();
            }
            return EmployeeOnbList();
        }



            // POST: EmployeeController/Create
            [HttpPost("UpdateEmployeeDetails")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployeeDetails(IFormFile PicturePath, IFormCollection collection)
        {
            try
            {
                string loggedEmployeeId;
                if (!string.IsNullOrEmpty(collection["loggedEmployeeId"].ToString()))
                    loggedEmployeeId = collection["loggedEmployeeId"].ToString();
                else
                    loggedEmployeeId = GetLoggedInEmpId().ToString();
                if (!string.IsNullOrEmpty(loggedEmployeeId))
                {
                    EmployeeOnboardingDetails empDetails;
                    empDetails = GetEmployeeProfileDetails(loggedEmployeeId.ToString());

                    if (ModelState.IsValid)
                    {
                        if (IsAllowPageAccess("AddEmployee"))
                        {
                            Employee emp = new Employee();
                            
                            emp.EmployeeId = 0;
                            emp.EmployeeNumber = Convert.ToInt32(collection["loggedEmployeeId"]);
                            //Upload profile picture
                            string fileUploadName = string.Empty;
                            if (PicturePath != null && !string.IsNullOrEmpty(PicturePath.FileName))
                            {
                                long size = PicturePath.Length;
                                // full path to file in temp location
                                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot\\UploadImages", PicturePath.FileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await PicturePath.CopyToAsync(stream);
                                }

                                fileUploadName = PicturePath.FileName;
                                empDetails.empDetails.EmployeeProfilePhoto = fileUploadName;
                            }

                            empDetails.empDetails.FirstName = collection["FirstName"];
                            empDetails.empDetails.MiddleName = collection["MiddleName"];
                            empDetails.empDetails.LastName = collection["LastName"];
                            empDetails.empDetails.ContactNumber = collection["ContactNumber"];
                            empDetails.empDetails.Email = collection["Email"];


                            if (!string.IsNullOrEmpty(collection["ReportingManager"]) && collection["ReportingManager"] != "0")
                                empDetails.empDetails.ReportingManager = collection["ReportingManager"];
                            empDetails.empDetails.DateOfJoining = collection["DateOfJoining"];
                            empDetails.empDetails.ConfirmationDate = collection["ConfirmationDate"];

                            //empDetails.empDetails.EmployeeProfilePhoto = collection["PicturePath"];

                            //empDetails.empDetails.DateOfBirth = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["DateOfBirth"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                            //If self enboarding is enabled
                            if (Convert.ToBoolean(empDetails.empDetails.IsSelfOnboarding))
                            {
                                empDetails.empOnboardingDetails.OnbemployeeId = Convert.ToInt32(loggedEmployeeId);
                                //empDetails.empOnboardingDetails.OnbemployeeId = GetLoggedInEmpId();
                                if (!string.IsNullOrEmpty(collection["BloodGroup"]))
                                    empDetails.empOnboardingDetails.BloodGroup = collection["BloodGroup"];
                                if (!string.IsNullOrEmpty(collection["DateOfBirth"]))
                                    empDetails.empOnboardingDetails.DateOfBirth = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["DateOfBirth"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                                if (!string.IsNullOrEmpty(collection["MaritalStatus"]))
                                    empDetails.empOnboardingDetails.MaritalStatus = collection["MaritalStatus"];
                                if (!string.IsNullOrEmpty(collection["DateOfMarriage"]))
                                    empDetails.empOnboardingDetails.MarriageDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["DateOfMarriage"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                                if (!string.IsNullOrEmpty(collection["PlaceOfBirth"]))
                                    empDetails.empOnboardingDetails.PlaceOfBirth = collection["PlaceOfBirth"];
                                if (!string.IsNullOrEmpty(collection["MothersName"]))
                                    empDetails.empOnboardingDetails.MothersName = collection["MothersName"];
                                if (!string.IsNullOrEmpty(collection["Religion"]))
                                    empDetails.empOnboardingDetails.Religion = collection["Religion"];
                                empDetails.empOnboardingDetails.PhysicallyChallenged = false;
                                empDetails.empOnboardingDetails.InternationalEmployee = false;
                                empDetails.empOnboardingDetails.PresentAddress = collection["PresentAddress"];
                                empDetails.empOnboardingDetails.PermanentAddress = collection["PermanentAddress"];
                                if (!string.IsNullOrEmpty(collection["EmergencyContactNumber"]))
                                    empDetails.empOnboardingDetails.AlternateContactNo = collection["EmergencyContactNumber"];
                                if (!string.IsNullOrEmpty(collection["EmergencyContactName"]))
                                    empDetails.empOnboardingDetails.AlternateContactName = collection["EmergencyContactName"];
                                if (!string.IsNullOrEmpty(collection["NomineeName"]))
                                    empDetails.empOnboardingDetails.NomineeName = collection["NomineeName"];
                                if (!string.IsNullOrEmpty(collection["NomineeContactNo"]))
                                    empDetails.empOnboardingDetails.NomineeContactNumber = collection["NomineeContactNo"];
                                if (!string.IsNullOrEmpty(collection["NomineeDateOfBirth"]))
                                    empDetails.empOnboardingDetails.NomineeDob = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["NomineeDateOfBirth"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                                if (!string.IsNullOrEmpty(collection["NomineeRelation"]))
                                    empDetails.empOnboardingDetails.RelationWithNominee = collection["NomineeRelation"];
                                //Time Stamp
                                empDetails.empOnboardingDetails.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                                empDetails.empOnboardingDetails.CreatedBy = GetLoggedInUserId();
                            }
                            else
                            {
                                //Normal saving to Employee Master if selfenboarding in disabled
                                if (!string.IsNullOrEmpty(collection["BloodGroup"]))
                                    empDetails.empDetails.BloodGroup = collection["BloodGroup"];
                                if (!string.IsNullOrEmpty(collection["DateOfBirth"]))
                                    empDetails.empDetails.DateOfBirth = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["DateOfBirth"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                                if (!string.IsNullOrEmpty(collection["PlaceOfBirth"]))
                                    empDetails.empDetails.PlaceOfBirth = collection["PlaceOfBirth"];
                                if (!string.IsNullOrEmpty(collection["MaritalStatus"]))
                                    empDetails.empDetails.MaritalStatus = collection["MaritalStatus"];
                                if (!string.IsNullOrEmpty(collection["DateOfMarriage"]))
                                    empDetails.empDetails.MarriageDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["DateOfMarriage"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                                empDetails.empDetails.PlaceOfBirth = collection["FirstName"];
                                if (!string.IsNullOrEmpty(collection["MothersName"]))
                                    empDetails.empDetails.MothersName = collection["MothersName"];
                                if (!string.IsNullOrEmpty(collection["Religion"]))
                                    empDetails.empDetails.Religion = collection["Religion"];
                                // empDetails.empDetails.PhysicallyChallenged = false;
                                // empDetails.empDetails.InternationalEmployee = false;
                                empDetails.empDetails.Address = collection["PresentAddress"];
                                empDetails.empDetails.PermanentAddress = collection["PermanentAddress"];
                                if (!string.IsNullOrEmpty(collection["EmergencyContactNumber"]))
                                    empDetails.empDetails.EmergencyNumber = collection["EmergencyContactNumber"];
                                if (!string.IsNullOrEmpty(collection["EmergencyContactName"]))
                                    empDetails.empDetails.EmergencyContactName = collection["EmergencyContactName"];
                                if (!string.IsNullOrEmpty(collection["NomineeName"]))
                                    empDetails.empDetails.NomineeName = collection["NomineeName"];
                                if (!string.IsNullOrEmpty(collection["NomineeContactNo"]))
                                    empDetails.empDetails.NomineeContactNumber = collection["NomineeContactNo"];
                                if (!string.IsNullOrEmpty(collection["NomineeDateOfBirth"]))
                                    empDetails.empDetails.NomineeDob = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["NomineeDateOfBirth"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                                if (!string.IsNullOrEmpty(collection["NomineeRelation"]))
                                    empDetails.empDetails.NomineeRelation = collection["NomineeRelation"];

                            }


                            //if (!string.IsNullOrEmpty(collection["EmployeeStatus"]) && collection["EmployeeStatus"] != "0")
                            //    empOnboardingDetails.EmployeeStatus = collection["EmployeeStatus"];

                            //empOnboardingDetails.ProbationPeriod = collection["ProbationPeriod"];

                            //if (!string.IsNullOrEmpty(collection["Department"]) && collection["Department"] != "0")
                            //    empOnboardingDetails.Department = collection["Department"];

                            //if (!string.IsNullOrEmpty(collection["Designation"]) && collection["Designation"] != "0")
                            //    empOnboardingDetails.Designation = collection["Designation"];

                            //if (!string.IsNullOrEmpty(collection["Grade"]) && collection["Grade"] != "0")
                            //    empOnboardingDetails.Grade = collection["Grade"];

                            //if (!string.IsNullOrEmpty(collection["FunctionalGrade"]) && collection["FunctionalGrade"] != "0")
                            //    empOnboardingDetails.FunctionalGrade = collection["FunctionalGrade"];

                            //if (!string.IsNullOrEmpty(collection["Level"]) && collection["Level"] != "0")
                            //    empOnboardingDetails.Level = collection["Level"];

                            //if (!string.IsNullOrEmpty(collection["SubLevel"]) && collection["SubLevel"] != "0")
                            //    empOnboardingDetails.SubLevel = collection["SubLevel"];

                            //if (!string.IsNullOrEmpty(collection["CostCenter"]) && collection["CostCenter"] != "0")
                            //    empOnboardingDetails.CostCenter = collection["CostCenter"];

                            //if (!string.IsNullOrEmpty(collection["Location"]) && collection["Location"] != "0")
                            //    empOnboardingDetails.Location = collection["Location"];

                            //empOnboardingDetails.EmployeeProfilePhoto = "default-avatar.png";
                            //empOnboardingDetails.Pfnumber = collection["Pfnumber"];
                            //empOnboardingDetails.Uannumber = collection["Uannumber"];
                            //empOnboardingDetails.IncludeEsi = Convert.ToBoolean(collection["IncludeEsi"]);
                            //empOnboardingDetails.IncludeLwf = Convert.ToBoolean(collection["IncludeLwf"]);

                            //if (!string.IsNullOrEmpty(collection["PaymentMethod"]) && collection["PaymentMethod"] != "0")
                            //    empOnboardingDetails.PaymentMethod = collection["PaymentMethod"];

                            //if (!string.IsNullOrEmpty(collection["IsSelfOnboarding"]))
                            //    empOnboardingDetails.IsSelfOnboarding = Convert.ToBoolean(collection["IsSelfOnboarding"]);
                            //else
                            //    empOnboardingDetails.IsSelfOnboarding = false;

                            //empOnboardingDetails.IsActive = true;
                            //empOnboardingDetails.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                            //empOnboardingDetails.CreatedBy = 1;

                            empDetails.SaveEmployeeData();
                            ViewBag.Message = string.Format("Successfully Updated Employee {0}.\\n Date: {1}", empDetails.empDetails.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                            //Send mail for self enboarding with link....
                            //if (Convert.ToBoolean(empDetails.empDetails.IsSelfOnboarding))
                            //{
                            //    bool success = SendSelfOnboardingMail(empDetails.empDetails);
                            //}
                            if (!string.IsNullOrEmpty(collection["CallingView"]))
                            {
                                //Set calling view

                                if (collection["CallingView"] == "EditEmployeeDetails")
                                    return EmployeeProfileDetails(loggedEmployeeId, "EditEmployeeDetails");
                                else
                                    return EmployeeOnbList();
                            }
                            else
                            {
                                //return EmployeeProfileDetails(loggedEmployeeId, "");
                                return EmployeeOnbList();
                            }
                                
                            //return View("EditEmployeeDetails", empDetails);
                            //using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                            //{
                            //    entities.Employees.Add(emp);
                            //entities.Add(emp);
                            //entities.SaveChanges();
                            //    id = emp.EmployeeId;
                            //}

                            //Adding user details for new employee

                            //if (id > 0)
                            //{
                            //    UserDetail user = new UserDetail();

                            //    if (!string.IsNullOrEmpty(emp.EmployeeNumber.ToString()))
                            //    {
                            //        user.UserName = emp.EmployeeNumber.ToString();
                            //        user.UserPassword = emp.EmployeeNumber.ToString();

                            //        if (!string.IsNullOrEmpty(emp.EmployeeId.ToString()))
                            //            user.EmployeeId = emp.EmployeeId;

                            //        user.RoleId = 4;

                            //        user.IsPwdChangeFt = false;

                            //        if (!string.IsNullOrEmpty(emp.FirstName))
                            //            user.FirstName = emp.FirstName;
                            //        if (!string.IsNullOrEmpty(emp.LastName))
                            //            user.LastName = emp.LastName;
                            //        if (!string.IsNullOrEmpty(emp.Email))
                            //            user.Email = emp.Email;
                            //        if (!string.IsNullOrEmpty(emp.ContactNumber))
                            //            user.Contact = emp.ContactNumber;

                            //        user.ProfilePicturePath = "default-avatar.png";

                            //        using (SHR_SHOBIGROUP_DBContext entities = new SHR_SHOBIGROUP_DBContext())
                            //        {
                            //            entities.UserDetails.Add(user);
                            //            entities.SaveChanges();
                            //        }

                            //        ViewBag.Message = string.Format("Successfully Added Employee {0}.\\n Date: {1}", emp.EmployeeNumber, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));



                            //    }


                            //}


                        }

                    }

                    return View("EditEmployeeDetails", empDetails);
                }
                // return View("EditEmployeeDetails", empDetails);
                return RedirectToAction("EmployeeProfileDetails", "Employee");
               //return View("EditEmployeeDetails", empDetails);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return RedirectToAction("EmployeeProfileDetails", "Employee");
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

        private bool SendSelfOnboardingMail(Employee emp, int templateId)
        {
            string htmlText;
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();

            string baseUrl = _configuration["AppData:BaseUrlLocal"];

            string callUrl = baseUrl + "Employee/EmpSetPassword?eid=" + DataSecurity.Encode(emp.EmployeeId.ToString());

            if (templateId == 1)
            {
                htmlText = _context.EmailTemplates.Where(x => x.EmailTemplateTitle == "EmployeeOnboardingTemplate").Select(x => x.EmailTemplateHtml).SingleOrDefault();
            }
            else
            {
                htmlText = _context.EmailTemplates.Where(x => x.EmailTemplateTitle == "EmployeeOnboardingSuccessTemplate").Select(x => x.EmailTemplateHtml).SingleOrDefault();
            }
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
            return true;

        }
        #endregion
    }
}
