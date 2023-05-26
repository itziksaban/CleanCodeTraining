public class Downloader : MessageHandler<DownloadMessage, DownloadResult>
{

    public Downloader(string queueConnectionString) : base(queueConnectionString)
    {
    }

    protected override DownloadResult Execute(DownloadMessage message)
    {
        // Download file bla bla..
        return new DownloadResult();
    }
}