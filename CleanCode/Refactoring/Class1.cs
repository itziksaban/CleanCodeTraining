using CleanCode_playground;
using StackExchange.Redis;

namespace Refactoring
{
    public class Class1
    {
        public Class1()
        {
        }

        public async Task SomeMethod()
        {
            var repository = await EmployeeRepository.CreateConnected(3.5, 5, "conn", true, false);
            Employee employee = new Employee
            {
                Id = new RedisKey(Guid.NewGuid().ToString())
            };
            repository.Upsert(employee);

        }
    }
}
