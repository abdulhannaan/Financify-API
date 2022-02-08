using Data.DataContext;
using Data.Models;
using Data.Utils;
using Utility.Commons;
using Utility.Enumerations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Repositories
{
    public class UserRepository : IDisposable
    {
        public User Add(User user)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Add(user);
                    _context.SaveChanges();
                }

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User Update(User user)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Update(user);
                    _context.SaveChanges();
                }

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(List<int> userIds)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    var user = _context.Users.Where(o => userIds.Contains(o.Id)).ToList();
                    var role = _context.UserInRoles.Where(o => userIds.Contains(o.UserId)).ToList();
                    if (user.Count == 0)
                    {
                        return false;
                    }
                    _context.RemoveRange(role);
                    _context.RemoveRange(user);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User Get(int id)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    return _context.Users.FirstOrDefault(o => o.Id == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetByEmailAddress(string emailAddress)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    return _context.Users.FirstOrDefault(o => o.Email == emailAddress);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User Login(string username, string password)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    var user = _context.Users.FirstOrDefault(o => o.Username == username && o.Password == password);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToFile(ex.Message);
                return null;
            }
        }

        public List<User> List()
        {
            List<User> users = new List<User>();
            try
            {
                using (var _context = Db.Create())
                {
                    users = _context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return users;
        }

        public DataSet List(int pageNo)
        {
            DataSet ds = new DataSet("Users");
            try
            {
                using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("SP_GetUsersList", conn);
                    sqlComm.Parameters.AddWithValue("@PageNumber", pageNo);

                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> GetUsersByStatus(int userStatus)
        {
            List<User> users = new List<User>();
            try
            {
                using (var _context = Db.Create())
                {
                    users = _context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return users;

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
        public List<UserInRole> GetAllRolesList(List<int> roleIds)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.UserInRoles.Where(r => roleIds.Contains(r.RoleId)).ToList();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public List<RolePermission> GetRolePermissionsById(int roleId)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.RolePermissions.Where(p => p.RoleId == roleId).ToList();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public List<MenuItem> GetAllMenus()
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.MenuItems.ToList();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public List<SubMenuItem> GetAllSubMenus()
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.SubMenuItems.ToList();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public List<Permission> GetAllPermissions()
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.Permissions.ToList();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public List<Scope> GetAllScope()
        {
            try
            {
                using (var _context = Db.Create())
                {
                    return _context.Scopes.ToList();
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public UserInRole GetUserRole(int userId)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    var userRole = _context.UserInRoles.Include(x => x.Role).FirstOrDefault(o => o.UserId == userId);

                    if (userRole != null && userRole.Role != null && userRole.Id > 0)
                    {
                        foreach (var child in userRole.Role.UserInRoles.ToList())
                            userRole.Role.UserInRoles.Remove(child);
                    }
                    return userRole;
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.Message);
                return null;
            }
        }
        public bool CheckEmailExists(string email, int userId)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    if (userId > 0)
                        return _context.Users.Any(o => o.Email == email && o.Id != userId);
                    else
                        return _context.Users.Any(o => o.Email == email);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckUsernameExists(string username, int userId)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    if (userId > 0)
                        return _context.Users.Any(o => o.Username.ToLower() == username.Trim().ToLower() && o.Id != userId);
                    else
                        return _context.Users.Any(o => o.Username.ToLower() == username.Trim().ToLower());
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
