using ContractMonthlyClaimsSystem.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractMonthlyClaimsSystem.Models
{
    public class Claims
    {
        public int Id { get; set; }
        [Required]
        public string? LecturerID { get; set; }
        [ForeignKey("LecturerID")]
        public ApplicationUser? Lecturer { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime ClaimPeriodsStart { get; set; }
        [Required]
        public DateTime ClaimPeriodEnd { get; set; }
        [Required]
        public double HoursWorked { get; set; }
        [Required]
        public double RatePerHour { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public string? DescriptionOfWork { get; set; }

        
        public string? FilePath { get; set; }
        [NotMapped]
        public IFormFile Upload { get; set; }
        public string StatusProg { get; set; } = "Pending";
        public string StatusAcademic { get; set; } = "Pending";
        public string? ClaimStatus { get; set; } = "Pending";
    }
}
