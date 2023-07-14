using System.IO;

namespace FunctionApp1;

public interface ILogReader
{
    Stream Read();
}