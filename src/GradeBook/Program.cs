using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Scott's Grade Book");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStats();

            System.Console.WriteLine($"For the Book named {book.Name}");
            System.Console.WriteLine($"Your lowest grade is {stats.Low}");
            System.Console.WriteLine($"Your highest grade is {stats.High}");
            System.Console.WriteLine($"Your average grade is {stats.Average:N1}");
            System.Console.WriteLine($"Your letter is grade {stats.Letter}");
        }

        private static void EnterGrades(IBook inMemoryBook)
        {
            while (true)
            {
                System.Console.WriteLine("Enter a grade number or 'q' to quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    inMemoryBook.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    throw;
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    System.Console.WriteLine("**");
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A Grade was added");
        }
    }
}
