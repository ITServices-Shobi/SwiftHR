using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class LeavesAllDetails
    {
        public List<Employee> empMasterDataItems { get; set; }

        public List<LeaveApplyDetail> leaveApplyListAll { get; set; }

    }
}