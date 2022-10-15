using System.Collections;

namespace NetTools.Lists;

public static class Sorting
{
    private static Comparison<T> DefaultComparisonMethod<T>() where T : IComparable<T>
    {
        return (x, y) => x.CompareTo(y);
    }

    private static Comparison<T> DefaultComparisonMethod<T>(IComparer comparer)
    {
        return (x, y) => comparer.Compare(x, y);
    }

    private static Comparison<T> DefaultComparisonMethod<T>(IComparer<T> comparer)
    {
        return comparer.Compare;
    }

    private static void ExecuteDefaultSortMethod<T>(this IList<T> list, Comparison<T> comparison)
    {
        // Change this to whatever the default sorting algorithm should be
        list.BubbleSort(comparison);
    }

    private static void ExecuteDefaultSortMethod<T>(this IList<T> list) where T : IComparable<T>
    {
        ExecuteDefaultSortMethod(list, DefaultComparisonMethod<T>());
    }

    private static void ExecuteDefaultSortMethod<T>(this IList<T> list, IComparer<T> comparer)
    {
        ExecuteDefaultSortMethod(list, DefaultComparisonMethod(comparer));
    }

    private static void ExecuteDefaultSortMethod<T>(this IList<T> list, IComparer comparer)
    {
        ExecuteDefaultSortMethod(list, DefaultComparisonMethod<T>(comparer));
    }

    public static void Sort<T>(this IList<T> list) where T : IComparable<T>
    {
        ExecuteDefaultSortMethod(list);
    }

    public static void Sort<T>(this IList<T> list, IComparer<T> comparer)
    {
        ExecuteDefaultSortMethod(list, comparer);
    }

    public static void Sort<T>(this IList<T> list, IComparer comparer)
    {
        ExecuteDefaultSortMethod(list, comparer);
    }

    public static void BubbleSort<T>(this IList<T> list, Comparison<T> comparison)
    {
        for (var i = 0; i < list.Count; i++)
        {
            for (var j = 0; j < list.Count - 1; j++)
            {
                if (comparison(list[j], list[j + 1]) <= 0) continue;
                (list[j], list[j + 1]) = (list[j + 1], list[j]);
            }
        }
    }

    public static void BubbleSort<T>(this IList<T> list) where T : IComparable<T>
    {
        list.BubbleSort(DefaultComparisonMethod<T>());
    }

    public static void BubbleSort<T>(this IList<T> list, IComparer<T> comparer)
    {
        list.BubbleSort(DefaultComparisonMethod(comparer));
    }

    public static void BubbleSort<T>(this IList<T> list, IComparer comparer)
    {
        list.BubbleSort(DefaultComparisonMethod<T>(comparer));
    }

    public static void InsertionSort<T>(this IList<T> list, Comparison<T> comparison)
    {
        for (var i = 1; i < list.Count; i++)
        {
            var j = i;
            while (j > 0 && comparison(list[j - 1], list[j]) > 0)
            {
                (list[j - 1], list[j]) = (list[j], list[j - 1]);
                j--;
            }
        }
    }

    public static void InsertionSort<T>(this IList<T> list) where T : IComparable<T>
    {
        list.InsertionSort(DefaultComparisonMethod<T>());
    }

    public static void InsertionSort<T>(this IList<T> list, IComparer<T> comparer)
    {
        list.InsertionSort(DefaultComparisonMethod(comparer));
    }

    public static void InsertionSort<T>(this IList<T> list, IComparer comparer)
    {
        list.InsertionSort(DefaultComparisonMethod<T>(comparer));
    }

    public static void SelectionSort<T>(this IList<T> list, Comparison<T> comparison)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var min = i;
            for (var j = i + 1; j < list.Count; j++)
            {
                if (comparison(list[j], list[min]) >= 0) continue;
                min = j;
            }

