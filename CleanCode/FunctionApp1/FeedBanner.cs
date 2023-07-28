namespace FunctionApp1;

public abstract class FeedBanner : Banner
{
    public abstract void CalcByPrev(FeedBanner prev);
    public int FeedIndex { get; set; }
}