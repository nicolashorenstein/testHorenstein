using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands
{
    public class ServiceRequestUpdateCommand
    {
        public string buildingCode { get; set; }
        public string description { get; set; }
        public int currentStatus { get; set; }
        public string createdBy { get; set; }
        public string lastModifiedBy { get; set; }
        public DateTime? lastModifiedDate { get; set; }
    }
}
