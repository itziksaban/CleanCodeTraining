using System.Collections.Generic;

namespace FunctionApp1;

internal interface IBannersCreator
{
    IEnumerable<FeedBanner> Create(PlaceHolder placeHolder);
    FeedBanner CreateFloating(FeedBanner firstRegularFeedBanner);
}