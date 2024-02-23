using CleanCode_playground;

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
            Employee employee = null;
            repository.Upsert(employee);

        }
    }
}
