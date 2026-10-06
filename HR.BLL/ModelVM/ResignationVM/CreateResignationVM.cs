using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HR.BLL.ModelVM.ResignationVM
{
    public class CreateResignationVM
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime LastWorkingDate { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Reason { get; set; } = null!;
    }
}
