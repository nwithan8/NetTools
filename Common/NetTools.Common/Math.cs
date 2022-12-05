using System;
using System.Collections.Generic;
using System.Linq;

namespace NetTools;

public static class Math
{
    public static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
    
    public static bool IsEven(long number)
    {
        return number % 2 != 0;
    }
    
    public static bool IsEven(double number)
    {
        return number % 2 != 0;
    }
    
    public static bool IsEven(decimal number)
    {
        return number % 2 != 0;
    }
    
    public static bool IsEven(float number)
    {
        return number % 2 != 0;
    }
    
    public static bool IsOdd(int number)
    {
        return !IsEven(number);
    }
    
    public static bool IsOdd(long number)
    {
        return !IsEven(number);
    }
    
    public static bool IsOdd(double number)
    {
        return !IsEven(number);
    }
    
    public static bool IsOdd(decimal number)
    {
        return !IsEven(number);
    }
    
    public static bool IsOdd(float number)
    {
        return !IsEven(number);
    }

    public static int Sum(IEnumerable<int> numbers)
    {
        return numbers.Sum();
    }
    
    public static int Sum(params int[] numbers)
    {
        return numbers.Sum();
    }
    
    public static long Sum(IEnumerable<long> numbers)
    {
        return numbers.Sum();
    }
    
    public static long Sum(params long[] numbers)
    {
        return numbers.Sum();
    }
    
    public static float Sum(IEnumerable<float> numbers)
    {
        return numbers.Sum();
    }
    
    public static float Sum(params float[] numbers)
    {
        return numbers.Sum();
    }
    
    public static double Sum(IEnumerable<double> numbers)
    {
        return numbers.Sum();
    }
    
    public static double Sum(params double[] numbers)
    {
        return numbers.Sum();
    }
    
    public static decimal Sum(IEnumerable<decimal> numbers)
    {
        return numbers.Sum();
    }
    
    public static decimal Sum(params decimal[] numbers)
    {
        return numbers.Sum();
    }
    
    public static int Product(IEnumerable<int> numbers)
    {
        return numbers.Aggregate((int)1, (a, b) => a * b);
    }
    
    public static int Product(params int[] numbers)
    {
        return numbers.Aggregate((int)1, (a, b) => a * b);
    }
    
    public static long Product(IEnumerable<long> numbers)
    {
        return numbers.Aggregate((long)1, (a, b) => a * b);
    }
    
    public static long Product(params long[] numbers)
    {
        return numbers.Aggregate((long)1, (a, b) => a * b);
    }
    
    public static float Product(IEnumerable<float> numbers)
    {
        return numbers.Aggregate((float)1, (a, b) => a * b);
    }
    
    public static float Product(params float[] numbers)
    {
        return numbers.Aggregate((float)1, (a, b) => a * b);
    }
    
    public static double Product(IEnumerable<double> numbers)
    {
        return numbers.Aggregate((double)1, (a, b) => a * b);
    }
    
    public static double Product(params double[] numbers)
    {
        return numbers.Aggregate((double)1, (a, b) => a * b);
    }
    
    public static decimal Product(IEnumerable<decimal> numbers)
    {
        return numbers.Aggregate((decimal)1, (a, b) => a * b);
    }
    
    public static decimal Product(params decimal[] numbers)
    {
        return numbers.Aggregate((decimal)1, (a, b) => a * b);
    }
    
    public static int Average(IEnumerable<int> numbers)
    {
        return (int)numbers.Average();
    }
    
    public static int Average(params int[] numbers)
    {
        return (int)numbers.Average();
    }
    
    public static long Average(IEnumerable<long> numbers)
    {
        return (long)numbers.Average();
    }
    
    public static long Average(params long[] numbers)
    {
        return (long)numbers.Average();
    }
    
    public static float Average(IEnumerable<float> numbers)
    {
        return numbers.Average();
    }
    
    public static float Average(params float[] numbers)
    {
        return numbers.Average();
    }
    
    public static double Average(IEnumerable<double> numbers)
    {
        return numbers.Average();
    }
    
    public static double Average(params double[] numbers)
    {
        return numbers.Average();
    }
    
    public static decimal Average(IEnumerable<decimal> numbers)
    {
        return numbers.Average();
    }
    
    public static decimal Average(params decimal[] numbers)
    {
        return numbers.Average();
    }
    
    public static int Min(IEnumerable<int> numbers)
    {
        return numbers.Min();
    }
    
    public static int Min(params int[] numbers)
    {
        return numbers.Min();
    }
    
    public static long Min(IEnumerable<long> numbers)
    {
        return numbers.Min();
    }
    
    public static long Min(params long[] numbers)
    {
        return numbers.Min();
    }
    
    public static float Min(IEnumerable<float> numbers)
    {
        return numbers.Min();
    }
    
    public static float Min(params float[] numbers)
    {
        return numbers.Min();
    }
    
    public static double Min(IEnumerable<double> numbers)
    {
        return numbers.Min();
    }
    
    public static double Min(params double[] numbers)
    {
        return numbers.Min();
    }
    
    public static decimal Min(IEnumerable<decimal> numbers)
    {
        return numbers.Min();
    }
    
