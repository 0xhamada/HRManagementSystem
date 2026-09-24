using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HR.BLL.ModelVM.AccountVM
{
    public class RegisterEmployeeVM
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Range(18, 100)]
        public int Age { get; set; }

        public decimal Salary { get; set; }

        [Required]
        public int DeptId { get; set; }
    }
}
