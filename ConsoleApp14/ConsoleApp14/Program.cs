//Erik Daniel Otalora Alba


using System;
using System.IO;
class SortingExercise
{
    private double[] numbers = new double[10];

    public void GetNumbers()
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Enter number {i + 1}: ");
                numbers[i] = Convert.ToDouble(Console.ReadLine());
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numerical values.");
            GetNumbers();
        }
    }

    public void DisplayMenu()
    {
        Console.WriteLine("Choose a sorting method:");
        Console.WriteLine("1. Bubble Sort");
        Console.WriteLine("2. Shell Sort");
        Console.WriteLine("3. Selection Sort");
        Console.WriteLine("4. Insertion Sort");
    }

    public void PrintNumbers(double[] sortedNumbers, string methodName)
    {
        Console.WriteLine($"Sorted Numbers using {methodName}:");
        foreach (var num in sortedNumbers)
        {
            Console.WriteLine(num);
        }
    }

    public void SaveToFile(double[] sortedNumbers, string methodName)
    {
        string filename = $"sorted_numbers_{methodName}.txt";
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine($"Sorted Numbers using {methodName}:");
            foreach (var num in sortedNumbers)
            {
                writer.WriteLine(num);
            }
        }
        Console.WriteLine($"Sorted numbers saved to {filename}");
    }

    public void BubbleSort()
    {
        int n = numbers.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (numbers[j] > numbers[j + 1])
                {
                    double temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                }
            }
        }
        PrintNumbers(numbers, "Bubble Sort");
        SaveToFile(numbers, "bubble_sort");
    }

    public void ShellSort()
    {
        int n = numbers.Length;
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                double temp = numbers[i];
                int j = i;
                while (j >= gap && numbers[j - gap] > temp)
                {
                    numbers[j] = numbers[j - gap];
                    j -= gap;
                }
                numbers[j] = temp;
            }
        }
        PrintNumbers(numbers, "Shell Sort");
        SaveToFile(numbers, "shell_sort");
    }

    public void SelectionSort()
    {
        int n = numbers.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (numbers[j] < numbers[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                double temp = numbers[i];
                numbers[i] = numbers[minIndex];
                numbers[minIndex] = temp;
            }
        }
        PrintNumbers(numbers, "Selection Sort");
        SaveToFile(numbers, "selection_sort");
    }

    public void InsertionSort()
    {
        int n = numbers.Length;
        for (int i = 1; i < n; i++)
        {
            double key = numbers[i];
            int j = i - 1;
            while (j >= 0 && numbers[j] > key)
            {
                numbers[j + 1] = numbers[j];
                j = j - 1;
            }
            numbers[j + 1] = key;
        }
        PrintNumbers(numbers, "Insertion Sort");
        SaveToFile(numbers, "insertion_sort");
    }

    static void Main()
    {
        SortingExercise sortingExercise = new SortingExercise();
        sortingExercise.GetNumbers();
        sortingExercise.DisplayMenu();

        Console.Write("Enter your choice (1-4): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                sortingExercise.BubbleSort();
                break;
            case "2":
                sortingExercise.ShellSort();
                break;
            case "3":
                sortingExercise.SelectionSort();
                break;
            case "4":
                sortingExercise.InsertionSort();
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                break;
        }
    }
}