namespace UnitTests;

public class MyList
{
    private readonly int _maxCols;
    private readonly int[][] _arr;
    private int _currentCol = 0;
    private int _currentRow = 0;
    private static int _maxRows;

    public MyList(int maxRows, int maxCols)
    {
        _maxRows = maxRows;
        _maxCols = maxCols;
        _arr = new int[_maxRows][];
        _arr[_currentRow] = new int[_maxCols];
    }

    public void Add(int value)
    {
        if (_currentCol == _maxCols)
        {
            _currentRow++;
            if (_currentRow >= _maxRows)
            {
                return;
            }
            _currentCol = 0;
            _arr[_currentRow] = new int[_maxCols];
        }
        _arr[_currentRow][_currentCol++] = value;
    }

    public int Get(int absoluteIndex)
    {
        var index = absoluteIndex % _maxCols;
        var arrayIndex = absoluteIndex / _maxCols;
        return _arr[arrayIndex][index];
    }
}