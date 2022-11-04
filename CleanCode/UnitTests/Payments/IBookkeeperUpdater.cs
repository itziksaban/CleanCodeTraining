using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Payments
{
    public interface IBookkeeperUpdater
    {
        void Update(string companyId);
    }
}
