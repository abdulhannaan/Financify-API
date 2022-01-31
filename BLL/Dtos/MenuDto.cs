using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Menu
{
    public class MenuDto
    {

        public MenuDto()
        {
            this.SubMenus = new List<MenuDto>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string SubMenuEnum { get; set; }
        public string MenuUrl { get; set; }
        public string MenuIcon { get; set; }
        public int SortOrder { get; set; }
        public List<MenuDto> SubMenus { get; set; }
        public List<MenuPermissions> MenuPermissions { get; set; }
    }
    public class MenuPermissions
    {
        public int PermissionId { get; set; }
        public RolePermissions PermissionEnumId { get; set; }
        public bool IsAllow { get; set; }
    }
    public enum RolePermissions
    {
        Add,
        Update,
        Delete,
        View,
        Print
    }
}
