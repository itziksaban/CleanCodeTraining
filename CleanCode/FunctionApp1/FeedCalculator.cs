using System.Collections.Generic;
using System.Linq;

namespace FunctionApp1;

internal class FeedCalculator : ILocationCalculator
{
    private readonly PlaceHolder _placeHolder;

    public FeedCalculator(PlaceHolder placeHolder)
    {
        _placeHolder = placeHolder;
    }

    public void CalcCoordinates(IEnumerable<Banner> banners)
    {
        Banner prev = null;
        foreach (var banner in banners.OrderBy(b => b.FeedIndex))
        {
            prev = banner;
            if (banner.FeedIndex == 0)
            {
                banner.CalcByGivenPoint(_placeHolder.Point);
            }
            else
            {
                banner.CalcByPrev(prev);
            }
        }
    }
}