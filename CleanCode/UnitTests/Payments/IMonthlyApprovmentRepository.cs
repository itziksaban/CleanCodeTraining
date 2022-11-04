using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Payments
{
    public interface IMonthlyApprovmentRepository
    {
        MonthlyArppvoment Update(string companyId, int month, int year, bool approved, string approver);
        bool Insert(string companyId, int month, int year, bool approved, string approver);
    }
}
