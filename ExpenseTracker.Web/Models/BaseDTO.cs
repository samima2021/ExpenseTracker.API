﻿using System;

namespace ExpenseTracker.Web.Models
{
    public class BaseDTO
    {
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsRowDeleted { get; set; }
    }
}
