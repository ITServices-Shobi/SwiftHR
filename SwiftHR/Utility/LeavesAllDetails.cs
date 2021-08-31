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
           
            leaveApplyListAll = new List<LeaveApplyDetail>();
            leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.LeaveStatus==1).ToList();
            leaveApproveListAll = new List<LeaveApplyDetail>();
            leaveApproveListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.LeaveStatus == 3).ToList();
            leaveCancelRejectListAll = new List<LeaveApplyDetail>();
            leaveCancelRejectListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && (x.LeaveStatus == 2 || x.LeaveStatus == 4)).ToList();

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
            
                leaveApproveListAll_Local = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(employeeCode) && x.LeaveStatus == 3).ToList().Where(x => (System.Convert.ToDateTime(x.LeaveFromDate.ToString()) >= System.Convert.ToDateTime(fromLeaveDate) && System.Convert.ToDateTime(x.LeaveFromDate.ToString()) <= System.Convert.ToDateTime(toLeaveDate))
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

        public int ChangeLeavesStatus(string empId, string leaveId, string leaveStatus, string rejectReason)
        {
            int success = 0;
            switch (leaveStatus)
            {
                case "2":
                    if (leaveApproveListAll.Count > 0)
                    {
                        foreach (var empDoc in leaveApproveListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
                        {
                            empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
                            empDoc.LeaveRejectReason = rejectReason;
                            empDoc.LeaveStatusChangeDate=TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                        }
                    }
                    break;
                case "3":
                    if (leaveApplyListAll.Count > 0)
                    {
                        foreach (var empDoc in leaveApplyListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
                        {
                            empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
                        }
                    }
                    break;
                case "4":
                    if (leaveApplyListAll.Count > 0)
                    {
                        foreach (var empDoc in leaveApplyListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
                        {
                            empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
                            empDoc.LeaveRejectReason = rejectReason;
                            empDoc.LeaveStatusChangeDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MM-yyyy");
                        }
                    }
                    break;

            }
            
            success = this.dbContext.SaveChanges();
            return success;

        }

    }
}