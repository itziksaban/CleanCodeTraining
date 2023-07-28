using System.IO;

namespace FunctionApp1.LogAnalyzer.LogAnalyzer;

public interface ILogReader
{
    Stream Read();
}