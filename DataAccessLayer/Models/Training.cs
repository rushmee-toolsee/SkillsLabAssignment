using System;

namespace DataAccessLayer.Models
{
    public class Training
    {
        public int TrainingId { get; set; }
        public string Title { get; set; }
        public string PreRequisites {  get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int SeatThreshold { get; set; }
        public DateTime TrainingDate { get; set; }
        public int PriorityDept { get; set; }


    }
}