using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "StudentGrades.txt";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("STUDENT GRADING SYSTEM");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. View all students and grades");
            Console.WriteLine("3. Exit");
            Console.Write("Enter option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                AddStudent(filePath);
            }
            else if (choice == "2")
            {
                ViewGrades(filePath);
                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option! Try again.");
                Console.ReadLine();
            }
        }
    }

    static void AddStudent(string filePath)
    {
        Console.Write("\nEnter student name: ");
        string name = Console.ReadLine();

        double[] grades = new double[4];
        for (int i = 0; i < 4; i++)
        {
            Console.Write($"Enter Term {i + 1} grade: ");
            while (!double.TryParse(Console.ReadLine(), out grades[i]) || grades[i] < 0 || grades[i] > 100)
            {
                Console.Write("Invalid input. Enter a number between 0-100: ");
            }
        }

        double average = (grades[0] + grades[1] + grades[2] + grades[3]) / 4;
        string category = GetGradeCategory(average);

        // Save to file
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"{category}: {name} - {average:F1}");
        }

        Console.WriteLine($"\n{name}'s average is {average:F1}, categorized as {category}.");
        Console.WriteLine("Saved successfully!");
        Console.WriteLine("\nPress Enter to return to the menu...");
        Console.ReadLine();
    }

    static string GetGradeCategory(double avg)
    {
        if (avg >= 90) return "A";
        if (avg >= 80) return "B";
        if (avg >= 70) return "C";
        return "D";
    }

    static void ViewGrades(string filePath)
    {
        Console.WriteLine("\nALL STUDENTS AND GRADES\n");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("No grades saved yet.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine("No data available.");
            return;
        }

        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
    }
}
