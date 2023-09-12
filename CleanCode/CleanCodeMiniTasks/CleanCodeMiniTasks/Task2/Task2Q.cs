namespace CleanCodeMiniTasks.Task2;

using Azure.Messaging.ServiceBus;

public interface IServiceBusMessageProcessor
{
    Task ProcessMessageAsync(ServiceBusReceivedMessage message);
}

public class Video 
{
    public string Id { get; } = default!;
}

public class VideoInsights { }

public class Frame { }

public class VideoMessageProcessor : IServiceBusMessageProcessor
{
    public async Task ProcessMessageAsync(ServiceBusReceivedMessage message)
    {
        var video = await DownloadAsync(message.Body.ToString());

        var transcript = await TranscribeAsync(video);

        var frames = await ExtractFramesAsync(video);

        // Fill insights here
        var insights = CreateInsights(transcript, frames);

        // Update
        await UpdateVideoAsync(video.Id, insights);
    }

    private VideoInsights CreateInsights(string transcript, Frame[] frames) => throw new NotImplementedException();

    private Task<Video> DownloadAsync(string url) => throw new NotImplementedException();

    private Task<string> TranscribeAsync(Video video) => throw new NotImplementedException();

    private Task<Frame[]> ExtractFramesAsync(Video video) => throw new NotImplementedException();

    private Task UpdateVideoAsync(string videoId, VideoInsights videoInsights) => throw new NotImplementedException();

}