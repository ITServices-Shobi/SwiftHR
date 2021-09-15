using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class LeavesAllDetails
    {
        SHR_SHOBIGROUP_DBContext dbContext = new SHR_SHOBIGROUP_DBContext();
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public List<Employee> empMasterDataItems { get; set; }

        //public List<object> empMasterDataItems1 { get; set; }

        //Applied Leaves
        public List<LeaveApplyDetail> leaveApplyListAll { get; set; }

        //Approved Leaves
        public List<LeaveApplyDetail> leaveApproveListAll { get; set; }

        //Cancelled & Rejected Leaves
        public List<LeaveApplyDetail> leaveCancelRejectListAll { get; set; }

        public List<LeaveType> leaveTypesListAll { get; set; }

        public List<LeaveTypeMapping> leaveTypeMappingAll { get; set; }

        public List<LeaveApplyDetail>  leaveAppliedApprovedListAll { get; set; }

        //Constructor
        //public LeavesAllDetails(string managerEmpId, int leaveYear)
        //{
        //    empMasterDataItems = new List<Employee>();
        //    empMasterDataItems = dbContext.Employees.Where(x => x.IsActive==true && x.ReportingManager == managerEmpId).ToList();

        //    //var empMasterDataItems1 = dbContext.Employees.Join(dbContext.LeaveApplyDetails, x => x.EmployeeId, y => y.EmployeeId, (x, y) => new {x.FirstName, x.MiddleName, x.LastName, x.EmployeeId,x.EmployeeProfilePhoto });



        //    //var empMasterDataItems1 = from a in dbContext.LeaveApplyDetails
        //    //                         join c in dbContext.Employees on a.EmployeeId equals c.EmployeeId
        //    //                         where c.IsActive == true && System.Convert.ToDateTime(a.LeaveFromDate).Month == System.DateTime.Now.Month
        //    //                         select c.FirstName.ToList();

        //    string leaveYearLocal = leaveYear.ToString();
        //    leaveApplyListAll = new List<LeaveApplyDetail>();
        //    //leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 1 && x.LeaveFromDate.Contains(leaveYearLocal)).ToList();
        //    leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 1 && (x.LeaveFromDate.Substring(x.LeaveFromDate.Length-4,4)== leaveYearLocal)).ToList();
        //    leaveApproveListAll = new List<LeaveApplyDetail>();
        //    leaveApproveListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 3 && (x.LeaveFromDate.Substring(x.LeaveFromDate.Length - 4, 4) == leaveYearLocal)).ToList();
        //    leaveCancelRejectListAll = new List<LeaveApplyDetail>();
        //    leaveCancelRejectListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 2 || x.LeaveStatus == 4 && (x.LeaveFromDate.Substring(x.LeaveFromDate.Length - 4, 4) == leaveYearLocal)).ToList();

        //}

        public LeavesAllDetails(string managerEmpId, string fromLeaveDate, string toLeaveDate)
        {
            empMasterDataItems = new List<Employee>();
            empMasterDataItems = dbContext.Employees.Where(x => x.IsActive == true && x.ReportingManager == managerEmpId).ToList();

            leaveTypesListAll = new List<LeaveType>();
            leaveTypesListAll = dbContext.LeaveTypes.ToList();

            leaveTypeMappingAll = new List<LeaveTypeMapping>();
            leaveTypeMappingAll = dbContext.LeaveTypeMappings.ToList();

            leaveApplyListAll = new List<LeaveApplyDetail>();
            leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 1).ToList().Where(x => (System.Convert.ToDateTime(x.LeaveFromDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveFromDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))
                                                                                                            || (System.Convert.ToDateTime(x.LeaveToDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveToDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))).ToList();
            leaveApproveListAll = new List<LeaveApplyDetail>();
            leaveApproveListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 3).ToList().Where(x => (System.Convert.ToDateTime(x.LeaveFromDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveFromDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))
                                                                                                            || (System.Convert.ToDateTime(x.LeaveToDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveToDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))).ToList();
            leaveCancelRejectListAll = new List<LeaveApplyDetail>();
            leaveCancelRejectListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 2 || x.LeaveStatus == 4).ToList().Where(x => (System.Convert.ToDateTime(x.LeaveFromDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveFromDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))
                                                                                                            || (System.Convert.ToDateTime(x.LeaveToDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveToDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))).ToList();


        }
        public LeavesAllDetails(string empId, string managerEmpId=null)
        {
            empMasterDataItems = new List<Employee>();
            if(!string.IsNullOrEmpty(managerEmpId))
            {
                empMasterDataItems = dbContext.Employees.Where(x => x.IsActive == true && x.EmployeeId == Convert.ToInt32(empId) && x.ReportingManager == managerEmpId).ToList();
            }
            else
            {
                empMasterDataItems = dbContext.Employees.Where(x => x.IsActive == true && x.EmployeeId == Convert.ToInt32(empId)).ToList();
            }

            leaveTypesListAll = new List<LeaveType>();
            leaveTypesListAll = dbContext.LeaveTypes.ToList();

            leaveTypeMappingAll = new List<LeaveTypeMapping>();
            leaveTypeMappingAll = dbContext.LeaveTypeMappings.ToList();

            leaveApplyListAll = new List<LeaveApplyDetail>();
            leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.LeaveStatus==1).ToList();
            leaveApproveListAll = new List<LeaveApplyDetail>();
            leaveApproveListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.LeaveStatus == 3).ToList();
            leaveCancelRejectListAll = new List<LeaveApplyDetail>();
            leaveCancelRejectListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && (x.LeaveStatus == 2 || x.LeaveStatus == 4)).ToList();
            //All applied and approved leaves
            leaveAppliedApprovedListAll = new List<LeaveApplyDetail>();
            leaveAppliedApprovedListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && (x.LeaveStatus == 1 || x.LeaveStatus == 3)).ToList();


        }
        //Validate new leave for duplicate
        public bool ValidateDuplicateLeave(string employeeCode, string fromLeaveDate, string toLeaveDate, string leaveId=null)
        {
            //SHR_SHOBIGROUP_DBContext dbContextLocal = new SHR_SHOBIGROUP_DBContext();
            LeaveApplyDetail leaveApproveListAll_validate;
            DateTime localfromLeaveDate = System.Convert.ToDateTime(fromLeaveDate);
            DateTime localtoLeaveDate = System.Convert.ToDateTime(toLeaveDate);

            foreach (DateTime day in EachCalendarDay(localfromLeaveDate, localtoLeaveDate))
            {
                if(leaveId!=null)
                {
                    leaveApproveListAll_validate = leaveAppliedApprovedListAll.Where(x => x.EmployeeId == Convert.ToInt32(employeeCode) && (x.LeaveStatus == 1 || x.LeaveStatus == 3) && x.EmpLeaveId != Convert.ToInt32(leaveId)).ToList()
                                                                        .Where(x => System.Convert.ToDateTime(x.LeaveFromDate.ToString()) <= System.Convert.ToDateTime(day) && System.Convert.ToDateTime(x.LeaveToDate.ToString()) >= System.Convert.ToDateTime(day))
                                                                                                        .ToList().SingleOrDefault();
                }
                else
                {
                    leaveApproveListAll_validate = leaveAppliedApprovedListAll.Where(x => x.EmployeeId == Convert.ToInt32(employeeCode) && (x.LeaveStatus == 1 || x.LeaveStatus == 3)).ToList()
                                                                        .Where(x => System.Convert.ToDateTime(x.LeaveFromDate.ToString()) <= System.Convert.ToDateTime(day) && System.Convert.ToDateTime(x.LeaveToDate.ToString()) >= System.Convert.ToDateTime(day))
                                                                                                        .ToList().SingleOrDefault();
                }
                
                if (leaveApproveListAll_validate != null)
                {
                    return false;
                }
            }
            return true;
        }


        //Get month wise employee leaves count
        public int[] GetMonthWiseEmployeeLeavesCount(int leaveYear, string employeeCode)
        {
            //int[] monthWiseLeaveCount = { 4, 5, 20, 0, 4, 6, 0, 0, 0, 12, 2, 1};
            int[] monthWiseLeaveCount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string fromLeaveDate = null;
            string toLeaveDate = null;
            int monthlyLeaveCount = 0;
            int yearlyLeaveCount = 0;
            int yearlyLeaveTakenCount = 0;
            List<LeaveApplyDetail> leaveApproveListAll_Local;
            leaveApproveListAll_Local = new List<LeaveApplyDetail>();

            for(int i=1;i<=12;i++)
            {
                monthlyLeaveCount = 0;
                fromLeaveDate = string.Concat("01-", i,"-", leaveYear.ToString());
                toLeaveDate = string.Concat(DateTime.DaysInMonth(System.DateTime.Today.Year, i), "-", i, "-", leaveYear.ToString());
            
                leaveApproveListAll_Local = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(employeeCode) && (x.LeaveStatus == 3 || x.LeaveStatus == 1)).ToList().Where(x => (System.Convert.ToDateTime(x.LeaveFromDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveFromDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))
                                                                                                                || (System.Convert.ToDateTime(x.LeaveToDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveToDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))).ToList();
                
                foreach (var empLeave in leaveApproveListAll_Local)
                {
                    DateTime StartDate = Convert.ToDateTime(empLeave.LeaveFromDate);
                    DateTime EndDate = Convert.ToDateTime(empLeave.LeaveToDate);
                    foreach(DateTime day in EachCalendarDay(StartDate, EndDate)) 
                    {
                        if (day.Month == i)
                        {
                            monthlyLeaveCount++;
                            yearlyLeaveCount++;
                            if (day < System.DateTime.Today)
                                yearlyLeaveTakenCount++;
                        }
                            

                    }
                    //empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
                    monthWiseLeaveCount[i - 1] = monthlyLeaveCount;
                }
            }
            monthWiseLeaveCount[12] = yearlyLeaveCount;
            monthWiseLeaveCount[13] = yearlyLeaveTakenCount;
            monthWiseLeaveCount[14] = yearlyLeaveCount- yearlyLeaveTakenCount;
            if (yearlyLeaveCount > 0 )
            {
                float result = (yearlyLeaveCount / 12);

                monthWiseLeaveCount[15] = Convert.ToInt32(Math.Round(result, 0));
            }
            return monthWiseLeaveCount;
        }
        public IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }

        public int AddUpdateLeaves(string empId, string leaveType, string fromDate, string toDate, string leaveReason, string leaveId = null)
        {
            int success = 0;
            bool validated = false;
            if (!string.IsNullOrEmpty(leaveId))
            {
                validated = ValidateDuplicateLeave(empId, fromDate, toDate, leaveId);
            }
            else
            {
                validated = ValidateDuplicateLeave(empId, fromDate, toDate);
            }
            if (validated)
            {
                LeaveApplyDetail leaveDetails = new LeaveApplyDetail();
                SHR_SHOBIGROUP_DBContext dbContextLocal = new SHR_SHOBIGROUP_DBContext();
                var empManagerName = dbContextLocal.Employees.Where(x => x.EmployeeId == Convert.ToInt32(empMasterDataItems[0].ReportingManager)).Select(x => x.FirstName).ToList();

                if (!string.IsNullOrEmpty(leaveId))
                {
                    leaveDetails = leaveApplyListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList().SingleOrDefault();

                    //foreach (var leaveDetails in leaveApproveListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
                    //{
                    //    leaveDetails.LeaveReason = leaveReason;
                    //    leaveDetails.LeaveStatusChangeDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                    //}
                }
                else
                {
                    int[] monthlyLeaveCount = this.GetMonthWiseEmployeeLeavesCount(System.DateTime.Today.Year, empId);

                    leaveDetails.EmployeeId = Convert.ToInt32(empId);
                    leaveDetails.LeaveAppliedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                    leaveDetails.LeaveStatus = 1;
                    leaveDetails.ReportingManagerUserId = Convert.ToInt32(empMasterDataItems[0].ReportingManager);
                    leaveDetails.ReportingManagerName = empManagerName[0].ToString();
                    dbContext.LeaveApplyDetails.Add(leaveDetails);
                }
                //leaveDetails = new LeaveApplyDetail();

                leaveDetails.LeaveFromDate = Convert.ToDateTime(fromDate).ToString("dd-MM-yyyy");
                leaveDetails.LeaveToDate = Convert.ToDateTime(toDate).ToString("dd-MM-yyyy");
                leaveDetails.LeaveType = Convert.ToInt32(leaveType);
                leaveDetails.LeaveReason = leaveReason;

                success = dbContext.SaveChanges();
            }
            else
            {
                success = 2;
            }
            
            return success;
        }

        public int ChangeLeavesStatus(string empId, string leaveId, string leaveStatus, string rejectReason)
        {
            int success = 0;
            if (leaveApproveListAll.Count > 0)
            {
                foreach (var empLeaveLocal in leaveAppliedApprovedListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
                {
                    if (leaveStatus == "2" || leaveStatus == "3" || leaveStatus == "4")
                    {
                        empLeaveLocal.LeaveStatus = Convert.ToInt32(leaveStatus);
                        empLeaveLocal.LeaveStatusChangeDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                    }
                    if (leaveStatus == "2" || leaveStatus == "4")
                    {
                        empLeaveLocal.LeaveRejectReason = rejectReason;
                    }
                }
            }
            //switch (leaveStatus)
            //{
            //    case "2":
            //        if (leaveApproveListAll.Count > 0)
            //        {
            //            foreach (var empDoc in leaveApproveListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
            //            {
            //                empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
            //                empDoc.LeaveRejectReason = rejectReason;
            //                empDoc.LeaveStatusChangeDate=TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
            //            }
            //        }
            //        break;
            //    case "3":
            //        if (leaveApplyListAll.Count > 0)
            //        {
            //            foreach (var empDoc in leaveApplyListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
            //            {
            //                empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
            //                empDoc.LeaveStatusChangeDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
            //            }
            //        }
            //        break;
            //    case "4":
            //        if (leaveApplyListAll.Count > 0)
            //        {
            //            foreach (var empDoc in leaveApplyListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
            //            {
            //                empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
            //                empDoc.LeaveRejectReason = rejectReason;
            //                empDoc.LeaveStatusChangeDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
            //            }
            //        }
            //        break;

            //}
            
            success = this.dbContext.SaveChanges();
            return success;

        }

        public String GetLeavetypeWiseEmployeePercentage(float totalByEmployeeLeaveType, float totalLeavesOfEmployee)
        {
            if (totalByEmployeeLeaveType > 0 && totalLeavesOfEmployee > 0)
            {
                float result = (totalByEmployeeLeaveType / totalLeavesOfEmployee) * 100;

                return Math.Round(result, 0).ToString();
            }
            return "0";
        }

    }
}