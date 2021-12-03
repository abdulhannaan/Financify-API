using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class SubMenuItem
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual MenuItem Menu { get; set; }
    }
}
