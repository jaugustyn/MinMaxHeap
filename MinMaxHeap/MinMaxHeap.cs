using System;
using System.Collections.Generic;

public class MinMaxHeap<T> where T : IComparable<T>
{
    private readonly List<T> values;
    private void Swap(int i, int j) => (values[i], values[j]) = (values[j], values[i]);
    private int IndexOfMaxChildOrGrandchild(int index)
    {
        var descendants = new[]
        {
            2 * index + 1, 2 * index + 2, 4 * index + 3, 4 * index + 4, 4 * index + 5, 4 * index + 6
        }; // 2 child, 4 grandchild
        var result = descendants[0];
        foreach (var des in descendants)
        {
            if (des >= Count) break;
            if (values[des].CompareTo(values[result]) > 0) result = des;
        }

        return result;
    }
    private int IndexOfMinChildOrGrandchild(int index)
    {
        var descendants = new[]
            {2 * index + 1, 2 * index + 2, 4 * index + 3, 4 * index + 4, 4 * index + 5, 4 * index + 6};
        var result = descendants[0];
        foreach (var des in descendants)
        {
            if (des >= Count) break;
            if (values[des].CompareTo(values[result]) < 0) result = des;
        }

        return result;
    }
    private static bool IsGrandchild(int node, int grandchild) => grandchild > 2 && ((grandchild - 1) / 2 - 1) / 2 == node;
    private static bool IsMinLevelIndex(int index)
    {
        // For all Min levels, value (index + 1) has the leftmost bit set to '1' at even position.
        const uint minLevelsBits = 0x55555555;
        const uint maxLevelsBits = 0xAAAAAAAA;
        return ((index + 1) & minLevelsBits) > ((index + 1) & maxLevelsBits);
    }

    public MinMaxHeap()
    {
        values = new List<T>();
    }
    public MinMaxHeap(IEnumerable<T> collection) : this()
    {
        foreach (var element in collection)
            Insert(element);
    }

    public void Insert(T element)
    {
        values.Add(element);
        PushUp(Count - 1);
    }

    private void PushDown(int index)
    {
        if (IsMinLevelIndex(index))
            PushDownMin(index);
        else
            PushDownMax(index);
    }
    private void PushDownMin(int index)
    {
        if (!(index * 2 + 1 < Count)) return; // If doesn't has child

        var minIndex = IndexOfMinChildOrGrandchild(index);
        var parent = (minIndex - 1) / 2;

        if (IsGrandchild(index, minIndex))
        {
            if (values[minIndex].CompareTo(values[index]) < 0)
            {
                Swap(minIndex, index);
                if (values[minIndex].CompareTo(values[parent]) > 0)
                {
                    Swap(minIndex, parent);
                }

                PushDownMin(minIndex);
            }
        }
        else
        {
            if (values[minIndex].CompareTo(values[index]) < 0)
            {
                Swap(minIndex, index);
            }
        }
    }
    private void PushDownMax(int index)
    {
        if (!(index * 2 + 1 < Count)) return; // If doesn't has child

        var maxIndex = IndexOfMaxChildOrGrandchild(index);
        var parent = (maxIndex - 1) / 2;

        if (IsGrandchild(index, maxIndex))
        {
            if (values[maxIndex].CompareTo(values[index]) > 0)
            {
                Swap(maxIndex, index);
                if (values[maxIndex].CompareTo(values[parent]) < 0)
                {
                    Swap(maxIndex, parent);
                }

                PushDownMax(maxIndex);
            }
        }
        else
        {
            if (values[maxIndex].CompareTo(values[index]) > 0)
            {
                Swap(maxIndex, index);
            }
        }
    }

    private void PushUp(int index)
    {
        if (index == 0) return;

        var parent = (index - 1) / 2;

        if (IsMinLevelIndex(index))
        {
            if (values[index].CompareTo(values[parent]) > 0)
            {
                Swap(index, parent);
                PushUpMax(parent);
            }
            else
            {
                PushUpMin(index);
            }
        }
        else
        {
            if (values[index].CompareTo(values[parent]) < 0)
            {
                Swap(index, parent);
                PushUpMin(parent);
            }
            else
            {
                PushUpMax(index);
            }
        }
    }
    private void PushUpMin(int index)
    {
        if (index <= 2) return;

        var grandparent = ((index - 1) / 2 - 1) / 2;
        if (values[index].CompareTo(values[grandparent]) < 0)
        {
            Swap(index, grandparent);
            PushUpMin(grandparent);
        }
    }
    private void PushUpMax(int index)
    {
        if (index <= 2) return;

        var grandparent = ((index - 1) / 2 - 1) / 2;
        if (values[index].CompareTo(values[grandparent]) > 0)
        {
            Swap(index, grandparent);
            PushUpMax(grandparent);
        }
    }

    private void Delete(int index)
    {
        Swap(index, Count - 1);
        values.RemoveAt(Count - 1);
        if (Count != 0) PushDown(index);
    }
    public T DeleteMin()
    {
        if (Count == 0) throw new InvalidOperationException("Heap is empty");
        var min = Min;
        Delete(0);
        return min;
    }
    public T DeleteMax()
    {
        if (Count == 0) throw new InvalidOperationException("Heap is empty");

        var max = Max;
        switch (Count)
        {
            case 1:
                Delete(0);
                break;
            case 2:
                Delete(1);
                break;
            default: 
                Delete(values[1].CompareTo(values[2]) > 0 ? 1 : 2);
                break;
        }
        return max;
    }

    public T Min => values[0];
    public T Max
    {
        get
        {
            return Count switch
            {
                0 => throw new InvalidOperationException("Heap is empty"),
                1 => values[0],
                2 => values[1],
                _ => values[1].CompareTo(values[2]) > 0 ? values[1] : values[2]
            };
            ;
        }
    }
    public void Clear() => values.Clear();
    public int Count => values.Count;
    public T[] ToArray() => values.ToArray();
}