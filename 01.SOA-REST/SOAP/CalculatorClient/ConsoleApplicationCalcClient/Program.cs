using System;
using ConsoleApplicationCalcClient.ServiceReferenceCalculator;

namespace ConsoleApplicationCalcClient
{
    class Program
    {
        static void Main()
        {
            CalculatorClient calc = new CalculatorClient();
            Console.WriteLine(calc.CalcDistance(new Point() {X = 2, Y = 3}, new Point() {X = 3, Y = 3}));
        }
    }
}
