using System.Collections.Generic;

namespace FunctionApp1;

internal interface ILocationCalculator
{
    void Calc(IEnumerable<Banner> banners);
}