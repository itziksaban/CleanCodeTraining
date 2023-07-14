namespace FunctionApp1;

public interface ILogAnalyzer
{
    void Analyze(ILinesReader linesReader);
}