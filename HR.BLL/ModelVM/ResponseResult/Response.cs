using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.ModelVM.ResponseResult
{
    public record Response<T>(T result, string errormessage, bool IsHaveErrorOrNo);
    
}
