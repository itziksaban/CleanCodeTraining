using System.Collections.Generic;
using System.Linq;

namespace FunctionApp1;

internal class FixedBannersCreator : BannersCreator
{
    public override IEnumerable<Banner> Create(PlaceHolder placeHolder)
    {
        IEnumerable<Banner> banners = base.Create(placeHolder);
        banners.First().CalcByGivenPoint(placeHolder.Point);
        return banners;
    }
}