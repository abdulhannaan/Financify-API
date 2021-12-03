using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string Permission1 { get; set; }
        public int? PermissionEnumVal { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
