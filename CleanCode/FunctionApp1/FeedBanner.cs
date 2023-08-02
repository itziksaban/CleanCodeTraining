using System.Drawing;

namespace FunctionApp1;

internal class FeedBanner : Banner
{
    public override void CalcByPrev(Banner prev)
    {
        Point = new Point(prev.Point.X, prev.Point.Y + Height);
    }

    public override int FeedIndex { get; set; }
}