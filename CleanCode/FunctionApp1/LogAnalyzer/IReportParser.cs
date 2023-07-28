using System.Collections.Generic;

namespace FunctionApp1.LogAnalyzer.LogAnalyzer;

public interface IReportParser
{
    void Save(Report report);
    object Parse(IEnumerable<Report> reports);
}