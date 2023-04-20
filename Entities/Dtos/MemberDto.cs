﻿using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class MemberShortDto
    {
        public string? NameSurname { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

    }
}
