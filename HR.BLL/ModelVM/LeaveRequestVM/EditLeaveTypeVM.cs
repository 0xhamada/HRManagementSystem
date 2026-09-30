using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HR.BLL.ModelVM.LeaveRequestVM
{
    public class EditLeaveTypeVM
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Leave type name is required")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;
    }
}
