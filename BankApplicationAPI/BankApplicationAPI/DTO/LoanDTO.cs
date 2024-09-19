using BankApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.DTO
{
    public class LoanDTO
    {
        
        public string? CustomerId { get; set; }

        public int? LoanTypeId { get; set; }

        public decimal? LoanAmount { get; set; }

        public DateTime? ApplicationDate { get; set; }

        [FromForm(Name = "file")]
        public IFormFile? File { get; set; }

        public string? EmployeeId { get; set; }

        public string? Comments { get; set; }
    }
}
