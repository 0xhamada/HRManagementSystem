using HR.BLL.ModelVM.ResignationVM;
using HR.BLL.ModelVM.ResponseResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Abstraction
{
    public interface IResignationService
    {
        Response<bool> Submit(string employeeId, CreateResignationVM vm);
        Response<List<ResignationListVM>> GetPending();
        Task<Response<bool>> Approve(int id);
        Task<Response<bool>> Reject(int id);
    }
}
