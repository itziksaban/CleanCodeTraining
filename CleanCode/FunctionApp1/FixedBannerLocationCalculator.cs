using System.Collections.Generic;
using System.Linq;

namespace FunctionApp1;

internal class FixedBannerLocationCalculator : ILocationCalculator
{
    private readonly PlaceHolder _placeHolder;

    public FixedBannerLocationCalculator(PlaceHolder placeHolder)
    {
        _placeHolder = placeHolder;
    }

    public void Calc(IEnumerable<Banner> banners)
    {
        banners.First().CalcByGivenPoint(_placeHolder.Point);
    }
}