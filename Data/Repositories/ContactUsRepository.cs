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
    public class ContactUsRepository : IDisposable
    {
        public ContactU Add(ContactU user)
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

        public ContactU Update(ContactU user)
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

                    var user = _context.ContactUs.Where(o => userIds.Contains(o.Id)).ToList();
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

        public ContactU Get(int id)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    return _context.ContactUs.FirstOrDefault(o => o.Id == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ContactU> List()
        {
            List<ContactU> users = new List<ContactU>();
            try
            {
                using (var _context = Db.Create())
                {
                    users = _context.ContactUs.ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return users;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
