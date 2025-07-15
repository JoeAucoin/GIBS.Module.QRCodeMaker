using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.QRCodeMaker.Models
{
    [Table("GIBSQRCodeMaker")]
    public class QRCodeMaker : IAuditable
    {
        [Key]
        public int QRCodeMakerId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        // Type of QR Code
        //(e.g., 'URL', 'TEXT', 'CONTACT', 'WIFI', 'EMAIL', 'SMS', 'LOCATION', 'EVENT', 'PAYMENT', 'V_CARD', etc.)
        public string QR_CodeType { get; set; }
        public string ContentData { get; set; }
        public string Notes { get; set; }
        public string QR_Color { get; set; }
        public string QR_BackgroundColor { get; set; }
        public string QR_Logo { get; set; }
        public string ImageURL { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
