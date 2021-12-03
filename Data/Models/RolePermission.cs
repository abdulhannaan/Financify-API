using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int? SubMenuId { get; set; }
        public int PermissionId { get; set; }
        public int? ScopeId { get; set; }

        public virtual MenuItem Menu { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