    public static decimal Min(params decimal[] numbers)
    {
        return numbers.Min();
    }
    
    public static int Max(IEnumerable<int> numbers)
    {
        return numbers.Max();
    }
    
    public static int Max(params int[] numbers)
    {
        return numbers.Max();
    }
    
    public static long Max(IEnumerable<long> numbers)
    {
        return numbers.Max();
    }
    
    public static long Max(params long[] numbers)
    {
        return numbers.Max();
    }
    
    public static float Max(IEnumerable<float> numbers)
    {
        return numbers.Max();
    }
    
    public static float Max(params float[] numbers)
    {
        return numbers.Max();
    }
    
    public static double Max(IEnumerable<double> numbers)
    {
        return numbers.Max();
    }
    
    public static double Max(params double[] numbers)
    {
        return numbers.Max();
    }
    
    public static decimal Max(IEnumerable<decimal> numbers)
    {
        return numbers.Max();
    }
    
    public static decimal Max(params decimal[] numbers)
    {
        return numbers.Max();
    }

    private static T GetMedian<T>(Array numbers)
    {
        var middleElement = numbers.GetMiddleElement(useUpperIndexIfEven: false);
        
        if (middleElement == null)
        {
            throw new InvalidOperationException("The collection is empty.");
        }
        
        return (T)middleElement;
    }

    private static T? GetMode<T>(IEnumerable<T> numbers)
    {
        return numbers.GroupBy(n => n)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();
    }
    
    public static int Median(IEnumerable<int> numbers)
    {
        numbers = numbers.OrderBy(n => n).ToArray();

        var numbersObjectArray = numbers.CastToGenericArray();
        
        if (numbersObjectArray == null)
            throw new InvalidOperationException("Unable to cast numbers to IEnumerable<object?>");
        
        return GetMedian<int>(numbersObjectArray);
    }

    public static int Median(params int[] numbers)
    {
        return Median((IEnumerable<int>)numbers);
    }
    
    public static long Median(IEnumerable<long> numbers)
    {
        numbers = numbers.OrderBy(n => n).ToArray();

        var numbersObjectArray = numbers.CastToGenericArray();
        
        if (numbersObjectArray == null)
            throw new InvalidOperationException("Unable to cast numbers to IEnumerable<object?>");
        
        return GetMedian<long>(numbersObjectArray);
    }
    
    public static long Median(params long[] numbers)
    {
        return Median((IEnumerable<long>)numbers);
    }
    
    public static float Median(IEnumerable<float> numbers)
    {
        numbers = numbers.OrderBy(n => n).ToArray();

        var numbersObjectArray = numbers.CastToGenericArray();
        
        if (numbersObjectArray == null)
            throw new InvalidOperationException("Unable to cast numbers to IEnumerable<object?>");
        
        return GetMedian<float>(numbersObjectArray);
    }
    
    public static float Median(params float[] numbers)
    {
        return Median((IEnumerable<float>)numbers);
    }
    
    public static double Median(IEnumerable<double> numbers)
    {
        numbers = numbers.OrderBy(n => n).ToArray();

        var numbersObjectArray = numbers.CastToGenericArray();
        
        if (numbersObjectArray == null)
            throw new InvalidOperationException("Unable to cast numbers to IEnumerable<object?>");
        
        return GetMedian<double>(numbersObjectArray);
    }
    
    public static double Median(params double[] numbers)
    {
        return Median((IEnumerable<double>)numbers);
    }
    
    public static decimal Median(IEnumerable<decimal> numbers)
    {
        numbers = numbers.OrderBy(n => n).ToArray();

        var numbersObjectArray = numbers.CastToGenericArray();
        
        if (numbersObjectArray == null)
            throw new InvalidOperationException("Unable to cast numbers to IEnumerable<object?>");
        
        return GetMedian<decimal>(numbersObjectArray);
    }
    
    public static decimal Median(params decimal[] numbers)
    {
        return Median((IEnumerable<decimal>)numbers);
    }
    
    public static int Mode(IEnumerable<int> numbers)
    {
        return GetMode(numbers);
    }
    
    public static int Mode(params int[] numbers)
    {
        return Mode((IEnumerable<int>)numbers);
    }
    
    public static long Mode(IEnumerable<long> numbers)
    {
        return GetMode(numbers);
    }
    
    public static long Mode(params long[] numbers)
    {
        return Mode((IEnumerable<long>)numbers);
    }
    
    public static float Mode(IEnumerable<float> numbers)
    {
        return GetMode(numbers);
    }
    
    public static float Mode(params float[] numbers)
    {
        return Mode((IEnumerable<float>)numbers);
    }
    
    public static double Mode(IEnumerable<double> numbers)
    {
        return GetMode(numbers);
    }
    
    public static double Mode(params double[] numbers)
    {
        return Mode((IEnumerable<double>)numbers);
    }
    
    public static decimal Mode(IEnumerable<decimal> numbers)
    {
        return GetMode(numbers);
    }
    
    public static decimal Mode(params decimal[] numbers)
    {
        return Mode((IEnumerable<decimal>)numbers);
    }
}
