using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dtos
{
    public class SelectListItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
    }
    public class SelectListItemId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
