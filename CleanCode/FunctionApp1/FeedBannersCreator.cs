using System.Collections.Generic;
using System.Linq;

namespace FunctionApp1;

internal class FeedBannersCreator : BannersCreator
{
    public override IEnumerable<Banner> Create(PlaceHolder placeHolder)
    {
        IEnumerable<FeedBanner> banners = base.Create(placeHolder).OfType<FeedBanner>().ToList();
        banners.Append(CreateFloatingBanner(banners));
        var locationCalculator = new LocationCalculator(placeHolder);
        locationCalculator.Calc(banners);
        return banners;
    }

    private FeedBanner CreateFloatingBanner(IEnumerable<FeedBanner> banners)
    {
        var firstBanner = banners.First(banner => banner.FeedIndex == 0);
        var floatingBanner = CreateFloating(firstBanner);
        return floatingBanner;
    }

    public FeedBanner CreateFloating(FeedBanner banner)
    {
        // create some new floating banner derived from the given banner bla bla...
        return new FixedFeedBanner(banner.FeedIndex);
    }
}