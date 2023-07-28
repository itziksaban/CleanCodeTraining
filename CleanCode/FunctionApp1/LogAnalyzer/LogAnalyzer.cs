using System.Collections.Generic;
using System.Linq;

namespace FunctionApp1.LogAnalyzer;

public class LogAnalyzer : ILogAnalyzer
{
    private IEnumerable<IReportGenerator> _reportsGenerators;

    public LogAnalyzer(IEnumerable<IReportGenerator> reportsGenerators)
    {
        _reportsGenerators = reportsGenerators;
    }

    public IEnumerable<Report> Analyze(ILinesReader linesReader)
    {
        var line = linesReader.Next();
        while (line != null)
        {
            foreach (var reportsGenerator in _reportsGenerators)
            {
                reportsGenerator.Add(line);
            }
            line = linesReader.Next();
        }

        return _reportsGenerators.Select(generator => generator.Generate());
    }
}