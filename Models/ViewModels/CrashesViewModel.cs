﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Models.ViewModels
{
    public class CrashesViewModel
    {
        public List<Crash> Crashes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
