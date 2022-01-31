using BLL.Dtos.Menu;
using Data.Models;
using System.Collections.Generic;

namespace BLL.Dtos
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            this.UserRolesAssigned = new List<UserInRole>();
            this.AllowedMenuList = new List<MenuDto>();
            this.AllowedMainMenuList = new List<MenuDto>();
            this.Role = new Role();
        }
        public User User { get; set; }
        public Role Role { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool IsBreakIn { get; set; }
        public List<UserInRole> UserRolesAssigned { get; set; }
        public List<MenuDto> AllowedMenuList { get; set; }
        public List<MenuDto> AllowedMainMenuList { get; set; }
    }
}
