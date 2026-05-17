using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniDesmos
{
    internal class Point
    {
        public static readonly Point Origin = new Point();

        private double x;
        private double y;


        /// <summary>
        /// Creates a <c>Point</c> instance using input <b>X</b> and <b>Y</b> coordinates
        /// </summary>
        /// <param name="x"><b>X</b> coordinate in <c>Point</c> instance</param>
        /// <param name="y"><b>Y</b> coordinate in <c>Point</c> instance</param>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a <c>Point</c> instance at (0, 0) coordinates
        /// </summary>
        public Point() : this(0, 0) { }

        /// <summary>
        /// Creates a copy of a <c>Point</c> instance that gets as <b>parameter</b>
        /// </summary>
        /// <param name="p"><c>Point</c> instance to copy</param>
        public Point(Point p) : this(p.GetX(), p.GetY()) { }

        /// <summary>
        /// Gets <b>X</b> coordinate from current <c>Point</c> instance
        /// </summary>
        /// <returns><b>X</b> coordinate from current <c>Point</c> instance</returns>
        public double GetX()
        {
            return this.x;
        }

        /// <summary>
        /// Sets input <b>parameter</b> as <b>X</b> coordinate for current <c>Point</c> instance
        /// </summary>
        /// <param name="x"><b>X</b> coordinate to set</param>
        public void SetX(double x)
        {
            this.x = x;
        }

        /// <summary>
        /// Gets <b>Y</b> coordinate from current <c>Point</c> instance
        /// </summary>
        /// <returns><b>Y</b> coordinate from current <c>Point</c> instance</returns>
        public double GetY()
        {
            return this.y;
        }

        /// <summary>
        /// Sets input <b>parameter</b> as <b>Y</b> coordinate for current <c>Point</c>
        /// </summary>
        /// <param name="y"></param>
        public void SetY(double y)
        {
            this.y = y;
        }

        /// <summary>
        /// Calculates between current <c>Point</c> instance to <b>parameter</b> <c>Point</c> instance
        /// </summary>
        /// <param name="p">Other <c>Point</c> instance</param>
        /// <returns>Distance between current <c>Point</c> and <b>parameter</b> <c>Point</c> instance</returns>
        public double Distance(Point p)
        {
            return Math.Sqrt((Math.Pow(this.x - p.GetX(), 2)) + (Math.Pow(this.y - p.GetY(), 2)));
        }

        /// <summary>
        /// Creates a point between current <c>Point</c> instance and <b>parameter</b> <c>Point</c> instance
        /// </summary>
        /// <param name="p">Other <c>Point</c> instance</param>
        /// <returns>New <c>Point</c> instance that is between current and parameter <c>Point</c> instances</returns>
        public Point MiddlePoint(Point p)
        {
            Point middlePoint = new Point((this.x + p.GetX()) / 2, (this.y + p.GetY()) / 2);
            return middlePoint;
        }

        /// <summary>
        /// Calculates a slope between current <c>Point</c> instance and <b>parameter</b> <c>Point</c> instance 
        /// </summary>
        /// <param name="p">Other <c>Point</c> instance</param>
        /// <returns>Slope between current <c>Point</c> instance and <b>parameter</b> <c>Point</c> instance </returns>
        public double Slope(Point p)
        {
            return (p.GetY() - this.y) / (p.GetX() - this.x);
        }

        /// <summary>
        /// Finds out which <c>Point</c> instance is farthest from the 0,0 coordinates (Origin) from array of <c>Point</c> instances
        /// </summary>
        /// <param name="points">Array of <c>Point</c> instances to find out which is farthest from the origin (center dot (0,0)) </param>
        public static Point FarthestPoint(Point[] points)
        {
            double largestDistance, currentDistance; 
            int lrgDistIndex;

            largestDistance = points[0].Distance(Origin);
            lrgDistIndex = 0;
            for (int i = 1; i < points.Length; i++)
            {
                currentDistance = points[i].Distance(Origin);
                if (currentDistance > largestDistance)
                {
                    largestDistance = currentDistance;
                    lrgDistIndex = i;
                }
            }

            return points[lrgDistIndex];
        }

        /// <summary>
        /// Creates array of <c>Point</c> instances that are in the middle of each <c>Point</c> instance in <b>parameter</b> array<br/>
        /// and origin (0,0 coords)
        /// </summary>
        /// <param name="points">Array of <c>Point</c> instances to find middle of them and origin (0,0 coords)</param>
        public static Point[] HalfPoints(Point[] points)
        {
            Point[] middlePoints = new Point[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                middlePoints[i] = points[i].MiddlePoint(Origin);	
            }

            return middlePoints;
        }

        /// <summary>
        /// Creates a copy of current <c>Point</c> instance
        /// </summary>
        /// <returns>New <c>Point</c> instance that is a copy of a current one</returns>
        public Point Copy()
        {
            Point copy = new Point(this.x, this.y);
            return copy;
        }

        /// <summary>
        /// Makes printing <c>Point</c> instance to print string with the point coordinates <br/>
        /// <example>
        /// Example: ( 0, 0 )
        /// </example>
        /// </summary>
        public override string ToString()
        {
            return $"( {this.x:0.##;-0.##;0}, {this.y:0.##;-0.##;0} )".ToString();
        }

        public static void UnitTest()
        {
            Point point = new Point(14.24, 15.69);

            Console.WriteLine($"Point coordinates is: {point}");

            Point point2 = new Point();

            Console.WriteLine(point2.GetY());

            Console.WriteLine($"New point coordinates is: {point2}");

            Console.WriteLine($"Distance is: {point2.Distance(point)}");

            Point point3 = point.MiddlePoint(point2);
            Console.WriteLine($"Middle point is: {point3}");

            Point point4 = new Point(point);

            Point point5 = new Point(2, 4);
            Point point6 = new Point(7, 8);

            Console.WriteLine($"The slope between point {point5} and {point6} is {point5.Slope(point6)}");

            Point[] points = {point, point2, point3};

            Console.WriteLine($"Farthest point from origin (( 0, 0 ) coords) is {Point.FarthestPoint(points)}. (comparing {point},{point2},{point3}");

            Point[] halfPoints = Point.HalfPoints(points);
            Console.Write($"Halfed points are: [");
            for (int i = 0 ; i < points.Length; i++)
            {
                Console.Write(halfPoints[i].ToString());
            }
            Console.Write("]");
        }
    }
}
