using System;
using System.ComponentModel.DataAnnotations;

namespace asp_xamar_solution.Models
{
    public class TransactionsHistoryModel
    {
        [Key]
        public int Id { get; set; }
        public string UserEmail { get; set; } //The email of the host for the History
        public DateTime trDate { get; set; }
        public string CorrespondentName { get; set; } // Should contain Username of Correspondent
        public string CorrespondentEmail { get; set; } // Should contain Email of Correspondent
        public decimal trAmount { get; set; }
        public decimal RestBalance { get; set; }
    }
}
