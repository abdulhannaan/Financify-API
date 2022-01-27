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
    public class JoinUsRepository : IDisposable
    {
        public JoinU Add(JoinU join)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Add(join);
                    _context.SaveChanges();
                }

                return join;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JoinU Update(JoinU join)
        {
            try
            {
                using (var _context = Db.Create())
                {
                    _context.Update(join);
                    _context.SaveChanges();
                }

                return join;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(List<int> joinIds)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    var join = _context.JoinUs.Where(o => joinIds.Contains(o.Id)).ToList();
                    _context.RemoveRange(join);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public JoinU Get(int id)
        {
            try
            {
                using (var _context = Db.Create())
                {

                    return _context.JoinUs.FirstOrDefault(o => o.Id == id && o.IsActive == true);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<JoinU> List()
        {
            List<JoinU> joins = new List<JoinU>();
            try
            {
                using (var _context = Db.Create())
                {
                    joins = _context.JoinUs.Where(o => o.IsActive== true).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return joins;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
