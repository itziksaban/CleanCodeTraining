using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Payments
{
    public interface IMonthlyApprovalRepository
    {
        MonthlyApproval Update(string companyId, int month, int year, bool approved, string approver);
        bool CreateNew(string companyId, int month, int year, bool approved, string approver);
        MonthlyApproval Get(string companyId, int month);
    }
}
