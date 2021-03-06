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
        public ContactU Add(ContactU contact)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Add(contact);
                    _context.SaveChanges();
                }

                return contact;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ContactU Update(ContactU contact)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Update(contact);
                    _context.SaveChanges();
                }

                return contact;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(List<int> contactIds)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    var contact = _context.ContactUs.Where(o => contactIds.Contains(o.Id)).ToList();
                    _context.RemoveRange(contact);
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

                    return _context.ContactUs.FirstOrDefault(o => o.Id == id && o.IsActive == true);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ContactU> List()
        {
            List<ContactU> contacts = new List<ContactU>();
            try
            {
                using (var _context = Db.Create())
                {
                    contacts = _context.ContactUs.Where(o => o.IsActive== true).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return contacts;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
