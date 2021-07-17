using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmployeeOnboardingDetails
    {
        SHR_SHOBIGROUP_DBContext dbContext = new SHR_SHOBIGROUP_DBContext();
        //public int EmployeeId { get; set; }
        //public int? CompanyId { get; set; }
        //public int? EmployeeNumber { get; set; }
        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        //public string LastName { get; set; }
        //public string ContactNumber { get; set; }
        //public string EmployeeProfilePhoto { get; set; }
        //public string Email { get; set; }
        //public string Department { get; set; }
        //public string Designation { get; set; }
        //public string ReportingManager { get; set; }
        public Employee empDetails { get; set; }
        public EmpOnboardingDetail empOnboardingDetails { get; set; }
        public List<EmpEducationDetail> empEducationDetail { get; set; }
        public EmpBankDetail empBankDetail { get; set; }
        public List<PrevEmploymentDetail> prevEmploymentDetail { get; set; }
        public List<EmpDocument> empDocument { get; set; }

        public EmployeeOnboardingDetails(string empId)
        {
            if (empId != null && empId != "")
            {
                Employee empData = new Employee();
                empData = dbContext.Employees.Where(x => x.EmployeeId == Convert.ToInt32(empId)).ToList().SingleOrDefault();
                empDetails = empData;

                //If self enboarding is enabled
                if (Convert.ToBoolean(empData.IsSelfOnboarding))
                {
                    EmpOnboardingDetail empBoardData = new EmpOnboardingDetail();
                    empBoardData = dbContext.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(empId)).ToList().SingleOrDefault();
                    if (empBoardData != null)
                    {
                        empOnboardingDetails = empBoardData;
                    }
                    else
                    {
                        empOnboardingDetails = new EmpOnboardingDetail();
                    }
                }
                //this.empOnboardingDetails = new EmpOnboardingDetail();

                //this.empDetails.EmployeeId = empData.EmployeeId;
                //this.empDetails.FirstName = empData.FirstName;
                //this.empDetails.MiddleName = empData.MiddleName.ToString();
                //this.empDetails.LastName = empData.LastName;
                //this.empDetails.ContactNumber = empData.ContactNumber;
                //this.empDetails.Email = empData.Email;
                //this.empDetails.EmployeeProfilePhoto = empData.EmployeeProfilePhoto;
                //this.empDetails.Department = empData.Department;
                //this.empDetails.Designation = empData.Designation;


                //leaveApplyListAll = _context.LeaveApplyDetails.ToList();

            }

        }

        public int SaveEmployeeData()
        {
                //int? idOnb = null;
                int idEmp = 0;

            //Employee empData = new Employee();
            //empData = dbContext.Employees.Where(x => x.EmployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).ToList().SingleOrDefault();

            ////empData.FirstName = this.empDetails.FirstName;
            ////empData.MiddleName = this.empDetails.MiddleName;

            ////empData.DateOfBirth = this.empOnboardingDetails.DateOfBirth;



            if (!Convert.ToBoolean(empDetails.IsSelfOnboarding))
            {
                if (dbContext.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).Count()<=0)
                { 
                    
                    dbContext.EmpOnboardingDetails.Add(empOnboardingDetails);
                }
                else
                {

                }
            }



                //if (Convert.ToBoolean(empData.IsSelfOnboarding))
                //{

                //    //this.empOnboardingDetails.OnbemployeeId = this.empDetails.EmployeeId;
                //    //this.empOnboardingDetails.FathersName = empData.FathersName;
                //    //this.empOnboardingDetails.MothersName = empData.MothersName;
                //    //this.empOnboardingDetails.SpoucesName = empData.SpouseName;

                //    using (SHR_SHOBIGROUP_DBContext dbOnb = new SHR_SHOBIGROUP_DBContext())
                //    {
                //        if (dbOnb.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).ToList().Count() <= 0)
                //        {
                //            dbOnb.EmpOnboardingDetails.Add(empOnboardingDetails);
                //            idOnb = dbOnb.SaveChanges();
                //        }
                //        else
                //        {
                //            EmpOnboardingDetail localEmpOnboardingDetails = new EmpOnboardingDetail();
                //            localEmpOnboardingDetails = dbOnb.EmpOnboardingDetails.Where(x => x.OnbemployeeId == Convert.ToInt32(this.empDetails.EmployeeId)).ToList().SingleOrDefault();

                //            localEmpOnboardingDetails.BloodGroup = empOnboardingDetails.BloodGroup;
                //            localEmpOnboardingDetails.DateOfBirth = empOnboardingDetails.DateOfBirth;
                //            localEmpOnboardingDetails.MaritalStatus = empOnboardingDetails.MaritalStatus;
                //            localEmpOnboardingDetails.MarriageDate = empOnboardingDetails.MarriageDate;
                //            localEmpOnboardingDetails.PlaceOfBirth = empOnboardingDetails.PlaceOfBirth;
                //            localEmpOnboardingDetails.MothersName = empOnboardingDetails.SpoucesName;
                //            localEmpOnboardingDetails.FathersName = empOnboardingDetails.FathersName;
                //            localEmpOnboardingDetails.Religion = empOnboardingDetails.FathersName;
                //            localEmpOnboardingDetails.SpoucesName = empOnboardingDetails.SpoucesName;
                //            localEmpOnboardingDetails.PhysicallyChallenged = empOnboardingDetails.MothersName;
                //            localEmpOnboardingDetails.InternationalEmployee = empOnboardingDetails.SpoucesName;
                //            localEmpOnboardingDetails.PresentAddress = empOnboardingDetails.FathersName;
                //            localEmpOnboardingDetails.PermanentAddress = empOnboardingDetails.MothersName;
                //            localEmpOnboardingDetails.AlternateContactNo = empOnboardingDetails.SpoucesName;
                //            localEmpOnboardingDetails.AlternateContactName = empOnboardingDetails.FathersName;
                //            localEmpOnboardingDetails.NomineeName = empOnboardingDetails.MothersName;
                //            localEmpOnboardingDetails.RelationWithNominee = empOnboardingDetails.SpoucesName;
                //            localEmpOnboardingDetails.NomineeDob = empOnboardingDetails.FathersName;
                //            localEmpOnboardingDetails.CreatedDate = empOnboardingDetails.MothersName;
                //            localEmpOnboardingDetails.CreatedBy = empOnboardingDetails.SpoucesName;
                //        idOnb = dbOnb.SaveChanges();
                //        }
                //        //idOnb = empOnboardingDetails.OnbemployeeId;
                //    }
                //}
                //else
                //{

                //    idEmp = dbContext.SaveChanges();

                //}
                idEmp = dbContext.SaveChanges();
                return idEmp;
            
            
        }


            private String CheckNull(String args)
            {
            if (string.IsNullOrEmpty(args))
            {
                return " ";
            }
            else return args;
        }
    }
}
