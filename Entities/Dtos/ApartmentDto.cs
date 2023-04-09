﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ApartmentDto : IDto
    {
        public int ApartmentId { get; set; }

        public Guid Guid { get; set; }

        public string? Code { get; set; }

        public string? ApartmentName { get; set; }

        public string? BlockNo { get; set; }

        public string? DoorNumber { get; set; }
        public int NumberOfFlats { get; set; }
        public string? OpenAdress { get; set; }
        public bool IsActive { get; set; }
    }
    public class ApartmentAddDto : IDto
    {
        public string? ApartmentName { get; set; }

        public string? BlockNo { get; set; }

        public string? DoorNumber { get; set; }

        public int ResponsibleMemberId { get; set; }

        public int NumberOfFlats { get; set; }

        public int CityId { get; set; }

        public int CountyId { get; set; }

        public string? OpenAdress { get; set; }
        public bool IsActive { get; set; }
    }
}