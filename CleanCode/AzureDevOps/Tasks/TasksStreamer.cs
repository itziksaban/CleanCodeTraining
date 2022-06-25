using System.IO;

namespace AzureDevOps.Tasks;

public class TasksStreamer
{
    private StreamWriter _streamWriter;
    private Stream _stream;

    public void OpenStream(string fileName)
    {
        _stream = File.Create(fileName);
        _streamWriter = new StreamWriter(_stream);
        _streamWriter.AutoFlush = true;
    }

    public async System.Threading.Tasks.Task WriteAsync(Task task)
    {
        await _streamWriter.WriteLineAsync(task.FeatureId + " | " + task.Name);
    }

    public async System.Threading.Tasks.Task FlushAsync()
    {
        await _streamWriter.FlushAsync();
    }
}