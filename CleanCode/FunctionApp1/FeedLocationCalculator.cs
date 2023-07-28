using System.Collections.Generic;
using System.Linq;

namespace FunctionApp1;

internal class FeedLocationCalculator : ILocationCalculator
{
    private readonly PlaceHolder _placeHolder;

    public FeedLocationCalculator(PlaceHolder placeHolder)
    {
        _placeHolder = placeHolder;
    }

    public void Calc(IEnumerable<Banner> banners)
    {
        FeedBanner prev = null;
        foreach (var banner in banners.OfType<FeedBanner>().OrderBy(b => b.FeedIndex))
        {
            prev = banner;
            SetBannerLocation(banner, prev);
        }
    }

    private void SetBannerLocation(FeedBanner banner, FeedBanner prev)
    {
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