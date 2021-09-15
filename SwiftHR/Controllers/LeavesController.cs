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
using System.Collections;

namespace SwiftHR.Controllers
{
    public class LeavesController : Controller
    {

        private readonly ILogger<LeavesController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private IConfiguration _configuration;

        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();

        public LeavesController(ILogger<LeavesController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }

        // GET: Holiday Calendar
        public ActionResult HolidayCalendar()
        {
            return View("HolidayCalendar");
        }

        // GET: Leaves Settings
        public ActionResult LeavesSettings(IFormCollection collection)
        {
            LeaveSettings leaveSettings = new LeaveSettings();

            return View("LeavesSettings", leaveSettings);
        }
            
        
        // GET: Leaves List
            public ActionResult LeavesList(IFormCollection collection)
        {
            string[] myArray= { "", "", "", "", "", "", "", "", "", "", "", "", "","","","",""};
            
            
            LeavesAllDetails empLeavesAll=null;
            string localManagerId= GetLoggedInEmpId().ToString();
            string fromLeaveDate = null;
            string toLeaveDate = null;

            int leavesMonth=0;
            if (!string.IsNullOrEmpty(collection["leavePeriod"].ToString()) && Convert.ToInt32(collection["leavePeriod"]) > 0)
            {
                leavesMonth = Convert.ToInt32(collection["leavePeriod"]);
                if(leavesMonth<=12)
                {
                    fromLeaveDate = string.Concat("01-", leavesMonth, "-", System.DateTime.Today.Year.ToString());
                    toLeaveDate = string.Concat(DateTime.DaysInMonth(System.DateTime.Today.Year, leavesMonth), "-", leavesMonth, "-", System.DateTime.Today.Year.ToString());
                }
                else
                {
                    if (!string.IsNullOrEmpty(collection["rangeLeaveFromDate"]) && !string.IsNullOrEmpty(collection["rangeLeaveToDate"]))
                    {
                        fromLeaveDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["rangeLeaveFromDate"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                        toLeaveDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(collection["rangeLeaveToDate"]), IST_TIMEZONE).ToString("dd-MM-yyyy");
                        myArray[15] = System.Convert.ToDateTime(fromLeaveDate).ToString("yyyy-MM-dd");
                        myArray[16] = System.Convert.ToDateTime(toLeaveDate).ToString("yyyy-MM-dd");
                        //System.Convert.ToDateTime(empOnboardingDetails.empDetails.DateOfBirth).ToString("yyyy-MM-dd");
                    }
                }
            }
            else
            {
                fromLeaveDate = string.Concat("01-", "01", "-", System.DateTime.Today.Year.ToString());
                toLeaveDate = string.Concat("31-", "12", "-", System.DateTime.Today.Year.ToString());

            }
            empLeavesAll = new LeavesAllDetails(localManagerId, fromLeaveDate, toLeaveDate);
            myArray[leavesMonth] = "autofocus";
            myArray[13] = System.DateTime.Today.Year.ToString();
            ViewBag.leaveListSelection = myArray;
            return View("LeavesApplyDetails", empLeavesAll);

        }

        public ActionResult LeavesStatus(string empId= null)
        {
            LeavesAllDetails empLeavesAll;
            if (!string.IsNullOrEmpty(empId))
            {
                string managerEmpId = GetLoggedInEmpId().ToString();
                empLeavesAll = new LeavesAllDetails(empId, managerEmpId);
            }
            else
            {
                string localEmpId = GetLoggedInEmpId().ToString();
                empId = GetLoggedInEmpId().ToString();
                empLeavesAll = new LeavesAllDetails(localEmpId);
                ViewBag.CallingView = "ApplyLeaves";
            }

            int[] monthlyLeaveCount = empLeavesAll.GetMonthWiseEmployeeLeavesCount(System.DateTime.Today.Year, empId);
            var result = string.Join(",", monthlyLeaveCount);
            ViewBag.monthlyLeaveCount = result;
            ViewBag.chartLegends = monthlyLeaveCount;
            return PartialView("LeaveStatus", empLeavesAll);

        }
        

