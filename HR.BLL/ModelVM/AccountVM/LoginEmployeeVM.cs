using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HR.BLL.ModelVM.AccountVM
{
    public class LoginEmployeeVM
    {
        [Required]
        public string UserName { get;  set; }
        [Required]

        public string PassWord { get;  set; }
    }
}
