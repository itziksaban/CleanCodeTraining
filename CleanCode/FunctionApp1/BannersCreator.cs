using System.Collections.Generic;

namespace FunctionApp1;

internal class BannersCreator
{
    public virtual IEnumerable<Banner> Create(PlaceHolder placeHolder)
    {
        // creating banners bla bla...
        return new List<Banner>();
    }
}