        // POST: LeavesController/UpdateStatus
        [HttpPost("UpdateLeavesStatus")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLeavesStatus(string empId, string leaveId, string leaveStatus, string rejectReason)
        {
            if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(leaveId) && !string.IsNullOrEmpty(leaveStatus))
            {
                string managerEmpId = GetLoggedInEmpId().ToString();
                LeavesAllDetails empLeavesDetails;
                empLeavesDetails = GetEmployeeLeavesDetails(empId, managerEmpId);

                int recordsUpdated = empLeavesDetails.ChangeLeavesStatus(empId, leaveId, leaveStatus, rejectReason);
                //bool success = SendSelfOnboardingMail(empLeavesDetails.empDetails, 2);
                string msg = null;
                ArrayList arrayResponse = new ArrayList();
                if(leaveStatus== _configuration["LeaveStatus:Approved"])
                {
                    msg = string.Format("Employee Leave(s) Successfully Approved ! {0}.\\n Date: {1}", empId, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else if(leaveStatus == _configuration["LeaveStatus:Cancelled"])
                {
                    msg = string.Format("Employee Leave(s) Successfully Cancelled ! {0}.\\n Date: {1}", empId, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else if (leaveStatus == _configuration["LeaveStatus:Rejected"])
                {
                    msg = string.Format("Employee Leave(s) Successfully Rejected ! {0}.\\n Date: {1}", empId, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                
                arrayResponse.Add(msg);

                string result = JsonConvert.SerializeObject(arrayResponse);
                return new JsonResult(result);

                //return EmployeeOnbList();
            }
            //return EmployeeOnbList();
            return RedirectToAction("LeavesList", "Leaves");
        }

        // POST: LeavesController/UpdateStatus
        [HttpPost("AddUpdateLeaves")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUpdateLeaves(string empId, string leaveId, string leaveType, string fromDate, string toDate, string leaveReason)
        {
            if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(leaveType) && !string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                string managerEmpId = GetLoggedInEmpId().ToString();
                LeavesAllDetails empLeavesDetails;
                empLeavesDetails = GetEmployeeLeavesDetails(empId, managerEmpId);

                int recordsUpdated = empLeavesDetails.AddUpdateLeaves(empId, leaveType, fromDate, toDate, leaveReason, leaveId);
                //bool success = SendSelfOnboardingMail(empLeavesDetails.empDetails, 2);
                string msg = null;
                int error = 0;
                ArrayList arrayResponse = new ArrayList();
                if (recordsUpdated == 1)
                {
                    msg = string.Format("Employee Leave(s) Successfully Saved ! {0}.\\n Date: {1}", empId, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                }
                else if (recordsUpdated == 0)
                {
                    msg = string.Format("Employee Leave(s) Could Not Be Saved ! {0}.\\n Date: {1}", empId, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    error = 1;
                }
                else if (recordsUpdated == 2)
                {
                    msg = string.Format("Overlapping leaves ! Employee Leave(s) Could Not Be Saved ! {0}.\\n Date: {1}", empId, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy"));
                    error = 1;
                }

                arrayResponse.Add(msg);
                arrayResponse.Add(error);

                string result = JsonConvert.SerializeObject(arrayResponse);
                return new JsonResult(result);

                //return EmployeeOnbList();
            }
            //return EmployeeOnbList();
            return RedirectToAction("LeavesStatus", "Leaves");
        }


        private LeavesAllDetails GetEmployeeLeavesDetails(string empId, string managerEmpId)
        {
            LeavesAllDetails empLeavesDetails = new LeavesAllDetails(empId, managerEmpId);
            return empLeavesDetails;

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

        // GET: LeavesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeavesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeavesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeavesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LeavesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeavesController/Edit/5
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

        // GET: LeavesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeavesController/Delete/5
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
    }
}
