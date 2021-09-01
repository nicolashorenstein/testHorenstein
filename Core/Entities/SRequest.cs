using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("services_requests")]
    public class SRequest
    {
        [Key]
        public Guid id { get; set; }
        public string buildingCode { get; set; }
        public string description { get; set; }
        public int currentStatus { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string lastModifiedBy { get; set; }
        public DateTime? lastModifiedDate { get; set; }



        //Created for an unit test example
        [NotMapped]
        public double price { get; set; }

        public void SetPrice(double price)
        {
            this.price = price;
        }
        


    }
}
