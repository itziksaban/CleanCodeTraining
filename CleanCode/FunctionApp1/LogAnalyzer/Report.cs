using System.Collections;
using System.Collections.Generic;

namespace FunctionApp1.LogAnalyzer;

public class Report
{
    public IEnumerable<string> Headers { get; set; }
    public IEnumerable<IEnumerable<string>> Lines { get; set; }
}