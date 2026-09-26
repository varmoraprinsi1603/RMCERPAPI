using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMCERPAPI.Models
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            CustomerID = 0;
            CustomerName = "";
            MobileNo = "";
            Email = "";
            Address = "";
            City = "";
            CreatedDate = null;
            IsActive = false;
        }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}