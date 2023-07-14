using System.Collections.Generic;
using System.Linq;

namespace FunctionApp1;

internal class FixedBannerCalculator : ILocationCalculator
{
    private readonly PlaceHolder _placeHolder;

    public FixedBannerCalculator(PlaceHolder placeHolder)
    {
        _placeHolder = placeHolder;
    }

    public void Calc(IEnumerable<Banner> banners)
    {
        banners.First().CalcByGivenPoint(_placeHolder.Point);
    }
}