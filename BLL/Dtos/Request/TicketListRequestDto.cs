using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHDBLL.Dtos.Request
{
    public class TicketListRequestDto : Pagination
    {
        public int AssignedUserId { get; set; }

        public int TicketPriorityId { get; set; }

        public int TicketStatusId { get; set; }

        public int TicketTypeId { get; set; }
    }
}
