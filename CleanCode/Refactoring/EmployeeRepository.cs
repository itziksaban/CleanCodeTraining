using Newtonsoft.Json;
using StackExchange.Redis;

namespace CleanCode_playground;

public class SavingOptions
{
    public SavingOptions(int maxRetries, int ttl)
    {
        MaxRetries = maxRetries;
        Ttl = ttl;
    }

    public int MaxRetries { get; private set; }
    public int Ttl { get; private set; }
}

public class EmployeeRepository
{
    private readonly SavingOptions _savingOptions;
    private readonly bool _ignoreNulls;
    private readonly bool _ignoreCircularReferences;
    private IDatabase _database;

    private EmployeeRepository(SavingOptions savingOptions, bool ignoreNulls, bool ignoreCircularReferences)
    {
        _savingOptions = savingOptions;
        _ignoreNulls = ignoreNulls;
        _ignoreCircularReferences = ignoreCircularReferences;
    }

    public static async Task<EmployeeRepository> CreateConnected(string connectionString, bool ignoreNulls, bool ignoreCircularReferences, 
        SavingOptions savingOptions)
    {
        var customerRepository = new EmployeeRepository(savingOptions, ignoreNulls, ignoreCircularReferences);
        await customerRepository.Connect(connectionString);
        return customerRepository;
    }

    public void Upsert(Employee employee)
    {
        var json = Serialize(employee);
        StoreInDb(employee.Id, json);
    }

    private void StoreInDb(RedisKey employeeId, string json)
    {
        int retries = 0;
        do
        {
            try
            {
                _database.StringSet(employeeId, json, TimeSpan.FromSeconds(_savingOptions.Ttl));
            }
            catch (Exception e)
            {
                retries++;
            }
        } while (retries < _savingOptions.MaxRetries);
    }

    private async Task Connect(string connectionString)
    {
        var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(connectionString);
        _database = connectionMultiplexer.GetDatabase();
    }

    private string Serialize(Employee employee)
    {
        var config = CreateConfig();
        string json = JsonConvert.SerializeObject(employee, config);
        return json;
    }

    private JsonSerializerSettings CreateConfig()
    {
        JsonSerializerSettings config = new JsonSerializerSettings
        {
            NullValueHandling = _ignoreNulls ? NullValueHandling.Ignore : NullValueHandling.Include,
            ReferenceLoopHandling = _ignoreCircularReferences
                ? ReferenceLoopHandling.Ignore
                : ReferenceLoopHandling.Serialize
        };
        return config;
    }
}