using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class JoinU
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        public string FileName { get; set; }
        public string CoverFileName { get; set; }
        public string CoverText { get; set; }
        public int JobId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
