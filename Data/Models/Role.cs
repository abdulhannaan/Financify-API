using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Role
    {
        public Role()
        {
            UserInRoles = new HashSet<UserInRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }
}
