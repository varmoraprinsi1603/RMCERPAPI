using System;

namespace RMCERPAPI.Models
{
    public class UserMasterModel
    {
        public UserMasterModel()
        {
            UserID = 0;
            UserName = "";
            Password = "";
            Name = "";
            EmailID = "";
            Mobile = "";
            RoleID = 0;
            RoleName = "";
            IsActive = true;
        }

        public decimal UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Mobile { get; set; }
        public decimal RoleID { get; set; } 
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}