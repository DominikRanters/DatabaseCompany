﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Model
{
    public class Departmnet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
    }
}
