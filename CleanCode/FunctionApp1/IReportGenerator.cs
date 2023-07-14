namespace FunctionApp1;

public interface IReportGenerator

{
    void Add(Line line);
    Report Generate();
}