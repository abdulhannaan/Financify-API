using Data.DataContext;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RoleRepository : IDisposable
    {
        public Role Add(Role role)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Add(role);
                    _context.SaveChanges();
                }

                return role;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddRolePermissions(List<RolePermission> rolePerms)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.RolePermissions.AddRange(rolePerms);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Role Get(int roleId)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.Roles.Find(roleId);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Role> GetAll()
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Role Update(Role role)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Update(role);
                    _context.SaveChanges();
                }

                return role;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteRolePermissions(int roleId)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    var rolePermissionsFromDb = _context.RolePermissions.Where(o => o.RoleId == roleId).ToList();
                    _context.RolePermissions.RemoveRange(rolePermissionsFromDb);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UserInRole AddUserRole(UserInRole userInRole)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Add(userInRole);
                    _context.SaveChanges();
                }

                return userInRole;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserInRole UpdateUserRole(UserInRole userInRole)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Update(userInRole);
                    _context.SaveChanges();
                }

                return userInRole;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<UserInRole> GetUserRoles(int userId)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.UserInRoles.Where(o => o.UserId == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public bool DeleteUserRoles(int roleId)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    var rolePermissionsFromDb = _context.UserInRoles.Where(o => o.RoleId == roleId).ToList();
                    _context.UserInRoles.RemoveRange(rolePermissionsFromDb);
                    _context.SaveChanges();
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
                using (var _context = Db.Create())
                {
                    var roleFromDb = _context.Roles.Find(roleId);
                    _context.Roles.Remove(roleFromDb);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
