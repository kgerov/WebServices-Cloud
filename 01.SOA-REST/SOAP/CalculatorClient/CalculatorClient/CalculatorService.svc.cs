using System;
using System.Web.Services.Protocols;
using System.Xml;

namespace CalculatorService
{
    public class CalculatorService : ICalculator
    {
        public int Multi(int numA, int numB)
        {
            if (numA < 0 || numB < 0)
            {
                throw new SoapException("Negative numbers not implemented. (test error)", 
                    new XmlQualifiedName("TestError"));
            }
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
