using System;

namespace Contract_Monthly_Claim_System_POE.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public DateTime Date { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; } // Nullable
        public string Status { get; set; } // "Pending", "Approved", "Rejected"
        public string? SupportingDocumentPath { get; set; } // Nullable
        public string? RejectionReason { get; set; } // Nullable





        public Lecturer Lecturer { get; set; } // Foreign Key relationship

        // Navigation property for the approval information
        public Approval Approval { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
