using System;

namespace FunctionApp1;

internal class FixedFeedBanner : FeedBanner
{
    public FixedFeedBanner(int feedIndex)
    {
        FeedIndex = feedIndex;
    }

    public override void CalcByPrev(FeedBanner prev)
    {
        throw new NotImplementedException("Never reach this...");
    }
}