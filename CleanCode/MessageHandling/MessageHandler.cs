using Newtonsoft.Json;

public abstract class MessageHandler<TMessage, TResult>
{
    private readonly SomeQueueClient _someQueueClient;

    public MessageHandler(string queueConnectionString)
    {
        _someQueueClient = new SomeQueueClient(queueConnectionString);
    }

    public void StartListening()
    {
        do
        {
            var message = _someQueueClient.ReceiveMessage();
            var deserializeObject = JsonConvert.DeserializeObject<TMessage>(message);
            var result = Execute(deserializeObject);
            _someQueueClient.SendMessage(JsonConvert.SerializeObject(result));
        } while (true);
    }

    protected abstract TResult Execute(TMessage message);
}