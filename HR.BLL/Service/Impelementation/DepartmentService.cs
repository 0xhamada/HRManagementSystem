using AutoMapper;
using HR.BLL.ModelVM.Department;
using HR.BLL.ModelVM.ResponseResult;
using HR.BLL.Service.Abstraction;
using HR.DAL.Entities;
using HR.DAL.Repo.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Service.Impelementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo dept;
        private readonly IMapper mapper;
        public DepartmentService(IDepartmentRepo _dept, IMapper mapper)
        {
            this.dept = _dept;
            this.mapper = mapper;
        }

        public Response<bool> AddDepartment(CreateDepartmentVM vm)
        {
            try
            {
                var map = mapper.Map<Department>(vm);
                var result = dept.Add(map);
                return new Response<bool>(result, result ? null : "Falied", !result);
            }
            catch (Exception e)
            {
                return new Response<bool>(false, e.Message, true);
            }

        }

        public Response<bool> DeleteDepartment(int id)
        {
            try
            {
                var result = dept.Delete(id);
                return new Response<bool>(result, result ? null : "Falied", !result);
            }
            
            catch (Exception e)
            {
                return new Response<bool>(false, e.Message, true);
            }
        }
        public Response<bool> EditDepartment(EditDepartmentVM vm)
        {
            try
            {
                var map = mapper.Map<Department>(vm);

                var result = dept.Edit(map);
                return new Response<bool>(result, result ? null : "Falied", !result);
            }
            catch (Exception e)
            {
                return new Response<bool>(false, e.Message, true);

            }
        }

        public Response<List<GetDepartmentVM>> GetAllDepartments()
        {
            try
            {
                var res = dept.GetAll();
                var map = mapper.Map<List<GetDepartmentVM>>(res);
                return new Response<List<GetDepartmentVM>>(map, null, false);
            }
            catch (Exception e)
            {
                return new Response<List<GetDepartmentVM>>(null, e.Message, true);

            }
        }

        public Response<GetDepartmentVM> GetDepartmentById(int id)
        {
            try
            {
                var res = dept.GetById(id);
                var map = mapper.Map<GetDepartmentVM>(res);
                return new Response<GetDepartmentVM>(map, null, false);
            }
            catch (Exception e)
            {
                return new Response<GetDepartmentVM>(null, e.Message, true);

            }
        }
    }
}
