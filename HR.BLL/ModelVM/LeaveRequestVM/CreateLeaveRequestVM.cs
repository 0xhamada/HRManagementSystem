using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HR.BLL.ModelVM.LeaveRequestVM
{
    public class CreateLeaveRequestVM
    {
        
        [Required(ErrorMessage = "Please select a leave type")]
        public int LeaveTypeId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get;  set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get;  set; }
        [Required(ErrorMessage = "Please provide a reason")]
        [StringLength(500, MinimumLength = 5)]
        public string Reason { get;  set; } = null!;
    }
}
