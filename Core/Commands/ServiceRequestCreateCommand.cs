using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands
{
    public class ServiceRequestCreateCommand
    {
        public string buildingCode { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }

    }
}
