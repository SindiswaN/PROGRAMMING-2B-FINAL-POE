namespace Contract_Monthly_Claim_System_POE.Models
{
    public class Approval
    {
        public int ApprovalID { get; set; }
        public int ClaimID { get; set; }  // Foreign Key
        public string CoordinatorApprovalStatus { get; set; }
        public string ManagerApprovalStatus { get; set; }

        // Navigation property back to the claim
        public Claim Claim { get; set; }
    }
}
