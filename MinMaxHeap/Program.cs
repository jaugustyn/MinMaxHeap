using System;

public class Program
{
    static void Main(string[] args)
    {
        var heap = new MinMaxHeap<int>(new int[] { 2, 1, 6, 7, 1, 3 });
        Console.WriteLine(string.Join(' ', heap.ToArray()));
        Console.WriteLine(heap.DeleteMax());
        Console.WriteLine(heap.Max);
        Console.WriteLine(string.Join(' ', heap.ToArray()));
    }
}

