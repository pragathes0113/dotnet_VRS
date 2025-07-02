﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class HouseType
    {
        public int HouseTypeID { get; set; }
        public string HouseTypeName { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
