using System.Collections.Generic;

namespace FunctionApp1;

public class BannersCreator
{
    public IEnumerable<Banner> Create(PlaceHolder placeHolder)
    {
        // creating banners bla bla...
        return new List<Banner>();
    }

    public Banner CreateFloating(Banner banner)
    {
        // creating floating banner bla bla...
        return new RegularFeedBanner();
    }
}