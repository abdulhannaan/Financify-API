using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
