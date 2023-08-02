using System.Drawing;

namespace FunctionApp1;

public abstract class Banner
{
    public abstract void CalcByPrev(Banner prev);
    public Point Point { get; set; }
    public int Height { get; set; }
    public abstract int FeedIndex { get; set; }

    public void CalcByGivenPoint(Point placeHolderPoint)
    {
        // do some calculations and put in Point...
    }
}