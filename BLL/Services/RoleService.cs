using BLL.Dtos;
using Data.DataContext;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class RoleService
    {
        public bool Save(RoleDto role)
        {
            Role newRole = new Role();
            try
            {
                if (role.RoleId == 0)
                {
                    using (var roleData = new RoleRepository())
                    {
                        newRole.Name = role.RoleName;
                        newRole.CreatedOn = DateTime.Now;
                        newRole.CreatedBy = role.CreatedId;
                        roleData.Add(newRole);
                    }

                    List<RolePermission> permissionList = new List<RolePermission>();

                    foreach (var item in role.RolePermissionList)
                    {
                        var selectedPerms = item.PermissionList.Any(o => o.IsSelected == true);
                        if (selectedPerms)
                        {
                            foreach (var Per in item.PermissionList)
                            {
                                if (Per.IsSelected)
                                {
                                    permissionList.Add(new RolePermission()
                                    {
                                        RoleId = newRole.Id,
                                        MenuId = item.Id,
                                        SubMenuId = item.SubmenuId,
                                        PermissionId = Convert.ToInt32(Per.PerId),
                                        ScopeId = item.ScopeId,
                                    });
                                }

                            }
                        }

                    }

                    using (var roleData = new RoleRepository())
                    {
                        roleData.AddRolePermissions(permissionList);
                    }

                }
                else if (role.RoleId > 0)
                {
                    using (var roleData = new RoleRepository())
                    {
                        var roleFromDb = roleData.Get(role.RoleId);

                        roleFromDb.Name = role.RoleName;
                        roleFromDb.UpdatedOn = DateTime.Now;
                        roleFromDb.UpdatedBy = role.CreatedId;

                        roleData.Update(roleFromDb);

                        roleData.DeleteRolePermissions(role.RoleId);

                        List<RolePermission> permissionList = new List<RolePermission>();

                        foreach (var item in role.RolePermissionList)
                        {
                            var selectedPerms = item.PermissionList.Any(o => o.IsSelected == true);
                            if (selectedPerms)
                            {
                                foreach (var Per in item.PermissionList)
                                {
                                    if (Per.IsSelected)
                                    {
                                        permissionList.Add(new RolePermission()
                                        {
                                            RoleId = roleFromDb.Id,
                                            MenuId = item.Id,
                                            SubMenuId = item.SubmenuId,
                                            PermissionId = Convert.ToInt32(Per.PerId),
                                            ScopeId = item.ScopeId,
                                        });
                                    }
                                }
                            }
                        }
                        roleData.AddRolePermissions(permissionList);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return false;
            }
        }

        public bool Delete(int roleId)
        {
            try
            {
                using (var roleData = new RoleRepository())
                {
                    var deleteRolePerms = roleData.DeleteRolePermissions(roleId);
                    var deleteUserRoles = roleData.DeleteUserRoles(roleId);
                    var deleteRole = roleData.Delete(roleId);
                    if (deleteRolePerms && deleteRole && deleteUserRoles)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return false;
            }
        }

        public RoleDto Get(int roleId)
        {
            try
            {
                var roleModel = new RoleDto();

                if (roleId == null || roleId == 0)
                {
                    List<SelectListItem> permissionList = new List<SelectListItem>();

                    var MainMenuList = new List<MenuItem>();
                    var SubMenuList = new List<SubMenuItem>();
                    var Permissions = new List<Permission>();
                    using (var userData = new UserRepository())
                    {
                        Permissions = userData.GetAllPermissions();
                        MainMenuList = userData.GetAllMenus();
                        SubMenuList = userData.GetAllSubMenus();
                    }

                    permissionList = Permissions.Select(e => new SelectListItem()
                    {
                        Text = e.Permission1,
                        Value = Convert.ToString(e.Id)
                    }).ToList();

                    if (permissionList.Count > 0)
                    {
                        var RolePermissionList = new List<RolePermissionListDto>();

                        if (MainMenuList != null && MainMenuList.Count > 0)
                        {
                            foreach (var menuItem in MainMenuList)
                            {
                                RolePermissionListDto Parent = new RolePermissionListDto();
                                Parent.MenuName = menuItem.Name;
                                Parent.IsParent = true;
                                Parent.Id = menuItem.Id;
                                Parent.PermissionCount = Permissions.Count + 1;
                                Parent.PermissionList = Permissions.Select(p => new PermissionDto()
                                {
                                    Permission = p.Permission1,
                                    PerId = p.Id
                                }).ToList();
                                RolePermissionList.Add(Parent);

                                if (SubMenuList != null && SubMenuList.Count > 0)
                                {
                                    var subMenuOfParent = SubMenuList.Where(o => Convert.ToInt32(o.MenuId) == menuItem.Id).ToList();
                                    if (subMenuOfParent != null && subMenuOfParent.Count > 0)
                                    {
                                        foreach (var subItem in subMenuOfParent)
                                        {
                                            RolePermissionListDto Child = new RolePermissionListDto();
                                            Child.MenuName = subItem.Name;
                                            Child.SubmenuId = subItem.Id;
                                            Child.Id = menuItem.Id;
                                            Child.PermissionList = Permissions.Select(p => new PermissionDto()
                                            {
                                                Permission = p.Permission1,
                                                PerId = p.Id
                                            }).ToList();
                                            RolePermissionList.Add(Child);
                                        }
                                    }
                                }
                            }

                            roleModel.RolePermissionList = RolePermissionList;
                        }
                    }

                }
                else
                {
                    Role existingRoleDetail = new Role();
                    using (var roleData = new RoleRepository())
                    {
                        existingRoleDetail = roleData.Get(roleId);
                    }
                    if (existingRoleDetail != null && existingRoleDetail.Id > 0)
                    {
                        roleModel.RoleId = existingRoleDetail.Id;
                        roleModel.RoleName = existingRoleDetail.Name;

                        var RolePermissionList = new List<RolePermissionListDto>();

                        var Permissions = new List<Permission>();
                        var MainMenuList = new List<MenuItem>();
                        var SubMenuList = new List<SubMenuItem>();
                        var RolePermissions = new List<RolePermission>();
                        using (var userData = new UserRepository())
                        {
                            Permissions = userData.GetAllPermissions();
                            MainMenuList = userData.GetAllMenus();
                            SubMenuList = userData.GetAllSubMenus();
                            RolePermissions = userData.GetRolePermissionsById(roleId); ;
                        }

                        foreach (var menuItem in MainMenuList)
                        {
                            RolePermissionListDto Parent = new RolePermissionListDto();
                            Parent.MenuName = menuItem.Name;
                            Parent.IsParent = true;
                            Parent.Id = menuItem.Id;
                            Parent.PermissionCount = Permissions.Count + 1;
                            Parent.PermissionList = Permissions.Select(p => new PermissionDto()
                            {
                                Permission = p.Permission1,
                                PerId = p.Id,
                                IsSelected = RolePermissions.Where(rp => rp.PermissionId == p.Id && rp.MenuId == menuItem.Id).FirstOrDefault() != null ? true : false
                            }).ToList();
                            RolePermissionList.Add(Parent);

                            var subMenuOfParent = SubMenuList.Where(o => Convert.ToInt32(o.MenuId) == menuItem.Id).ToList();

                            if (subMenuOfParent != null && subMenuOfParent.Count > 0)
                            {
                                foreach (var subItem in subMenuOfParent)
                                {
                                    RolePermissionListDto Child = new RolePermissionListDto();
                                    Child.MenuName = subItem.Name;
                                    Child.SubmenuId = subItem.Id;
                                    Child.Id = menuItem.Id;
                                    Child.PermissionList = Permissions.Select(p => new PermissionDto()
                                    {
                                        Permission = p.Permission1,
                                        PerId = p.Id,
                                        IsSelected = RolePermissions.Where(rp => rp.PermissionId == p.Id && rp.SubMenuId == subItem.Id).FirstOrDefault() != null ? true : false
                                    }).ToList();
                                    RolePermissionList.Add(Child);
                                }
                            }
                            roleModel.RolePermissionList = RolePermissionList;
                        }
                    }
                }

                return roleModel;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public List<Role> GetAll()
        {
            try
            {
                using (var roleData = new RoleRepository())
                {
                    return roleData.GetAll();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }


        public UserInRole GetUserRole(int Id)
        {
            try
            {
                using (var roleData = new RoleRepository())
                {
                    var result = roleData.GetUserRoles(Id).FirstOrDefault(); 
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
