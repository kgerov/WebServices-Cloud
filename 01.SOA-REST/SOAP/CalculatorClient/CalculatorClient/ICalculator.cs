using System.Runtime.Serialization;
using System.ServiceModel;

namespace CalculatorService
{
    [ServiceContract]
    public interface ICalculator
    {

        [OperationContract]
        int Multi(int numA, int numB);

        [OperationContract]
        double CalcDistance(Point startPoint, Point endPoint);
    }


    [DataContract]
    public class Point
    {
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }
    }
}
