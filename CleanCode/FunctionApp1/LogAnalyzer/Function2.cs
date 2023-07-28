using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp1.LogAnalyzer.LogAnalyzer;

public class Function2
{
    private readonly ILogReader _logReader;
    private readonly ILogAnalyzer _logAnalyzer;
    private readonly IReportParser _reportParser;
    private IBannersCreator _bannersCreator;

    public Function2(ILogReader logReader, ILogAnalyzer logAnalyzer, IReportParser reportParser)
    {
        _logReader = logReader;
        _logAnalyzer = logAnalyzer;
        _reportParser = reportParser;
    }

    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
    {
        var stream = _logReader.Read();
        var linesReader = new LinesReader(stream);
        var reports = _logAnalyzer.Analyze(linesReader);
        return new OkObjectResult(_reportParser.Parse(reports));
    }

    private FeedBanner CreateFloatingBanner(IEnumerable<FeedBanner> banners)
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
                return new FeedLocationCalculator(placeHolder);
            case PlaceholderType.FixedBanner:
                return new FixedBannerLocationCalculator(placeHolder);
            default:
                throw new ArgumentException("Unknown placeholder type");
        }
    }
}