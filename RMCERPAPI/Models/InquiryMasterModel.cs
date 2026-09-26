using System;
using System.Collections.Generic;

namespace RMCERPAPI.Models
{
    public class InquiryMasterModel
    {
        public InquiryMasterModel()
        {
            InquiryID = 0;
            InquiryNo = "";

            BranchID = "";
            OnAcID = "";

            Date = DateTime.Now;

            InquiryTypeID = "";

            PartyName = "";
            Address = "";
            Area = "";
            State = "";

            ContactName = "";
            Mobile = "";
            Mobile2 = "";

            EmailID = "";
            EmailID2 = "";

            ReferenceBy = "";
            Currency = "";

           
            SendMail = false;

            GUIDs = "";
            Remarks = "";

            Status = "";
            MKTBy = "";
            CUID = "";

            CDT = "";
            MUID = "";
            MDT = "";

            InquiryItemList = new List<InquiryItemModel>();
        }

        // ==============================
        // INQUIRY MASTER
        // ==============================

        public decimal InquiryID { get; set; }

        public string InquiryNo { get; set; }

        public string BranchID { get; set; }

        public string OnAcID { get; set; }

        public DateTime? Date { get; set; }

        public string InquiryTypeID { get; set; }

        public string PartyName { get; set; }

        public string Address { get; set; }

        public string Area { get; set; }

        public string State { get; set; }

        public string ContactName { get; set; }

        public string Mobile { get; set; }

        public string Mobile2 { get; set; }

        public string EmailID { get; set; }

        public string EmailID2 { get; set; }

        public string ReferenceBy { get; set; }

        public string Currency { get; set; }
               
        public bool SendMail { get; set; }

        public string GUIDs { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }

        public string MKTBy { get; set; }

        public string CUID { get; set; }

        public string CDT { get; set; }

        public string MUID { get; set; }

        public string MDT { get; set; }

        public string StatusName { get; set; }


        // ==============================
        // INQUIRY ITEMS
        // ==============================

        public List<InquiryItemModel> InquiryItemList { get; set; }
    }


    public class InquiryItemModel
    {
        public InquiryItemModel()
        {
            InquiryDtlID = 0;
            InquiryID = 0;
            SeqNo = 1;

            ItemID = "";
            Description = "";
            UOMID = "";

            Qty = 0;

            Remarks = "";
        }

        public decimal InquiryDtlID { get; set; }

        public decimal InquiryID { get; set; }

        public int SeqNo { get; set; }

        public string ItemID { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public string UOMID { get; set; }

        public string UOMName { get; set; }

        public decimal Qty { get; set; }

        public string Remarks { get; set; }
    }
}