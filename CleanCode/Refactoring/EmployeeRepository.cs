using Newtonsoft.Json;
using StackExchange.Redis;

namespace CleanCode_playground;

public class EmployeeRepository
{
    private IDatabase _database;
    private readonly double _ttl;
    private readonly int _maxRetries;
    private readonly bool _ignoreNulls;
    private bool _ignoreCircularReferences;

    public EmployeeRepository(double ttl, int maxRetries, bool ignoreNulls, bool ignoreCircularReferences,
        IDatabase database)
    {
        _ignoreNulls = ignoreNulls;
        _ttl = ttl;
        _maxRetries = maxRetries;
        _ignoreCircularReferences = ignoreCircularReferences;
        _database = database;
    }

    public static async Task<EmployeeRepository> CreateConnected(double ttl, int maxRetries, string connectionString, bool ignoreNulls, bool ignoreCircularReferences)
    {
        var database = await Connect(connectionString);
        var customerRepository = new EmployeeRepository(ttl, maxRetries, ignoreNulls, ignoreCircularReferences, database);
        return customerRepository;
    }

    public void Upsert(Employee employee)
    {
        var config = new JsonSerializerSettings
        {
            NullValueHandling = _ignoreNulls ? NullValueHandling.Ignore : NullValueHandling.Include,
            ReferenceLoopHandling = _ignoreCircularReferences
                ? ReferenceLoopHandling.Ignore
                : ReferenceLoopHandling.Serialize
        };
        var json = JsonConvert.SerializeObject(employee, config);
        int retries = 0;
        do
        {
            try
            {
                _database.StringSet(employee.Id, json, TimeSpan.FromSeconds(_ttl));
            }
            catch (Exception e)
            {
                retries++;
            }
        } while (retries < _maxRetries);
    }

    private static async Task<IDatabase> Connect(string connectionString)
    {
        var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(connectionString);
        return connectionMultiplexer.GetDatabase();
    }
}

public class Employee
{
    public RedisKey Id { get; set; }
}