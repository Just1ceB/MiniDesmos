using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniDesmos
{
    internal class Line
    {
        public static readonly Line XAxis = new Line(0, 0);

        private double m; // Slope variable
        private double b; // Intercept with y axis in function (y pos, cuz x=0)

        /// <summary>
        /// Creates <c>Line</c> instance (function) with it's slope and <b>b</b> parameters, as <b>Parameters</b>
        /// </summary>
        /// <param name="m"><b>Slope</b> of the <c>Line</c></param>
        /// <param name="b"><b>b</b> parameter of the <c>Line</c></param>
        public Line(double m, double b)
        {
            this.m = m;
            this.b = b;
        }

        /// <summary>
        /// Creates y = x <c>Line</c> instance (function) without any <b>parameters</b>
        /// </summary>
        public Line() : this(1,0) { }

        /// <summary>
        /// Creates <c>Line</c> instance with two points passed as <b>parameters<b>
        /// </summary>
        /// <param name="p1"><c>Point</c> instance on the new <c>Line</c> instance</param>
        /// <param name="p2"><c>Point</c> instance on the new <c>Line</c> instance</param>
        public Line(Point p1, Point p2) : this(p1.Slope(p2), p1.GetY() - (p1.Slope(p2) * p1.GetX())) { }

        /// <summary>
        /// Function will provide current <c>Line</c> instance's slope, the m parameter
        /// </summary>
        /// <returns>Slope (m parameter) of the current <c>Line</c> instance (function)</returns>
        public double GetSlope()
        {
            return this.m;
        }

        /// <summary>
        /// Function will provide current <c>Line</c> instance's b parameter
        /// </summary>
        /// <returns>B parameter of the current <c>Line</c> instance (function)</returns>
        public double GetB()
        {
            return this.b;
        }

        /// <summary>
        /// Function will find out what <b>Y</b> coordinate, point with <b>X parameter</b> on the <c>Line</c> instance (function)
        /// </summary>
        /// <param name="x">X coordinate of the <c>Point</c> on the <c>Line</c></param>
        /// <returns><b>Y</b> coordinate of point on the <c>Line</c> instance with <b>X parameter</b> coordinate</returns>
        public double GetY(double x)
        {
            return (this.m * x) + b;
        }

        /// <summary>
        /// Function will find thousand points on current <c>Line</c> instance
        /// </summary>
        /// <returns>Array of <c>Point</c> instances on <c>Line</c></returns>
        public Point[] GetPoints()
        {
            Point[] points = new Point[1000];
            for (int i = 0; i < 1000; i++)
            {
                points[i] = new Point(i, GetY(i));
            }

            return points;
        }

        /// <summary>
        /// Function will check if <b>parameter</b> <c>Point</c> instance is on current <c>Line</c> instance (function)
        /// </summary>
        /// <param name="p"><c>Point</c> instance to check if it's on the current <c>Line</c> instance</param>
        /// <returns>Returns whether is the <c>Point</c> on the <c>Line</c> or not</returns>
        public bool IsOnLine(Point p)
        {
            return p.GetY() == GetY(p.GetX());
        }

        /// <summary>
        /// Function will find current <c>Line</c>'s instance interception <c>Point</c> with <b>Y</b> axis
        /// </summary>
        /// <returns>Interception <c>Point</c> of current <c>Line</c> instance (function) with <b>Y</b> axis</returns>
        public Point Yintercept()
        {
            return new Point(0, this.b);
        }

        /// <summary>
        /// Function will find current <c>Line</c>'s instance interception <c>Point</c> with <b>X</b> axis
        /// </summary>
        /// <remarks>
        /// If <c>Line</c> instance (function) doesn't have interception point with <b>X</b> axis function will return NULL
        /// </remarks>
        /// <returns>Interception <c>Point</c> of current <c>Line</c> instance (function) with <b>X</b> axis</returns>
        public Point Xintercept()
        {
            return m == 0 ? null : new Point(-this.b / this.m, 0);
        }

        /// <summary>
        /// Function will check whether current <c>Line</c> is parallel, to other <c>Line</c>, intersects with it, or the <c>Lines</c> are the same
        /// </summary>
        /// <param name="line">Other <c>Line</c> instace to check with it</param>
        /// <returns>
        /// 0 if the <c>Lines</c> are parallel
        /// 1 if the <c>Lines</c> intercenting with each other
        /// -1 if the <c>Lines</c> are the same
        /// </returns>   
        public int LineStatus(Line line)
        {
            if (this.m == line.GetSlope() && this.b == line.GetB()) // Same parameters == Same Lines (functions)
            {
                return -1;
            }
            else if (this.m == line.GetSlope()) // If the slopes are the same lines are parallel
            {
                return 0;
            }
            else // If lines have different slopes and different b parameters, they are 100% intercenting
            {
                return 1;
            }
        }

        /// <summary>
        /// Function will find interception <c>Point</c> between current <c>Line</c> instance and parameter <c>Line</c> instance
        /// </summary>
        /// <param name="line">Other <c>Line</c> instance to find interception <c>Point</c> with it</param>
        /// <returns>Interception <c>Point</c> between two <c>Line</c> instances</returns>
        public Point LineIntercept(Line line)
        {
            if (LineStatus(line) != 1)
            {
                return null;
            }
            else
            {
                double x = (line.GetB() - this.b) / (this.m - line.GetSlope());
                return new Point(x, line.GetY(x));
            }
        }

        /// <summary>
        /// Function will create perpendicular <c>Line</c> instance to the current <c>Line</c> instance
        /// </summary>
        /// <param name="p"><c>Point</c> instance where the other <c>Line</c> will intercept with current <c>Line</c> instance</param>
        /// <returns><c>Line</c> instance that is perpendicular to the current <c>Line</c> instance</returns>
        public Line Perpendicular(Point p)
        {
            double m = -1.0 / this.m;
            return new Line(m, p.GetY() - (m * p.GetX()));
        }

        /// <summary>
        /// Function will find a shorted distance between <b>parameter</b> <c>Point</c> instance and between current <c>Line</c> instance
        /// </summary>
        /// <param name="p"><c>Point</c> instance to find it's distance from current <c>Line</c> instance</param>
        /// <returns>Distance between current <c>Line</c> instance and <b>parameter</b> <c>Point</c> instance</returns>
        public double DistanceFromPoint(Point p)
        {
            Line pointLine = new Line(this.m, p.GetY() - (this.m * p.GetX()));
            return p.Distance(pointLine.Perpendicular(p).LineIntercept(this));
        }

        /// <summary>
        /// Function will find out whether is a combination of current <c>Line</c> instance and <b>parameter</b> <c>Line</c> instance and <b>X Axis</b> could form a valid triangle
        /// </summary>
        /// <param name="line"><c>Line</c> instance to check if currenet <c>Line</c> instance can have a triangle between them</param>
        /// <returns>Whether triangle is possible between current and <b>parameter</b> <c>line</c> instances</returns>
        public bool IsTrianglePossible(Line line)
        {
            return !(LineStatus(line) == -1 || LineStatus(line) == 0 || LineStatus(XAxis) == 0 || LineStatus(XAxis) == -1 || line.LineStatus(XAxis) == 0 || line.LineStatus(XAxis) == -1);
        }

        /// <summary>
        /// Function will find area of the triangle between current <c>Line</c> instance, <b>parameter</b> <c>Line</c> instance and <b>X Axis</b>
        /// </summary>
        /// <param name="line">Other <c>Line</c> instance that makes triangle between current <c>Line</c> instance and <b>X Axis</b></param>
        /// <returns>Area of the triangle between current, <b>parameter</b> <c>Line</c> instance and <b>X Axis</b></returns>
        public double AreaWithX(Line line)
        {
            return IsTrianglePossible(line) ? (Math.Abs(Xintercept().GetX() - line.Xintercept().GetX()) * Math.Abs(LineIntercept(line).GetY())) / 2.0 : 0;
        }

        /// <summary>
        /// Function will find angles in triangle between <b>X Axis</b>, current <c>Line</c> instance and <b>parameter</b> line instance
        /// </summary>
        /// <param name="line">Other <c>Line</c> instance that makes triangle between current <c>Line</c> instance and <b>X Axis</b></param>
        /// <returns>Array of angles in triangle between <b>X Axis</b>, current <c>Line</c> instance and <b>parameter</b> line instance</returns>
        public double[] TriangleAnglesWithX(Line line)
        {
            double[] triangleAngles = new double[3];
            double dist;
            
            if (IsTrianglePossible(line))
            {
                if (this.m > 0 && line.GetSlope() > 0 || this.m < 0 && line.GetSlope() < 0)
                {
                    dist = Math.Max(Math.Abs(LineIntercept(line).GetX() - Xintercept().GetX()),
                            Math.Abs(LineIntercept(line).GetX() - line.Xintercept().GetX()));

                    triangleAngles[0] = Math.Atan(LineIntercept(line).GetY() / dist) * (180/Math.PI);

                    dist = Math.Min(Math.Abs(LineIntercept(line).GetX() - Xintercept().GetX()),
                            Math.Abs(LineIntercept(line).GetX() - line.Xintercept().GetX()));

                    triangleAngles[2] = Math.Atan(LineIntercept(line).GetY() / dist) * (180/Math.PI);
                    triangleAngles[1] = 180 - triangleAngles[0] - triangleAngles[2];
                }
                else
                {
                    dist = Math.Abs(LineIntercept(line).GetX() - Xintercept().GetX());
                    triangleAngles[0] = Math.Abs(Math.Atan(LineIntercept(line).GetY() / dist) * (180/Math.PI));
                    dist = Math.Abs(LineIntercept(line).GetX() - line.Xintercept().GetX());
                    triangleAngles[2] = Math.Abs(Math.Atan(LineIntercept(line).GetY() / dist) * (180/Math.PI));
                    triangleAngles[1] = 180 - triangleAngles[0] - triangleAngles[2];
                }

                return triangleAngles;

            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Function will force to print <c>Line</c> instance in f(x) = mx + b format when printing <c>Line</c> instance
        /// </summary>
        /// <returns>A string with f(x) = mx + b data about the <c>Line</c> instance (function)</returns>
        public override string ToString()
        {
            return b < 0 ? $"f(x) = {this.m:0.##}x {this.b:0.##}" : $"f(x) = {this.m:0.##}x + {this.b:0.##}";
        }

        /// All double checked with demos :D
        public static void UnitTest()
        {
            Line gX = new Line(new Point(2, 4), new Point(7, 8)); // WORKS
            Line interceptorG = new Line(1, 2);

            Console.WriteLine(gX);

            Console.WriteLine($"The Y intercept Point: {gX.Yintercept()}"); // WORKS :D

            Console.WriteLine($"The X intercept Point: {gX.Xintercept()}"); // WORKS

            Console.WriteLine($"The slope of the function is {gX.GetSlope()}"); // WORKS
            Console.WriteLine($"The Y coordinate when X is 4.2, is: {gX.GetY(4.2)}"); // WORKS

            Console.WriteLine($"The point ( 9.5, 10 ) is on the line?: {gX.IsOnLine(new Point(9.5, 10))}"); // WORKS
            Console.WriteLine($"The point ( 8, 10 ) is on the line?: {gX.IsOnLine(new Point(8, 10))}"); // WORKS

            Console.WriteLine();

            Console.WriteLine($"{gX}\nSame Line: {gX.LineStatus(gX)}"); // -1
            Console.WriteLine($"{new Line(0.8, 1)}\nParallels: {gX.LineStatus(new Line(0.8, 1))}"); // 0
            Console.WriteLine($"{interceptorG}\nIntercepting: {gX.LineStatus(interceptorG)}"); // 1

            Console.WriteLine($"Intercept point between {interceptorG} and {gX} is {gX.LineIntercept(interceptorG)}");

            Console.WriteLine($"Perpendicular function for {gX} is {gX.Perpendicular(new Point(9.5, 10))}");

            Console.WriteLine($"Distance between {gX} and ( 8, 10 ) is {gX.DistanceFromPoint(new Point(8, 10)):0.##}");

            Console.WriteLine($"Triangle between {gX} and {interceptorG} has area of {gX.AreaWithX(interceptorG):0.##}");

            Console.WriteLine($"Triangle angles are: {gX.TriangleAnglesWithX(interceptorG)[0]:0.##}, {gX.TriangleAnglesWithX(interceptorG)[1]:0.##} and {gX.TriangleAnglesWithX(interceptorG)[2]:0.##}");
            
            Console.WriteLine("Other Triangle:");
            
            Line fX = new Line(1, 2);
            Line interceptorF = new Line(-1, 0);

            Console.WriteLine(fX);
            Console.WriteLine(interceptorF);

            Console.WriteLine($"Triangle angles are: {fX.TriangleAnglesWithX(interceptorF)[0]:0.##}, {fX.TriangleAnglesWithX(interceptorF)[1]:0.##} and {fX.TriangleAnglesWithX(interceptorF)[2]:0.##}");

            Console.WriteLine("Other Triangle:");
            Line hX = new Line(1, -5);
            Line interceptorH = new Line(-1, 0);

            Console.WriteLine(hX);
            Console.WriteLine(interceptorH); 

            Console.WriteLine($"Triangle angles are: {hX.TriangleAnglesWithX(interceptorH)[0]:0.##}, {hX.TriangleAnglesWithX(interceptorH)[1]:0.##} and {hX.TriangleAnglesWithX(interceptorH)[2]:0.##}");
        }
    }
}
