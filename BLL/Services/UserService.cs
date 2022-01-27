using BLL.Dtos;
using Utility.Commons;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Utility.Enumerations;
using AHDBLL.Dtos.Request;
using System.Data;

namespace BLL.Services
{
    public class UserService
    {
        public ApiResponseMessage SignUp(UserDto user)
        {
            User newUser = new User();
            var response = new ApiResponseMessage();
            response.Status = HttpStatusCode.OK;

            var encryptedPw = Encryption.Encrypt(user.Password);
            newUser.Password = encryptedPw;
            newUser.Username = user.Username;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.CreatedOn = DateTime.Now;


            User result = new UserRepository().Add(newUser);
            if (result.Id > 0)
            {
                response.Message = "User added successfully";
                response.Response = result;
            }
            else
            {
                response.Message = "Failed, an error has occured.";
                response.Response = "";
            }
            return response;
        }

        public User Login(string username, string password)
        {
            try
            {
                var encryptedPw = Encryption.Encrypt(password);
                using (var userData = new UserRepository())
                {
                    User login = userData.Login(username, encryptedPw);
                    return login;
                    #region UserRoleImplimentationNotNeeded
                    //if (login != null && login.Id > 0)
                    //{
                    //    var userRoles = userData.GetUserRoles(login.Id);
                    //    var userRoleIds = userRoles.Select(p => p.RoleId).ToList();
                    //    List<MenuDto> menuList = new List<MenuDto>();

                    //    var roles = userData.GetAllRolesList(userRoleIds);

                    //    var responseParentMenus = userData.GetAllMenus();
                    //    var responseSubMenus = userData.GetAllSubMenus();
                    //    var responsePermission = userData.GetAllPermissions();

                    //    if (roles != null && roles.Count > 0)
                    //    {
                    //        foreach (var role in roles)
                    //        {
                    //            var responseMenusPermissions = userData.GetRolePermissionsById(role.RoleId);
                    //            foreach (var item in responseParentMenus)
                    //            {
                    //                var existingMenu = menuList.Where(p => p.MenuId == item.Id).FirstOrDefault();
                    //                if (existingMenu == null)
                    //                {
                    //                    MenuDto menu = new MenuDto();
                    //                    menu.MenuId = item.Id;
                    //                    menu.MenuName = item.Name;
                    //                    menu.MenuUrl = item.Url;
                    //                    menu.MenuIcon = item.Icon;
                    //                    menu.SortOrder = (int)item.SortOrder;

                    //                    menu.SubMenuEnum = "";
                    //                    menu.MenuPermissions = new List<MenuPermissions>();
                    //                    if (responseMenusPermissions != null)
                    //                    {
                    //                        if (responseMenusPermissions.Count > 0 && responseMenusPermissions != null)
                    //                        {
                    //                            foreach (var per in responsePermission)
                    //                            {
                    //                                MenuPermissions MP = new MenuPermissions();
                    //                                var responseMenu = responseMenusPermissions.Where(r => r.PermissionId == per.Id && r.RoleId == role.RoleId && r.MenuId == menu.MenuId).FirstOrDefault();
                    //                                if (responseMenu != null)
                    //                                {
                    //                                    MP.IsAllow = true;
                    //                                }
                    //                                MP.PermissionId = per.Id;
                    //                                MP.PermissionEnumId = (RolePermissions)per.PermissionEnumVal;

                    //                                menu.MenuPermissions.Add(MP);
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            foreach (var per in responsePermission)
                    //                            {
                    //                                MenuPermissions MP = new MenuPermissions();
                    //                                menu.MenuPermissions.Add(MP);
                    //                            }
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        foreach (var per in responsePermission)
                    //                        {
                    //                            MenuPermissions MP = new MenuPermissions();
                    //                            menu.MenuPermissions.Add(MP);
                    //                        }
                    //                    }

                    //                    if (menu.MenuPermissions.Any(p => p.IsAllow == true && p.PermissionEnumId == RolePermissions.View))
                    //                    {
                    //                        //get submenus for this parent menu
                    //                        var subMenusForThisParentMenu = responseSubMenus.Where(p => Convert.ToInt32(p.MenuId) == item.Id).OrderBy(x => x.SortOrder).ToList();
                    //                        menu.SubMenus = new List<MenuDto>();
                    //                        foreach (var submenu in subMenusForThisParentMenu)
                    //                        {
                    //                            MenuDto sm = new MenuDto();
                    //                            sm.MenuId = submenu.Id;
                    //                            sm.MenuName = submenu.Name;
                    //                            sm.MenuIcon = submenu.Icon;
                    //                            sm.MenuUrl = submenu.Url;
                    //                            sm.SortOrder = (int)submenu.SortOrder;
                    //                            sm.SubMenuEnum = "";// submenu.SubMenuEnumVal != null ? Convert.ToString(EnumDescription.GetDescription((SubMenuEnum)item.submenu.Value)) : " - ";
                    //                            sm.MenuPermissions = new List<MenuPermissions>();
                    //                            if (responseMenusPermissions != null)
                    //                            {
                    //                                if (responseMenusPermissions.Count > 0 && responseMenusPermissions != null)
                    //                                {
                    //                                    foreach (var per in responsePermission)
                    //                                    {
                    //                                        MenuPermissions MP = new MenuPermissions();
                    //                                        var responseMenu = responseMenusPermissions.ToList().Where(r => r.PermissionId == per.Id && r.RoleId == role.RoleId
                    //                                        && r.MenuId == item.Id && r.SubMenuId == submenu.Id).FirstOrDefault();
                    //                                        if (responseMenu != null)
                    //                                        {
                    //                                            MP.IsAllow = true;
                    //                                        }
                    //                                        MP.PermissionId = per.Id;
                    //                                        MP.PermissionEnumId = (RolePermissions)per.PermissionEnumVal;
                    //                                        sm.MenuPermissions.Add(MP);
                    //                                    }
                    //                                    menu.SubMenus.Add(sm);
                    //                                }
                    //                                else
                    //                                {
                    //                                    foreach (var per in responsePermission)
                    //                                    {
                    //                                        MenuPermissions MP = new MenuPermissions();
                    //                                        sm.MenuPermissions.Add(MP);
                    //                                    }
                    //                                    menu.SubMenus.Add(sm);
                    //                                }
                    //                            }
                    //                            else
                    //                            {
                    //                                foreach (var per in responsePermission)
                    //                                {
                    //                                    MenuPermissions MP = new MenuPermissions();
                    //                                    sm.MenuPermissions.Add(MP);
                    //                                }
                    //                                menu.SubMenus.Add(sm);
                    //                            }
                    //                        }
                    //                    }

                    //                    menuList.Add(menu);
                    //                }
                    //                else
                    //                {
                    //                    if (responseMenusPermissions != null)
                    //                    {
                    //                        if (responseMenusPermissions.Count > 0 && responseMenusPermissions != null)
                    //                        {
                    //                            foreach (var per in responsePermission)
                    //                            {
                    //                                MenuPermissions MP = new MenuPermissions();
                    //                                var responseMenu = responseMenusPermissions.Where(r => r.PermissionId == per.Id && r.RoleId == role.RoleId && r.MenuId == existingMenu.MenuId).FirstOrDefault();
                    //                                if (responseMenu != null)
                    //                                {
                    //                                    MP.IsAllow = true;
                    //                                }
                    //                                MP.PermissionId = per.Id;
                    //                                MP.PermissionEnumId = (RolePermissions)per.PermissionEnumVal;

                    //                                if (MP.IsAllow)
                    //                                {
                    //                                    var existingMenuPermission = existingMenu.MenuPermissions.Where(p => p.PermissionId == MP.PermissionId && p.IsAllow == false).FirstOrDefault();
                    //                                    if (existingMenuPermission != null)
                    //                                    {
                    //                                        existingMenuPermission.IsAllow = MP.IsAllow;
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                    }

                    //                    if (existingMenu.MenuPermissions.Any(p => p.IsAllow == true && p.PermissionEnumId == RolePermissions.View))
                    //                    {
                    //                        //get submenus for this parent menu
                    //                        var subMenusForThisParentMenu = responseSubMenus.Where(p => Convert.ToInt32(p.MenuId) == item.Id).ToList();
                    //                        //existingMenu.SubMenus = new List<MenuVM>();
                    //                        var existingSubMenus = existingMenu.SubMenus;
                    //                        foreach (var submenu in subMenusForThisParentMenu)
                    //                        {
                    //                            var existingSubMenu = existingSubMenus.Where(p => p.MenuId == submenu.Id).FirstOrDefault();

                    //                            MenuDto sm = new MenuDto();
                    //                            sm.MenuId = submenu.Id;
                    //                            sm.MenuName = submenu.Name;
                    //                            sm.MenuUrl = submenu.Url;
                    //                            sm.MenuIcon = submenu.Icon;
                    //                            sm.SortOrder = (int)submenu.SortOrder;
                    //                            sm.SubMenuEnum = "";// submenu.SubMenuEnumVal != null ? Convert.ToString(EnumDescription.GetDescription((SubMenuEnum)item.submenu.Value)) : " - ";
                    //                            sm.MenuPermissions = new List<MenuPermissions>();
                    //                            if (responseMenusPermissions != null)
                    //                            {
                    //                                if (responseMenusPermissions.Count > 0 && responseMenusPermissions != null)
                    //                                {
                    //                                    foreach (var per in responsePermission)
                    //                                    {
                    //                                        MenuPermissions MP = new MenuPermissions();
                    //                                        var responseMenu = responseMenusPermissions.Where(r => r.PermissionId == per.Id && r.RoleId == role.RoleId
                    //                                        && r.MenuId == item.Id && r.SubMenuId == submenu.Id).FirstOrDefault();
                    //                                        if (responseMenu != null)
                    //                                        {
                    //                                            MP.IsAllow = true;
                    //                                        }
                    //                                        MP.PermissionId = per.Id;
                    //                                        MP.PermissionEnumId = (RolePermissions)per.PermissionEnumVal;

                    //                                        if (MP.IsAllow && existingSubMenu != null)
                    //                                        {
                    //                                            var existingMenuPermission = existingSubMenu.MenuPermissions.Where(p => p.PermissionId == MP.PermissionId && p.IsAllow == false).FirstOrDefault();
                    //                                            if (existingMenuPermission != null)
                    //                                            {
                    //                                                existingMenuPermission.IsAllow = MP.IsAllow;
                    //                                            }
                    //                                        }
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }

                    //    var allMenuList = menuList.Where(p => p.MenuPermissions.Any(x => x.IsAllow == true)).OrderBy(p => p.SortOrder).ToList();

                    //    var loginRes = new LoginResponse();
                    //    loginRes.User = login;
                    //    loginRes.AllowedMenuList = allMenuList;
                    //    loginRes.AllowedMainMenuList = menuList.Where(p => p.MenuPermissions.Any(x => x.PermissionEnumId == RolePermissions.View && x.IsAllow == true)).OrderBy(p => p.SortOrder).ToList();

                    //    return loginRes;
                    //}
                    //else
                    //{
                    //    return null;
                    //} 
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> List()
        {
            try
            {
                using (var userData = new UserRepository())
                {
                    return userData.List();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<UserDto> List(UserListRequestDto req)
        {
            try
            {
                List<UserDto> users = new List<UserDto>();
                using (var userData = new UserRepository())
                {
                    var usersDataset =  userData.List(req.PageNo);
                    users = UsersDStoList(usersDataset);

                }
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ApiResponseMessage Save(UserDto user, int loggedInUserId)
        {
            var response = new ApiResponseMessage();

            try
            {
                User obj = new User();
                using (var userData = new UserRepository())
                {
                    if (user != null && user.Id > 0)
                    {
                        obj = userData.Get(user.Id);
                    }

                    obj.FirstName = user.FirstName;
                    obj.LastName = user.LastName;
                    obj.DateOfBirth = user.DateOfBirth;
                    obj.Email = user.Email;
                    obj.Phone = user.Phone;
                    obj.CountryId = user.CountryId;
                    obj.State = user.State;
                    obj.City = user.City;
                    obj.Address = user.Address;
                    obj.Zip = user.Zip;
                    
                    if (user.Id > 0)
                    {
                        obj.UpdatedOn = DateTime.Now;
                        obj.UpdatedBy = loggedInUserId;
                        userData.Update(obj);
                    }
                    else
                    {
                        obj.Username = user.Username;
                        obj.Password = user.Password;
                        obj.CreatedBy = loggedInUserId;
                        obj.CreatedOn = DateTime.Now;
                        userData.Add(obj);
                    }

                }
                response.Status = HttpStatusCode.OK;
                response.Message = "User saved successfully!";
                response.Response = obj;

            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "An Error has occured!";
                response.Response = false;
            }
            return response;
        }

        public User Get(int Id)
        {
            try
            {
                using (var userData = new UserRepository())
                {
                    return userData.Get(Id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Delete(string ids)
        {
            try
            {
                using (var userRepository = new UserRepository())
                {
                    var listIds = ids.Split(',').Select(Int32.Parse).ToList();
                    return userRepository.Delete(listIds);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<User> GetPending()
        {
            try
            {
                using (var userData = new UserRepository())
                {
                    return userData.GetUsersByStatus(Convert.ToInt32(UserStatus.Pending));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> GetApproved()
        {
            try
            {
                using (var userData = new UserRepository())
                {
                    return userData.GetUsersByStatus(Convert.ToInt32(UserStatus.Approved));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> GetRejected()
        {
            try
            {
                using (var userData = new UserRepository())
                {
                    return userData.GetUsersByStatus(Convert.ToInt32(UserStatus.Rejected));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateStatus(string Ids, int statusId,int companyId)
        {
            try
            {
                using (var userData = new UserRepository())
                {
                    var ids = Ids.Split(',').Select(Int32.Parse).ToList();
                    foreach (var id in ids)
                    {
                        var user = userData.Get(id);
                        userData.Update(user);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string ForgotPassword(string emailAddress, MailSettingsDto mailSettings,int loggedInUserId)
        {

            try
            {

                MailRequestDto mailRequest = new MailRequestDto();
                User user = new User();
                string newPassword = Helper.GeneratePassword();

                using (var userData = new UserRepository())
                {
                    user = userData.GetByEmailAddress(emailAddress);

                    if (user != null && user.Id > 0)
                    {
                        user.Password = newPassword;
                        user.UpdatedBy = loggedInUserId;
                        user.UpdatedOn = DateTime.Now;
                        userData.Update(user);

                        mailRequest.ToEmail = user.Email;
                        mailRequest.HtmlAllowed = true;
                        mailRequest.Subject = "Agile Help Desk - Password Changed";
                        mailRequest.Body = "You agile help desk account password has been updated to: <h4>" + newPassword + "</h4> . Please use this password to login to the system and change it.";
                    }
                    
                }
                if (!string.IsNullOrEmpty(mailRequest.ToEmail)) 
                {
                    try
                    {
                        new MailService(mailSettings).Send(mailRequest);
                        return "You updated password has been sent on email: " + emailAddress + ". Please login with the new password and change it.";

                    }
                    catch (Exception ex)
                    {
                        return "Something went wrong, please try again later.";

                    }
                }
                else
                {
                    return "No user found for "+emailAddress+".";
                }

            }
            catch (Exception ex)
            {
                return "Something went wrong, please try again later.";
            }
        }

        public string ChangePassword(PasswordDto passwordDto,int loggedInUserId)
        {
            User user = new User();
            try
            {
                using (var userData = new UserRepository())
                {
                    user = userData.Get(passwordDto.UserId);

                    if (user != null && user.Id > 0)
                    {
                        var passwordFromDb = Encryption.Decrypt(user.Password);
                        var validationMessage = this.ValidatePassword(passwordFromDb, passwordDto);
                        if (validationMessage != "valid")
                            return validationMessage;
                        else
                        {
                            user.Password = Encryption.Encrypt(passwordDto.Password);
                            user.UpdatedBy = loggedInUserId;
                            user.UpdatedOn = DateTime.Now;
                            userData.Update(user);

                            return "Password changed.";
                        }
                    }
                    else
                    {
                        return "Invalid User.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Failed to update password. Please try again later.";
            }
        }

        private string ValidatePassword(string passwordDb, PasswordDto password)
        {
            if (passwordDb != password.OldPassword)
                return "The Old Password you entered is invalid.";
            else if (password.Password != password.ConfirmPassword)
                return "Passwords are not matching.";
            else
                return "valid";
        }

        private List<UserDto> UsersDStoList(DataSet ds)
        {
            List<UserDto> users = new List<UserDto>();
            var table = ds.Tables[0];
            if (table != null && table.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var obj = new UserDto();
                        obj.Id = int.Parse(row["Id"] + "");
                        obj.Username = row["Username"] + "";
                        obj.FirstName = row["FirstName"] + "";
                        obj.LastName = row["LastName"] + "";

                        if (!string.IsNullOrEmpty(row["DateOfBirth"] + ""))
                        {
                            var dbo = DateTime.Parse(row["DateOfBirth"] + "");
                            obj.DateOfBirthStr = dbo.ToString("dd-MMMM-yyyy hh:mm tt");
                        }

                        obj.Email = row["Email"] + "";
                        obj.Phone = row["Phone"] + "";
                        obj.CountryId = int.Parse(row["CountryId"] + "");
                        obj.CountryTitle = row["CountryTitle"] + "";
                        obj.State = row["State"] + "";
                        obj.City = row["City"] + "";
                        obj.Address = row["Address"] + "";
                        obj.Zip = row["Zip"] + "";
                        obj.CompanyId = int.Parse(row["CompanyId"] + "");
                        obj.CompanyName = row["CompanyName"] + "";
                        obj.Status = int.Parse(row["Status"] + "");
                        obj.StatusTitle = row["StatusTitle"] + "";

                        if (!string.IsNullOrEmpty(row["CreatedOn"] + ""))
                        {
                            var createdDateTime = DateTime.Parse(row["CreatedOn"] + "");
                            obj.CreatedOnStr = createdDateTime.ToString("dd-MMMM-yyyy hh:mm tt");
                        }
                        if (!string.IsNullOrEmpty(row["UpdatedOn"] + ""))
                        {
                            var updatedDateTime = DateTime.Parse(row["UpdatedOn"] + "");
                            obj.UpdatedOnStr = updatedDateTime.ToString("dd-MMMM-yyyy hh:mm tt");
                        }

                        users.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return users;
        }

    }
}
