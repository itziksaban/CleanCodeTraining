using System.Drawing;

namespace FunctionApp1;

internal abstract class Banner
{
    public abstract void CalcByPrev(Banner prev);
    public Point Point { get; set; }
    public int Height { get; set; }
    public int FeedIndex { get; set; }
    public bool IsInappropriate { get; set; }

    public void CalcByGivenPoint(Point placeHolderPoint)
    {
        // do some calculations and put in Point...
    }
}