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

        public List<Employee> empMasterDataItems { get; set; }

        //public List<object> empMasterDataItems1 { get; set; }

        //Applied Leaves
        public List<LeaveApplyDetail> leaveApplyListAll { get; set; }

        //Approved Leaves
        public List<LeaveApplyDetail> leaveApproveListAll { get; set; }

        //Cancelled & Rejected Leaves
        public List<LeaveApplyDetail> leaveCancelRejectListAll { get; set; }
                    


        //Constructor
        public LeavesAllDetails(string managerEmpId)
        {
            empMasterDataItems = new List<Employee>();
            empMasterDataItems = dbContext.Employees.Where(x => x.IsActive==true && x.ReportingManager == managerEmpId).ToList();

            //var empMasterDataItems1 = dbContext.Employees.Join(dbContext.LeaveApplyDetails, x => x.EmployeeId, y => y.EmployeeId, (x, y) => new {x.FirstName, x.MiddleName, x.LastName, x.EmployeeId,x.EmployeeProfilePhoto });



            //var empMasterDataItems1 = from a in dbContext.LeaveApplyDetails
            //                         join c in dbContext.Employees on a.EmployeeId equals c.EmployeeId
            //                         where c.IsActive == true && System.Convert.ToDateTime(a.LeaveFromDate).Month == System.DateTime.Now.Month
            //                         select c.FirstName.ToList();


            leaveApplyListAll = new List<LeaveApplyDetail>();
            leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 1).ToList();
            leaveApproveListAll = new List<LeaveApplyDetail>();
            leaveApproveListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 3).ToList();
            leaveCancelRejectListAll = new List<LeaveApplyDetail>();
            leaveCancelRejectListAll = dbContext.LeaveApplyDetails.Where(x => x.LeaveStatus == 2 || x.LeaveStatus == 4).ToList();

        }
        public LeavesAllDetails(string empId, string managerEmpId)
        {
            empMasterDataItems = new List<Employee>();
            empMasterDataItems = dbContext.Employees.Where(x => x.IsActive == true && x.EmployeeId == Convert.ToInt32(empId) && x.ReportingManager == managerEmpId).ToList();
            leaveApplyListAll = new List<LeaveApplyDetail>();
            leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.LeaveStatus==1).ToList();
            leaveApproveListAll = new List<LeaveApplyDetail>();
            leaveApproveListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && x.LeaveStatus == 3).ToList();
            leaveCancelRejectListAll = new List<LeaveApplyDetail>();
            leaveCancelRejectListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId) && (x.LeaveStatus == 2 || x.LeaveStatus == 4)).ToList();

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
                        }
                    }
                    break;

            }
            
            success = this.dbContext.SaveChanges();
            return success;

        }

    }
}