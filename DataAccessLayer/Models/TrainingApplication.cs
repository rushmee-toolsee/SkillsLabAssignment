using System;

namespace DataAccessLayer.Models
{
    public class TrainingApplication
    {
        public string TrainingTitle {  get; set; }

        public int ApplicationId { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string ManagerApprovalStatus { get; set; }
        public string DeclineReason { get; set;}
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
        public string AttachmentPath {  get; set; }
    }
}