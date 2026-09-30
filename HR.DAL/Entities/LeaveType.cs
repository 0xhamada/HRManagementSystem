using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DAL.Entities
{
    public class LeaveType
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public ICollection<LeaveRequest> LeaveRequests { get; private set; } = new List<LeaveRequest>();
        private LeaveType() { } // required by EF Core

        public LeaveType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
