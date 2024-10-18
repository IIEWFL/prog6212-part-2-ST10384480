// Models/Claim.cs
namespace ST10384480CMCS.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string LecturerId { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal Rate { get; set; }
        public string AdditionalNotes { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime SubmissionDate { get; set; }
    }
}