//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RMCERPAPI.Models
//{
//    public class PropMasterModel
//    {
//        public PropMasterModel()
//        {
//            PropID = 0;
//            PropTypeName = "";
//            PropName = "";
//            PropValue = "";
//            Status = "";
//            CUID = 0;
//        }
//        [Display(Name = "PropID")]
//        public Decimal PropID { get; set; }

//        [Required(ErrorMessage = "PropTypeName Required")]
//        [Display(Name = "PropTypeName")]
//        public String PropTypeName { get; set; }

//        public String PropTypeName1 { get; set; }

//        public SelectList PropTypeNameSelectList { get; set; }

//        [Required(ErrorMessage = "PropName Required")]
//        [Display(Name = "PropName")]
//        public String PropName { get; set; }

//        [Required(ErrorMessage = "PropValue Required")]
//        [Display(Name = "PropValue")]
//        public String PropValue { get; set; }

//        [Required(ErrorMessage = "Status Required")]
//        [Display(Name = "Status")]
//        public String Status { get; set; }

//        [Display(Name = "Entry By")]
//        public Decimal CUID { get; set; }



//        public SelectList StatusSelectList { get; set; }
//        public String StatusName { get; set; }

//        public String UserName { get; set; }
//        public SelectList UserIDSelectList { get; set; }
//    }

//}


using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMCERPAPI.Models
{
    public class PropMasterModel
    {
        public PropMasterModel()
        {
            PropID = 0;
            PropTypeName = "";
            PropName = "";
            PropValue = "";
            Status = "";
            CUID = 0;
        }

        public Decimal PropID { get; set; }
        public String PropTypeName { get; set; }
        public String PropTypeName1 { get; set; }
        public String PropName { get; set; }
        public String PropValue { get; set; }
        public String Status { get; set; }
        public Decimal CUID { get; set; }

        public String StatusName { get; set; }

        public String UserName { get; set; }
    }
}

