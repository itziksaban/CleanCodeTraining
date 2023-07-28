namespace FunctionApp1.LogAnalyzer.LogAnalyzer;

public interface IReportGenerator

{
    void Add(Line line);
    Report Generate();
}