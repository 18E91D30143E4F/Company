using Data.Repositories;
using Data.Repositories.Abstract;
using System;
using Services;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var reportService = new ReportService();
            reportService.GenerateReport();

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
