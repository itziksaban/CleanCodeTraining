using System.Collections.Generic;

namespace FunctionApp1.LogAnalyzer.LogAnalyzer;

public interface ILogAnalyzer
{
    IEnumerable<Report> Analyze(ILinesReader linesReader);
}