using System.Drawing;

namespace FunctionApp1;

internal class RegularBanner : Banner
{
    public override void CalcByPrev(Banner prev)
    {
        Point = new Point(prev.Point.X, prev.Point.Y + Height);
    }
}