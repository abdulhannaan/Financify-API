using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class ContactU
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
