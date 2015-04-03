using System;

namespace CalculatorService
{
    public class CalculatorService : ICalculator
    {
        public int Multi(int numA, int numB)
        {
            return numA*numB;
        }

        public double CalcDistance(Point startPoint, Point endPoint)
        {
            double powDeltaX = Math.Pow((startPoint.X - endPoint.X), 2);
            double powDeltaY = Math.Pow((startPoint.Y - endPoint.Y), 2);

            return Math.Sqrt(powDeltaX + powDeltaY);
        }
    }
}
