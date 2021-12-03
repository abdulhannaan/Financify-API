using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            RolePermissions = new HashSet<RolePermission>();
            SubMenuItems = new HashSet<SubMenuItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<SubMenuItem> SubMenuItems { get; set; }
    }
}
