using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
      
        Task task1 = new Task(DisplayPrimeNumbers);
        task1.Start();

      
        Task<int[]> task2 = Task.Factory.StartNew(GeneratePrimeNumbers);

        
        int[] array = { 5, 2, 8, 1, 7, 3, 6, 4 };
        Task task3 = Task.Factory.StartNew(() => PerformArrayOperations(array))
            .ContinueWith(previousTask => DisplayResults(previousTask.Result));

      
        Task.WaitAll(task1, task2, task3);
    }

    static void DisplayPrimeNumbers()
    {
        Console.WriteLine("Прості числа від 0 до 1000:");

        for (int i = 2; i <= 1000; i++)
        {
            if (IsPrime(i))
                Console.Write($"{i} ");
        }

        Console.WriteLine();
    }

    static bool IsPrime(int number)
    {
        if (number < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }

    static int[] GeneratePrimeNumbers()
    {
        return Enumerable.Range(2, 999)
            .Where(IsPrime)
            .ToArray();
    }

    static int[] PerformArrayOperations(int[] array)
    {
        int min = array.Min();
        int max = array.Max();
        double average = array.Average();
        int sum = array.Sum();

        int[] distinctArray = array.Distinct().ToArray();
        Array.Sort(distinctArray);

        return new[] { min, max, (int)average, sum, distinctArray.Length, distinctArray };
    }

    static void DisplayResults(int[] results)
    {
        Console.WriteLine($"Мінімум: {results[0]}");
        Console.WriteLine($"Максимум: {results[1]}");
        Console.WriteLine($"Середнє арифметичне: {results[2]}");
        Console.WriteLine($"Сума: {results[3]}");
        Console.WriteLine($"Кількість унікальних значень: {results[4]}");
        Console.WriteLine("Відсортований масив без дублікатів:");
        Console.WriteLine(string.Join(" ", results[5]));
    }
}