            (list[i], list[min]) = (list[min], list[i]);
        }
    }

    public static void SelectionSort<T>(this IList<T> list) where T : IComparable<T>
    {
        list.SelectionSort(DefaultComparisonMethod<T>());
    }

    public static void SelectionSort<T>(this IList<T> list, IComparer<T> comparer)
    {
        list.SelectionSort(DefaultComparisonMethod(comparer));
    }

    public static void SelectionSort<T>(this IList<T> list, IComparer comparer)
    {
        list.SelectionSort(DefaultComparisonMethod<T>(comparer));
    }

    public static void MergeSort<T>(this IList<T> list, Comparison<T> comparison)
    {
        if (list.Count <= 1) return;
        var mid = list.Count / 2;
        var left = new List<T>();
        var right = new List<T>();
        for (var i = 0; i < mid; i++)
        {
            left.Add(list[i]);
        }

        for (var i = mid; i < list.Count; i++)
        {
            right.Add(list[i]);
        }

        left.MergeSort(comparison);
        right.MergeSort(comparison);
        list.Merge(left, right, comparison);
    }

    public static void MergeSort<T>(this IList<T> list) where T : IComparable<T>
    {
        list.MergeSort(DefaultComparisonMethod<T>());
    }

    public static void MergeSort<T>(this IList<T> list, IComparer<T> comparer)
    {
        list.MergeSort(DefaultComparisonMethod(comparer));
    }

    public static void MergeSort<T>(this IList<T> list, IComparer comparer)
    {
        list.MergeSort(DefaultComparisonMethod<T>(comparer));
    }

    private static void Merge<T>(this IList<T> list, IList<T> left, IList<T> right, Comparison<T> comparison)
    {
        var i = 0;
        var j = 0;
        var k = 0;
        while (i < left.Count && j < right.Count)
        {
            if (comparison(left[i], right[j]) <= 0)
            {
                list[k] = left[i];
                i++;
            }
            else
            {
                list[k] = right[j];
                j++;
            }

            k++;
        }

        while (i < left.Count)
        {
            list[k] = left[i];
            i++;
            k++;
        }

        while (j < right.Count)
        {
            list[k] = right[j];
            j++;
            k++;
        }
    }

    public static void QuickSort<T>(this IList<T> list, Comparison<T> comparison)
    {
        list.QuickSort(0, list.Count - 1, comparison);
    }

    public static void QuickSort<T>(this IList<T> list) where T : IComparable<T>
    {
        list.QuickSort(DefaultComparisonMethod<T>());
    }

    public static void QuickSort<T>(this IList<T> list, IComparer<T> comparer)
    {
        list.QuickSort(DefaultComparisonMethod(comparer));
    }

    public static void QuickSort<T>(this IList<T> list, IComparer comparer)
    {
        list.QuickSort(DefaultComparisonMethod<T>(comparer));
    }

    private static void QuickSort<T>(this IList<T> list, int left, int right, Comparison<T> comparison)
    {
        if (left >= right) return;
        var pivot = list[(left + right) / 2];
        var index = list.Partition(left, right, pivot, comparison);
        list.QuickSort(left, index - 1, comparison);
        list.QuickSort(index, right, comparison);
    }

    private static int Partition<T>(this IList<T> list, int left, int right, T pivot, Comparison<T> comparison)
    {
        while (left <= right)
        {
            while (comparison(list[left], pivot) < 0)
            {
                left++;
            }

            while (comparison(list[right], pivot) > 0)
            {
                right--;
            }

            if (left <= right)
            {
                (list[left], list[right]) = (list[right], list[left]);
                left++;
                right--;
            }
        }

        return left;
    }

    public static void HeapSort<T>(this IList<T> list, Comparison<T> comparison)
    {
        var heap = new Heap<T>(list, comparison);
        for (var i = 0; i < list.Count; i++)
        {
            list[i] = heap.Remove();
        }
    }

    public static void HeapSort<T>(this IList<T> list) where T : IComparable<T>
    {
        list.HeapSort(DefaultComparisonMethod<T>());
    }

    public static void HeapSort<T>(this IList<T> list, IComparer<T> comparer)
    {
        list.HeapSort(DefaultComparisonMethod(comparer));
    }

    public static void HeapSort<T>(this IList<T> list, IComparer comparer)
    {
        list.HeapSort(DefaultComparisonMethod<T>(comparer));
    }

    public static void RadixSort<T>(this IList<T> list, Comparison<T> comparison)
    {
        var min = list.Min();
        var max = list.Max();
        var maxDigits = Math.Max(Digits(min), Digits(max));
        for (var i = 0; i < maxDigits; i++)
        {
            var buckets = new List<T>[10];
            for (var j = 0; j < buckets.Length; j++)
            {
                buckets[j] = new List<T>();
            }

            foreach (var item in list)
            {
                var digit = (comparison(item, min) / (int)Math.Pow(10, i)) % 10;
                buckets[digit].Add(item);
            }

            var index = 0;
            foreach (var bucket in buckets)
            {
                foreach (var item in bucket)
                {
                    list[index] = item;
                    index++;
                }
            }
        }
    }

    public static void RadixSort<T>(this IList<T> list) where T : IComparable<T>
    {
        list.RadixSort(DefaultComparisonMethod<T>());
    }

    public static void RadixSort<T>(this IList<T> list, IComparer<T> comparer)
    {
        list.RadixSort(DefaultComparisonMethod(comparer));
    }

    public static void RadixSort<T>(this IList<T> list, IComparer comparer)
    {
        list.RadixSort(DefaultComparisonMethod<T>(comparer));
    }

    private static int Digits<T>(T value)
    {
        return (int)Math.Floor(Math.Log10(Convert.ToDouble(value))) + 1;
    }

    private static T Min<T>(this IList<T> list)
    {
        var min = list[0];
        for (var i = 1; i < list.Count; i++)
        {
            var @switch = new SwitchCase
            {
                {
                    typeof(int), () =>
                    {
                        if ((int)(object)list[i] < (int)(object)min) min = list[i];
                    }
                },
                {
                    typeof(long), () =>
                    {
                        if ((long)(object)list[i] < (long)(object)min) min = list[i];
                    }
                },
                {
                    typeof(float), () =>
                    {
                        if ((float)(object)list[i] < (float)(object)min) min = list[i];
                    }
                },
                {
                    typeof(double), () =>
                    {
                        if ((double)(object)list[i] < (double)(object)min) min = list[i];
                    }
                },
                {
                    typeof(decimal), () =>
                    {
                        if ((decimal)(object)list[i] < (decimal)(object)min) min = list[i];
                    }
                },
                {
                    typeof(char), () =>
                    {
                        if ((char)(object)list[i] < (char)(object)min) min = list[i];
                    }
                },
                {
                    typeof(string), () =>
                    {
                        if (string.Compare((string)(object)list[i], (string)(object)min) < 0) min = list[i];
                    }
                },
                {
                    typeof(DateTime), () =>
                    {
                        if (DateTime.Compare((DateTime)(object)list[i], (DateTime)(object)min) < 0) min = list[i];
                    }
                },
                {
                    typeof(TimeSpan), () =>
                    {
                        if (TimeSpan.Compare((TimeSpan)(object)list[i], (TimeSpan)(object)min) < 0) min = list[i];
                    }
                },
                {
                    typeof(bool), () =>
                    {
                        if ((bool)(object)list[i] && !(bool)(object)min) min = list[i];
                    }
                },
                {
                    typeof(byte), () =>
                    {
                        if ((byte)(object)list[i] < (byte)(object)min) min = list[i];
                    }
                },
                {
                    typeof(sbyte), () =>
                    {
                        if ((sbyte)(object)list[i] < (sbyte)(object)min) min = list[i];
                    }
                },
                {
                    typeof(short), () =>
                    {
                        if ((short)(object)list[i] < (short)(object)min) min = list[i];
                    }
                },
                {
                    typeof(ushort), () =>
                    {
                        if ((ushort)(object)list[i] < (ushort)(object)min) min = list[i];
                    }
                },
                {
                    typeof(uint), () =>
                    {
                        if ((uint)(object)list[i] < (uint)(object)min) min = list[i];
                    }
                },
                {
                    typeof(ulong), () =>
                    {
                        if ((ulong)(object)list[i] < (ulong)(object)min) min = list[i];
                    }
                },
                {
                    typeof(object), () =>
                    {
                        if (Convert.ToDouble(list[i]) < Convert.ToDouble(min)) min = list[i];
                    }
                },
                {
                    Scenario.Default, () => throw new Exception("Unsupported type.")
                }
            };
            @switch.MatchFirst(min.GetType());
        }

        return min;
    }

    private static T Max<T>(this IList<T> list)
    {
        var max = list[0];
        for (var i = 1; i < list.Count; i++)
        {
            var @switch = new SwitchCase
            {
                {
                    typeof(int), () =>
                    {
                        if ((int)(object)list[i] > (int)(object)max) max = list[i];
                    }
                },
                {
                    typeof(long), () =>
                    {
                        if ((long)(object)list[i] > (long)(object)max) max = list[i];
                    }
                },
                {
                    typeof(float), () =>
                    {
                        if ((float)(object)list[i] > (float)(object)max) max = list[i];
                    }
                },
                {
                    typeof(double), () =>
                    {
                        if ((double)(object)list[i] > (double)(object)max) max = list[i];
                    }
                },
                {
                    typeof(decimal), () =>
                    {
                        if ((decimal)(object)list[i] > (decimal)(object)max) max = list[i];
                    }
                },
                {
                    typeof(char), () =>
                    {
                        if ((char)(object)list[i] > (char)(object)max) max = list[i];
                    }
                },
                {
                    typeof(string), () =>
                    {
                        if (string.Compare((string)(object)list[i], (string)(object)max) > 0) max = list[i];
                    }
                },
                {
                    typeof(DateTime), () =>
                    {
                        if (DateTime.Compare((DateTime)(object)list[i], (DateTime)(object)max) > 0) max = list[i];
                    }
                },
                {
                    typeof(TimeSpan), () =>
                    {
                        if (TimeSpan.Compare((TimeSpan)(object)list[i], (TimeSpan)(object)max) > 0) max = list[i];
                    }
                },
                {
                    typeof(bool), () =>
                    {
                        if (!(bool)(object)list[i] && (bool)(object)max) max = list[i];
                    }
                },
                {
                    typeof(byte), () =>
                    {
                        if ((byte)(object)list[i] > (byte)(object)max) max = list[i];
                    }
                },
                {
                    typeof(sbyte), () =>
                    {
                        if ((sbyte)(object)list[i] > (sbyte)(object)max) max = list[i];
                    }
                },
                {
                    typeof(short), () =>
                    {
                        if ((short)(object)list[i] > (short)(object)max) max = list[i];
                    }
                },
                {
                    typeof(ushort), () =>
                    {
                        if ((ushort)(object)list[i] > (ushort)(object)max) max = list[i];
                    }
                },
                {
                    typeof(uint), () =>
                    {
                        if ((uint)(object)list[i] > (uint)(object)max) max = list[i];
                    }
                },
                {
                    typeof(ulong), () =>
                    {
                        if ((ulong)(object)list[i] > (ulong)(object)max) max = list[i];
                    }
                },
                {
                    typeof(object), () =>
                    {
                        if (Convert.ToDouble(list[i]) > Convert.ToDouble(max)) max = list[i];
                    }
                },
                {
                    Scenario.Default, () => throw new Exception("Unsupported type.")
                }
            };
            @switch.MatchFirst(max.GetType());
        }

        return max;
    }

    private class Heap<T>
    {
        private readonly List<T> _list;
        private readonly Comparison<T> _comparison;

        public Heap(IList<T> list, Comparison<T> comparison)
        {
            _list = new List<T>(list);
            _comparison = comparison;
            for (var i = _list.Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public T Remove()
        {
            var item = _list[0];
            _list[0] = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            Heapify(0);
            return item;
        }

        private void Heapify(int index)
        {
            var left = 2 * index + 1;
            var right = 2 * index + 2;
            var largest = index;
            if (left < _list.Count && _comparison(_list[left], _list[largest]) > 0)
            {
                largest = left;
            }

            if (right < _list.Count && _comparison(_list[right], _list[largest]) > 0)
            {
                largest = right;
            }

            if (largest != index)
            {
                (_list[index], _list[largest]) = (_list[largest], _list[index]);
                Heapify(largest);
            }
        }
    }

    private static void Swap<T>(this IList<T> list, int i, int j)
    {
        (list[i], list[j]) = (list[j], list[i]);
    }

    private static void Shuffle<T>(this IList<T> list)
    {
        var random = new Random();
        for (var i = 0; i < list.Count; i++)
        {
            list.Swap(i, random.Next(i, list.Count));
        }
    }

    private static void Shuffle<T>(this IList<T> list, Random random)
    {
        for (var i = 0; i < list.Count; i++)
        {
            list.Swap(i, random.Next(i, list.Count));
        }
    }

    private static void Shuffle<T>(this IList<T> list, int seed)
    {
        var random = new Random(seed);
        for (var i = 0; i < list.Count; i++)
        {
            list.Swap(i, random.Next(i, list.Count));
        }
    }

    private static void Shuffle<T>(this IList<T> list, int seed, int count)
    {
        var random = new Random(seed);
        for (var i = 0; i < count; i++)
        {
            list.Swap(random.Next(0, list.Count), random.Next(0, list.Count));
        }
    }

    private static void Shuffle<T>(this IList<T> list, int seed, int count, int min, int max)
    {
        var random = new Random(seed);
        for (var i = 0; i < count; i++)
        {
            list.Swap(random.Next(min, max), random.Next(min, max));
        }
    }

    private static void Shuffle<T>(this IList<T> list, int seed, int count, int min, int max, int min2, int max2)
    {
        var random = new Random(seed);
        for (var i = 0; i < count; i++)
        {
            list.Swap(random.Next(min, max), random.Next(min2, max2));
        }
    }
}
