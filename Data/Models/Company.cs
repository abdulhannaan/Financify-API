using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Company
    {
        public Company()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Domain { get; set; }
        public bool IsOwner { get; set; }
        public bool? IsDeleted { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
