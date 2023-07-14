using System.Drawing;

namespace FunctionApp1;

internal class ExclusiveBanner : Banner
{
    public override void CalcByPrev(Banner prev)
    {
        var height = Height;
        if (prev.IsInappropriate)
        {
            height += 30;
        }

        Point = new Point(prev.Point.X, prev.Point.Y + height);
    }
}