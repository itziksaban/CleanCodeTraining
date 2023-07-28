using System;
using System.Collections;
using System.Collections.Generic;
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
        public Function1(PlaceHolder placeHolder1)
        {
            _placeHolder = placeHolder1;
        }

        private BannersCreator _bannersCreator;
        private PlaceHolder _placeHolder;

        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, IEnumerable<PlaceHolder> placeHolders)
        {
            var allBanners = new List<Banner>();
            foreach (var placeHolder in placeHolders)
            {
                var bannersCreator = GetBannersCreator(placeHolder);
                allBanners.AddRange(bannersCreator.Create(placeHolder));
            }

            return new OkObjectResult(allBanners);
        }

        private BannersCreator GetBannersCreator(PlaceHolder placeHolder)
        {
            switch (placeHolder.Type)
            {
                case PlaceholderType.Feed:
                    return new FeedBannersCreator();
                case PlaceholderType.FixedBanner:
                    return new FixedBannersCreator();
                default:
                    throw new ArgumentException("Unknown placeholder type");
            }
        }
    }
}
