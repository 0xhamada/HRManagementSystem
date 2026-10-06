using AutoMapper;
using HR.BLL.ModelVM.ResignationVM;
using HR.BLL.ModelVM.ResponseResult;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using HR.DAL.Enum;
using HR.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Impelementation
{
    
    public class ResignationService : IResignationService
    {
        private readonly IResignationRepo resignationRepo;
        private readonly IMapper mapper;
        private readonly UserManager<Employee> userManager;
        public ResignationService(IResignationRepo resignationRepo, IMapper mapper, UserManager<Employee> userManager)
        {
            this.resignationRepo = resignationRepo;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<Response<bool>> Approve(int id) => await ChangeStatus(id, approve: true);

        public async Task<Response<bool>> Reject(int id) => await ChangeStatus(id, approve: false);


        private async Task<Response<bool>> ChangeStatus(int id , bool approve)
        {
            try
            {
                var request = resignationRepo.GetById(id);
                if (request == null)
                    return new Response<bool>(false, "failed to get resignation ", true);

                if (request.Status != ResignationStatus.Pending)
                {
                    return new Response<bool>(false, $"the request is aleardy ${request.Status} ", true);
                }
                if (approve)
                {
                    request.Approve();
                    var employee = request.Employee;
                    if(employee != null)
                    {
                        employee.ToggleStatus();
                        employee.LockoutEnabled = true;
                        await userManager.SetLockoutEndDateAsync(employee, DateTimeOffset.MaxValue);
                    }
                }
                else
                {
                    request.Reject();
                }
                var result = resignationRepo.Update(request);
                return new Response<bool>(result, result ? null! : "Failed to update resignation", !result);
            }

            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);

            }
        }
        public Response<List<ResignationListVM>> GetPending()
        {
            try
            {
                var pendingResignation = resignationRepo.GetAll(true);

                var mappedResignation = mapper.Map<List<ResignationListVM>>(pendingResignation);
                return new Response<List<ResignationListVM>> (mappedResignation, null! , false);
            }
            catch (Exception ex)
            {
                return new Response<List<ResignationListVM>>(null!, ex.Message, true);

            }
        }

        

        public Response<bool> Submit(string employeeId, CreateResignationVM vm)
        {
            try
            {
                var existingPending = resignationRepo.GetPendingByEmployee(employeeId);
                if (existingPending != null)
                    return new Response<bool>(false, "You already have a pending resignation request", true);

                //var mappedResignation = mapper.Map<Resignation>(vm);
                var mappedResignation = new Resignation(employeeId, vm.Reason, vm.LastWorkingDate);
                var result = resignationRepo.Add(mappedResignation);
                return new Response<bool>(result , result ? null! : "failed to add Resignation" , !result);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false ,ex.Message ,true);

            }
        }
    }
}
