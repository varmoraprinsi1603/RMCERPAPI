using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMCERPAPI.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            EmployeeID = 0;
            EmployeeName = "";
            DepartmentID = null;
            Salary = null;
            JoiningDate = null;
            IsActive = false;
        }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int? DepartmentID { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? JoiningDate { get; set; }
        public bool IsActive { get; set; }
    }
}