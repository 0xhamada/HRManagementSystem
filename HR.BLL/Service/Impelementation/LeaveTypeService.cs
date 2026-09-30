using AutoMapper;
using HR.BLL.ModelVM.LeaveRequestVM;
using HR.BLL.ModelVM.ResponseResult;
using HR.BLL.Service.Abstraction;
using HR.DAL.DataBase;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Impelementation
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepo _leaveTypeRepo;
        private readonly IMapper _mapper;

        public LeaveTypeService(ILeaveTypeRepo leaveTypeRepo, IMapper mapper)
        {
            _leaveTypeRepo = leaveTypeRepo;
            _mapper = mapper;
        }

        public Response<bool> Create(CreateLeaveTypeVM vm)
        {
            try
            {
                // Prevent duplicate leave type names (e.g., two "Annual" entries)
                var existing = _leaveTypeRepo.GetAll()
                                             .FirstOrDefault(t => t.Name.Equals(vm.Name, StringComparison.OrdinalIgnoreCase));

                if (existing != null)
                    return new Response<bool>(false, "A leave type with this name already exists", true);
                var mappedType = _mapper.Map<LeaveType>(vm);
                var result = _leaveTypeRepo.Add(mappedType);
                return new Response<bool>(result, result ? null! : "Failed To Add Leave Type", !result);
            }
            catch (Exception ex) 
            {
                return new Response<bool>(false,ex.Message,true);
            }
        }

        public Response<bool> Delete(int id)
        {
            try
            {
                var result = _leaveTypeRepo.Delete(id);
                return new Response<bool>(result, result ? null! : "Failed To Remove Leave Type", !result);
            }
            catch (DbUpdateException)
            {
                // Specifically catch the FK-constraint case and give a clean, non-technical message
                return new Response<bool>(false, "Cannot delete this leave type because it is used by existing leave requests.", true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
           
        }

        public Response<bool> Edit(EditLeaveTypeVM vm)
        {
            try
            {
                var entity = _leaveTypeRepo.GetById(vm.Id);
                if (entity == null)
                    return new Response<bool>(false, "Leave type not found", true);
                // thi create new instanse and throw exp
                //var mappedType = _mapper.Map<LeaveType>(vm);
                 _mapper.Map(vm, entity);
                var result = _leaveTypeRepo.Update(entity);
                return new Response<bool>(result, result ? null! : "Failed To Update Leave Type", !result);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }

        }
        public Response<List<LeaveTypeVM>> GetAll()
        {
            try
            {
                var leaveTypes = _leaveTypeRepo.GetAll();
                var mappedTypes = _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
                return new Response<List<LeaveTypeVM>>(mappedTypes, null!, false);
            }
            catch (Exception ex)
            {
                return new Response<List<LeaveTypeVM>>(null!, ex.Message, true);
            }

        }

        public Response<LeaveTypeVM> GetById(int id)
        {
            try
            {
                var entity = _leaveTypeRepo.GetById(id);
                var mappedType = _mapper.Map<LeaveTypeVM>(entity);
                return new Response<LeaveTypeVM>(mappedType, null!, false);
            }

            catch(Exception ex)
            {
                return new Response<LeaveTypeVM>(null!, ex.Message, true);

            }
        }
    }
}
