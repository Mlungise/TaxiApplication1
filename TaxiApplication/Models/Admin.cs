﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApplication.Models
{
    public class Admin
    {
        [Key]
        [Display(Name ="Admin ID")]
        public string AdminID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}