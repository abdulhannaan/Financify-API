using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class RoleDto
    {
        public RoleDto()
        {
            RolePermissionList = new List<RolePermissionListDto>();
        }

        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int CreatedId { get; set; }
        public List<RolePermissionListDto> RolePermissionList { get; set; }
    }

    public class RolePermissionListDto
    {
        public RolePermissionListDto()
        {
            PermissionList = new List<PermissionDto>();
        }
        public int Id { get; set; }
        public string MenuName { get; set; }
        public bool IsParent { get; set; }
        public int SubmenuId { get; set; }
        public int ScopeId { get; set; }
        public int PermissionCount { get; set; }
        public List<PermissionDto> PermissionList { get; set; }
    }
    public class PermissionDto
    {
        public int PerId { get; set; }
        public string Permission { get; set; }
        public bool IsSelected { get; set; }
    }
}
