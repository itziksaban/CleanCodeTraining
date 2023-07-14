using System.Collections.Generic;

namespace FunctionApp1;

internal interface ILocationCalculator
{
    void CalcCoordinates(IEnumerable<Banner> banners);
}