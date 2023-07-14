using System;

namespace FunctionApp1;

internal class FixedBanner : Banner
{
    public override void CalcByPrev(Banner prev)
    {
        throw new NotImplementedException("Never reach this...");
    }

    public override int FeedIndex
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
}