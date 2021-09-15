﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class LeaveTypeCategory
    {
        public int LeaveTypeCategoryId { get; set; }
        public string LeaveTypeCategoryName { get; set; }
        public string LeaveTypeCategoryDesc { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
