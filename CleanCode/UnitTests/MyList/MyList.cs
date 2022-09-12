namespace UnitTests;

public class MyList
{
    private readonly int _chunkSize;
    private readonly int[][] _arr = new int[1000][];
    private int _size = 0;
    private int currentArray = 0;

    public MyList(int chunkSize)
    {
        _chunkSize = chunkSize;
        _arr[currentArray] = new int[_chunkSize];
    }

    public void Add(int value)
    {
        if (_size == _chunkSize)
        {
            currentArray++;
            _size = 0;
            _arr[currentArray] = new int[_chunkSize];
        }
        _arr[currentArray][_size++] = value;
    }

    public int Get(int absoluteIndex)
    {
        var index = absoluteIndex % _chunkSize;
        var arrayIndex = absoluteIndex / _chunkSize;
        return _arr[arrayIndex][index];
    }
}