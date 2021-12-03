using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public int? PhoneCode { get; set; }
    }
}
