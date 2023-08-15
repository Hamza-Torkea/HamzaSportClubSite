using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class EmployeeRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();

        public List<Employee> GetAllEmployees()
        {
            return db.Employees.ToList();
        }
        public void Add(Employee e)
        {
            db.Employees.Add(e);
        }
        public void Delete(Employee e)
        {
            db.Employees.Remove(e);
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}