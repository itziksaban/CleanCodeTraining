using System.Collections.Generic;

namespace FunctionApp1;

public class LogAnalyzer : ILogAnalyzer
{
    private IEnumerable<IReportGenerator> _reportsGenerators;
    private IReportSaver _reportsSaver;

    public LogAnalyzer(IEnumerable<IReportGenerator> reportsGenerators)
    {
        _reportsGenerators = reportsGenerators;
    }

    public void Analyze(ILinesReader linesReader)
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

        foreach (var reportsGenerator in _reportsGenerators)
        {
            _reportsSaver.Save(reportsGenerator.Generate());
        }
    }
}