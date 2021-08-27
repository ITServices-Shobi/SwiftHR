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

        // GET: Leaves List
        public ActionResult LeavesList(IFormCollection collection)
        {
            string[] myArray= { "", "", "", "", "", "", "", "", "", "", "", "", "" };
            
            LeavesAllDetails empLeavesAll=null;
            string localManagerId=null;
            string leavesPeriod=null;
            if (!string.IsNullOrEmpty(collection["leavePeriod"].ToString()))
            {
                localManagerId = GetLoggedInEmpId().ToString();
                leavesPeriod = collection["leavePeriod"].ToString();
                myArray[Convert.ToInt32(leavesPeriod)] = "autofocus";
                empLeavesAll = new LeavesAllDetails(localManagerId);

            }
            else
            {
                myArray[0] = "autofocus";
                empLeavesAll = new LeavesAllDetails(localManagerId);
            }
            ViewBag.leaveListSelection = myArray;
            return View("LeavesApplyDetails", empLeavesAll);

        }

        //// GET: Leaves List
        //public ActionResult LeavesList(string empId)
        //{
        //    LeavesAllDetails empLeavesAll = new LeavesAllDetails();
        //    return View("LeavesApplyDetails", empLeavesAll);

        //}

        public ActionResult LeavesStatus(string empId)
        {
            //Employee empData = new Employee();
            //empData = _context.Employees.Where(o => o.EmployeeId == Convert.ToInt32(empId)).SingleOrDefault();
            //return PartialView("LeavesStatus", empData);
            //return View("LeavesStatus", empData);

            //LeavesEmployeeDetails leaveEmployeeData = new LeavesEmployeeDetails(empId);

            string managerEmpId = GetLoggedInEmpId().ToString();
            LeavesAllDetails empLeavesAll = new LeavesAllDetails(empId, managerEmpId);

            return PartialView("LeaveStatus", empLeavesAll);

        }

        // POST: LeavesController/UpdateStatus
        [HttpPost("UpdateLeavesStatus")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLeavesStatus(string empId, string leaveId, string leaveStatus, string rejectReason)
        {
            if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(leaveId) && !string.IsNullOrEmpty(leaveStatus))
            {
                LeavesAllDetails empLeavesDetails;
                empLeavesDetails = GetEmployeeLeavesDetails(empId);

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


        private LeavesAllDetails GetEmployeeLeavesDetails(string empId)
        {
            LeavesAllDetails empLeavesDetails = new LeavesAllDetails(empId);
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
