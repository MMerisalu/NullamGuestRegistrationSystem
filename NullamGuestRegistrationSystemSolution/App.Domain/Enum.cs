﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public enum AttendeeType
    {
        [Display(Name ="Isik")]
        Person = 1,
        [Display(Name = "Ettevõte")]
        Company
    }
}
