using AutoMapper;
using HR.BLL.ModelVM.LeaveRequestVM;
using HR.BLL.ModelVM.ResponseResult;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using HR.DAL.Enum;
using HR.DAL.Repo.Abstraction;
using HR.DAL.Repo.Impelementation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HR.BLL.Service.Impelementation
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepo RequestRepo;
        private readonly ILeaveTypeRepo TypeRepo;

        public LeaveRequestService(ILeaveRequestRepo RequestRepo, IMapper mapper, ILeaveTypeRepo TypeRepo)
        {
            this.RequestRepo = RequestRepo;
            this.mapper = mapper;
            this.TypeRepo = TypeRepo;
        }
        public Response<bool> CreateRequest(string employeeId , CreateLeaveRequestVM vm)
        {
            try
            {
                if(vm.EndDate < vm.StartDate)
                {
                    return new Response<bool>(false, "End date cannot be before start date", true);
                }

                var leavetybe = TypeRepo.GetById(vm.LeaveTypeId);

                if (leavetybe == null)
                    return new Response<bool>(false, "Invalid leave type", true);
                // We Mapped Manully Cause The User Can Open DivTools And Change Id So We Get THe Id From ClaimTypes.NameIdentifier
                // var mappedRequest = mapper.Map<LeaveRequest>(vm);
                var mappedRequest = new LeaveRequest(employeeId, vm.LeaveTypeId, vm.StartDate, vm.EndDate, vm.Reason);
                var result = RequestRepo.AddRequest(mappedRequest);
                return new Response<bool>(result, result ? null! : "Failed TO Create Request", !result);
            }
            
            catch(Exception ex) 
            {
                return new Response<bool>(false, ex.Message,true);

            }
        }
        public Response<bool> Approve(int requestId) => ChangeStatus(requestId, approve: true);
        

        public Response<List<LeaveTypeVM>> GetAllLeaveTypes()
        {
            try
            {
                var types = TypeRepo.GetAll();
                var mappedType = mapper.Map<List<LeaveTypeVM>>(types);
                return new Response<List<LeaveTypeVM>>(mappedType, null!, false);

            }

            catch (Exception ex)
            {
                return new Response<List<LeaveTypeVM>>(null!, ex.Message, true);
            }
        }

        public Response<List<LeaveRequestListVM>> GetMyRequests(string employeeId)
        {
            try
            {
                var request = RequestRepo.GetAll(a => a.EmployeeId == employeeId);
                var mappedRequests = MapToListVM(request);
                //var mappedRequests = mapper.Map<List<LeaveRequestListVM>>(request);
                return new Response<List<LeaveRequestListVM>>(mappedRequests, null!, false);
            }

            catch (Exception ex)
            {
                return new Response<List<LeaveRequestListVM>>(null!,ex.Message, true);
            }
        }

        public Response<List<LeaveRequestListVM>> GetPendingRequests()
        {   // Why Did Not Use Mapper ?????
            //because EmployeeName/LeaveTypeName come from navigation properties, and Status needs .ToString() conversion
            try
            {
                var request = RequestRepo.GetAll(a => a.Status == LeaveStatus.Pending);
                var mappedRequests = MapToListVM(request);
               // var mappedRequests = mapper.Map<List<LeaveRequestListVM>>(request);
                return new Response<List<LeaveRequestListVM>>(mappedRequests, null!, false);
            }

            catch (Exception ex)
            {
                return new Response<List<LeaveRequestListVM>>(null!, ex.Message, true);
            }
        }

        public Response<bool> Reject(int requestId) => ChangeStatus(requestId, approve: false);

        private Response<bool> ChangeStatus(int requestId, bool approve)
        {
            try
            {
                var request = RequestRepo.GetRequestById(requestId);
                if(request == null)
                {
                    return new Response<bool>(false, "Request not found", true);
                }
                if (request.Status != LeaveStatus.Pending)
                {
                    return new Response<bool>(false, $"Request is already {request.Status} and cannot be changed", true);
                }
                if(approve)
                {
                    request.Approve();
                }
                else
                    request.Reject();
                var result = RequestRepo.UpdateRequest(request);
                return new Response<bool>(result, result ? null! : "Failed To Update", !result);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }
        private List<LeaveRequestListVM> MapToListVM(List<LeaveRequest> requests)
        {
            return requests.Select(r => new LeaveRequestListVM
            {
                Id = r.Id,
                EmployeeName = r.employee?.Name ?? "Unknown",
                LeaveTypeName = r.leavetype?.Name ?? "Unknown",
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Reason = r.Reason,
                Status = r.Status.ToString(),
                RequestedAt = r.RequestedAt
            }).ToList();
        }
    }
}
