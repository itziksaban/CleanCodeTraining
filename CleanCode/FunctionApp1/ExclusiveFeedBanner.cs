using System.Drawing;

namespace FunctionApp1;

internal class ExclusiveFeedBanner : FeedBanner
{
    public override void CalcByPrev(FeedBanner prev)
    {
        var height = Height;
        if (prev.IsInappropriate)
        {
            height += 30;
        }

        Point = new Point(prev.Point.X, prev.Point.Y + height);
    }
}