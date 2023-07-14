using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public class Function1
    {
        private BannersCreator _bannersCreator;

        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, IEnumerable<PlaceHolder> placeHolders)
        {
            var allBanners = new List<Banner>();
            foreach (var placeHolder in placeHolders)
            {
                IEnumerable<Banner> banners = _bannersCreator.Create(placeHolder);
                if (placeHolder.Type == PlaceholderType.Feed)
                {
                    banners.Append(CreateFloatingBanner(banners));
                }
                var locationCalculator = GetLocationCalculator(placeHolder);
                locationCalculator.Calc(banners);

                allBanners.AddRange(banners);
            }

            return new OkObjectResult(allBanners);
        }

        private Banner CreateFloatingBanner(IEnumerable<Banner> banners)
        {
            var firstBanner = banners.First(banner => banner.FeedIndex == 0);
            var floatingBanner = _bannersCreator.CreateFloating(firstBanner);
            return floatingBanner;
        }

        private ILocationCalculator GetLocationCalculator(PlaceHolder placeHolder)
        {
            switch (placeHolder.Type)
            {
                case PlaceholderType.Feed:
                    return new FeedCalculator(placeHolder);
                case PlaceholderType.FixedBanner:
                    return new FixedBannerCalculator(placeHolder);
                default:
                    throw new ArgumentException("Unknown placeholder type");
            }
        }
    }
}
