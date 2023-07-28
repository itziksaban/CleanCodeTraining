using System.Drawing;

namespace FunctionApp1;

public class Banner
{
    public Point Point { get; set; }
    public int Height { get; set; }
    public bool IsInappropriate { get; set; }

    public void CalcByGivenPoint(Point placeHolderPoint)
    {
        // do some calculations and put in Point...
    }
}