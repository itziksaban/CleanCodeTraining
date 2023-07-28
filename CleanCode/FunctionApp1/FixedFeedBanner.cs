using System;

namespace FunctionApp1;

internal class FixedFeedBanner : FeedBanner
{
    public override void CalcByPrev(FeedBanner prev)
    {
        throw new NotImplementedException("Never reach this...");
    }
}