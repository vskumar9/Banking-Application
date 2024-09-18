using BankApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.DTO
{
    public class ComplaintDTO
    {
        public string? CustomerId { get; set; }

        public int? ComplaintTypeId { get; set; }

        public DateTime? ComplaintDate { get; set; }

        public string? ComplaintDescription { get; set; }

        [FromForm(Name = "file")]
        public IFormFile? File { get; set; }

        public string? ComplaintStatus { get; set; }

        public string? EmployeeId { get; set; }
            

      
    }
}
