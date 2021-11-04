using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CustomerForAddDto:IDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressForAddDto Address { get; set; }
    }
}
