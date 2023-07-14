using System.Collections.Generic;

namespace FunctionApp1;

internal interface IBannersCreator
{
    IEnumerable<Banner> Create(PlaceHolder placeHolder);
    Banner CreateFloating(Banner firstRegularBanner);
}