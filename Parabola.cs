using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace MiniDesmos
{
    internal class Parabola
    {
        private double a;
        private double b;
        private double c;

        /*
           I activated feature in KDE plasma so when I press right ctrl and 2, it prints ², also it has ³ and ¹.
           Also can type «Text», „Text” and ∞, ° all sorts of useless bs 😏(emoji is from win + .)
        */

        /// <summary>
        /// Function will create <c>Parabola</c> function instance with 3 <b>paramers</b>: <b>a</b>, </b>b</b> and <b>c</b>
        /// </summary>
        /// <param name="a"><b>a parameter</b> for the function (the slope)</param>
        /// <param name="b"><b>b parameter</b> for the function</param>
        /// <param name="c"><b>c parameter</b> for the function</param>
        /// <remarks>0 if passed as <b>a parameter</b> will crash the function and cause an error</remarks>
        public Parabola(double a, double b, double c)
        {
            if (a != 0)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
        }

        /// <summary>
        /// Function will create <c>Parabola</c> function with <b>p</b>, and <b>k</b> parameters like in (x + <b>p</b>)² - <b>k</b> formula
        /// </summary>
        /// <param name="p"><b>p parameter</b>, horizontal shift of the vertex of the <c>parabola</c></param>
        /// <param name="k"><b>p parameter</b>, vertical shift of the vertex of the <c>parabola</c></param>
        public Parabola(double p, double k) : this(1, -2 * p, (p * p) + k) { }

        /// <summary>
        /// Function will find current's <c>Parabola</c> instance a <b>Y axis</b> interception point
        /// </summary>
        /// <returns>Current's <c>Parabola</c> instance <b>Y axis</b> interception point</returns>
        public Point Yintercept()
        {
            return new Point(0 ,this.c);
        }

        /// <summary>
        /// Function will find current's <c>Parabola</c> instance interception <c>Points</c> with <b>X axis</b>
        /// </summary>
        /// <returns>
        /// Array of <c>Points</c> where current <c>Parabola</c> instance intercepting with <b>X axis</b> If not intercepting returns <b><c>null</c></b>
        /// </returns>
        /// <remark> Should be used only with null checks if it's returning null or it will crash </remark>
        public Point[] Xintercept()
        {
            Point[] points = new Point[2];

            if ((Math.Pow(this.b, 2) - 4 * this.a * this.c) < 0)
            {
                points[0] = null;
                points[1] = null;
            }
            else if ((this.c - ((this.b * this.b) / (4 * this.a)) == 0))
            {
                points[0] = new Point(-this.b / (a * 2), 0);
                points[1] = null;
            }
            else
            {
                points[0] = new Point((-this.b + (Math.Sqrt((this.b * this.b) - (4 * this.a * this.c))) / (this.a * 2)), 0);
                points[1] = new Point((-this.b - (Math.Sqrt((this.b * this.b) - (4 * this.a * this.c))) / (this.a * 2)), 0);
            }

            return points;
        }

        /// <summary>
        /// Function prints all interception <c>Points<c/> of <b>parameter</b> <c>Parabola</c> instance with <b>X axis</b>
        /// </summary>
        /// <param name="parabola">Any <c>Parabula</c> instance/'function'</param>
        public static void PrintXintercept(Parabola parabola)
        {
            if (parabola.Xintercept()[0] != null)
            {
                Console.Write($"{parabola} has interception point with X axis: ");
                Console.Write(parabola.Xintercept()[0]);
                Console.Write(parabola.Xintercept()[1] != null ? $" and {parabola.Xintercept()[1]}\n" : "\n");
            }
            else
            {
                Console.WriteLine($"{parabola} has no interception points with X axis");
            }
        }

        /// <summary>
        /// Function will find <b>Y</b> coordinate that will be on <b>X</b> coordinate passed as <b>parameter</b> on current <c>Parabola</c> instance
        /// </summary>
        /// <param name="x"><b>X</b> coordinate for the <c>Point</c> which's <b>Y</b> coordinate function will find</param>
        public double GetY(double x)
        {
            return this.a * (x * x) + this.b * x + c;
        }

        /// <summary>
        /// Function will find thousand points on current <c>Parabola</c> instance
        /// </summary>
        /// <returns>Array of <c>Point</c> instances on <c>Parabola</c></returns>
        public Point[] GetPoints()
        {
            Point[] points = new Point[2000]; 
            double x = -10;
            for (int i = 0; i < 2000; i++)
            {
                points[i] = new Point(x, GetY(x));
                x += 0.01;
            }

            return points;
        }

        /// <summary>
        /// Function will provide <b>a parameter</b> of the current <c>Parabola</c> instance
        /// </summary>
        /// <returns><b>a parameter</b> of the current <c>Parabola</c> instance</returns>
        public double GetA()
        {
            return this.a;
        }

        /// <summary>
        /// Function will provide <b>b parameter</b> of the current <c>Parabola</c> instance
        /// </summary>
        /// <returns><b>b parameter</b> of the current <c>Parabola</c> instance</returns>
        public double GetB()
        {
            return this.b;
        }

        /// <summary>
        /// Function will provide <b>c parameter</b> of the current <c>Parabola</c> instance
        /// </summary>
        /// <returns><b>c parameter</b> of the current <c>Parabola</c> instance</returns>
        public double GetC()
        {
            return this.c;
        }

        /// <summary>
        /// Function will find out whether a <b>parameter</b> <c>Point</c> is on current <c>Parabola</c> instance, or not
        /// </summary>
        /// <param name="p"><c>Point</c> to find out if it's on current <c>Parabola</c> or not</param>
        /// <returns>Whether a <b>parameter</b> <c>Point</c> is on current <c>Parabola</c> instance, or not</returns>
        public bool IsOnParabola(Point p)
        {
            return p.GetY() == GetY(p.GetX());
        }

        /// <summary>
        /// Function will find the <b>vertex</b> of the current <c>Parabola</c> instance
        /// </summary>
        /// <returns><c>Point</c> instance of the <b>vertex</b> of the current <c>Parabola</c> instance</returns>
        public Point Extreme()
        {
            double p = (this.b / this.a) / -2;
            double k = this.c - (this.a * (p*p));
            return new Point(p, k);
        }

        /// <summary>
        /// Function will find <b>Tangent function</b> of the current <c>Parabola</c> instance on <b>X</b> coordinate that's passed as <b>parameter</b>
        /// </summary>
        /// <param name="X"><b>X</b> coordinate where to find the <b>Tangent</b></param>
        /// <returns><c>Line</c> instance of the <b>Tangent</b> function on the <b>X</b> coordinate of the current <c>Parabola</c> instance</returns>
        public Line Tangent(double x)
        {
            double m = (2 * this.a * x) + this.b;
            double b = ((this.a * (x*x)) + (this.b * x) + this.c) - (m * x);
            return new Line(m, b);
        }

        /// <summary>
        /// Function will find all intercept <c>Point</c>s of current <c>Parabola</c> instance with another <c>Line</c> instance that passed as <b>parameter</b>
        /// </summary>
        /// <param name="line"><c>Line</c> instance to check all <b>interception</b> <c>Point</c>s with current <c>Parabola</c> instance</param>
        /// <returns>Array of <c>Point</c>s where <b>parameter</b> <c>Line</c> instance is <b>intercepting</b> with current <c>Parabola</c> instance</returns>
        /// <remarks>
        /// Should be used with <c><b>null</b></c> checks for each element or use PrintInterceptLine function
        /// </remarks>
        public Point[] InterceptLine(Line line)
        {
            Point[] points = new Point[2];
            double b = this.b - line.GetSlope();
            double c = this.c - line.GetB();
            double x1;
            double x2;

            if ((b*b) - (4 * this.a * c) < 0)
            {
                points[0] = null;
                points[1] = null;
            }
            else if (line.GetSlope() == 0 && Extreme().GetY() == line.GetB())
            {
                points[0] = this.Extreme();
                points[1] = null;
            }
            else
            {
                x1 = (-b + Math.Sqrt((b*b) - (4 * this.a * c))) / (2 * this.a);
                x2 = (-b - Math.Sqrt((b*b) - (4 * this.a * c))) / (2 * this.a);
                points[0] = new Point(x1, (line.GetSlope() * x1) + line.GetB());
                points[1] = new Point(x2, (line.GetSlope() * x2) + line.GetB());
            }

            return points;
        }

        /// <summary>
        /// Function will safely print all <b>interception</b> <c>point</c>s between
        /// current <c>Parabola</c> instance and <c>Line</c> instance passed as parameter
        /// </summary>
        /// <param name="line"><c>Line</c> instance to check all <b>interception</b> <c>Point</c>s with current <c>Parabola</c> instance</param>
        public void PrintInterceptLine(Line line)
        {
            if (InterceptLine(line)[0] != null)
            {
                Console.Write($"{this} has interception point with {line}: ");
                Console.Write(InterceptLine(line)[0]);
                Console.Write(InterceptLine(line)[1] != null ? $" and {InterceptLine(line)[1]}\n" : "\n");
            }
            else
            {
                Console.WriteLine($"{this} has no interception points with {line}");
            }
        }

        /// <summary>
        /// Function will find <c>Line</c> that's perpendicular to given as <b>parameter</b> <c>Point</c> and
        /// to <c>Point</c> at <b>X coordinate parameter</b> on current <c>Parabola</c> instance
        /// </summary>
        /// <param name="p"><c>Point</c> that Perpendicular <c>Line</c> will go through</param>
        /// <param name="x">X coordinate of the <c>Point</c> on <c>Parabola</c> where the <b>tangent</b> that would be perpendicular to new <c>Line</c> </param>
        /// <returns>new <c>Line</c> instance that is perpendicular to <b>tangent</b> on <b>X coordinate</b>
        /// on current <c>Parabola</c> instance and also goes through <c>Point</c> that passed as <b>parameter</b></returns>
        public Line PerpendicularFromPoint(Point p, double x)
        {
            double m = Tangent(x).Perpendicular(new Point(x, GetY(x))).GetSlope();
            return new Line(m, p.GetY() - (m * p.GetX()));
        }

        /// <summary>
        /// Function will find all intercept <c>Point</c>s of current <c>Parabola</c> instance with another <c>Parabola</c> instance that passed as <b>parameter</b>
        /// </summary>
        /// <param name="Parabola"><c>Parabola</c> instance to check all <b>interception</b> <c>Point</c>s with current <c>Parabola</c> instance</param>
        /// <returns>Array of <c>Point</c>s where <b>parameter</b> <c>Parabola</c> instance is <b>intercepting</b> with current <c>Parabola</c> instance</returns>
        /// <remarks>
        /// Should be used with <c><b>null</b></c> checks for each element or use PrintInterceptParabola function
        /// </remarks>
        public Point[] InterceptParabola(Parabola par)
        {
            Point[] points = new Point[2];
            double a = this.a - par.GetA();
            double b = this.b - par.GetB();
            double c = this.c - par.GetC();
            double x1;
            double x2;

            if ((b*b) - (4 * a * c) < 0)
            {
                points[0] = null;
                points[1] = null;
            }
            else if (a == 0)
            {
                x1 = -c / b;
                points[0] = new Point(x1, GetY(x1));
                points[1] = null;
            }
            else if ((b*b) - (4 * a * c) == 0)
            {
                x1 = -b / (2 * a);
                points[0] = new Point(x1, par.GetY(x1));
                points[1] = null;
            }
            else
            {
                x1 = (-b + Math.Sqrt((b*b) - (4 * a * c))) / (2 * a);
                x2 = (-b - Math.Sqrt((b*b) - (4 * a * c))) / (2 * a);
                points[0] = new Point(x1, GetY(x1));
                points[1] = new Point(x2, GetY(x2));
            }

            return points;
        }

        /// <summary>
        /// Function will safely print all <b>interception</b> <c>point</c>s between
        /// current <c>Parabola</c> instance and <c>Parabola</c> instance passed as parameter
        /// </summary>
        /// <param name="par"><c>Parabola</c> instance to check all <b>interception</b> <c>Point</c>s with current <c>Parabola</c> instance</param>
        public void PrintInterceptParabola(Parabola par)
        {
            if (this.a == par.GetA() && this.b == par.GetB() && this.c == par.GetC())
            {
                Console.WriteLine($"{this} and {par} are the same Parabolas");
            }
            else if (InterceptParabola(par)[0] != null)
            {
                Console.Write($"{this} has interception point with {par}: ");
                Console.Write(InterceptParabola(par)[0]);
                Console.Write(InterceptParabola(par)[1] != null ? $" and {InterceptParabola(par)[1]}\n" : "\n");
            }
            else
            {
                Console.WriteLine($"{this} has no interception points with {par}");
            }
        }

        /// <summary>
        /// Function will find area of the triangle between current <c>Parabola</c> instance <b>X axis</b>
        /// </summary>
        /// <returns>
        /// An area between current <c>Parabola</c> instance and <b>X axis</b>
        /// If there is no interception <c>Points</c> between current <c>Parabola</c> and <b>X axis</b> function will return 0
        /// </returns>
        public double ExtremeArea()
        {
            double area = 0;
            Point xIntercept1 = Xintercept()[0];
            Point xIntercept2 = Xintercept()[1];
            if (xIntercept1 != null && xIntercept2 != null)
            {
                area = (Math.Abs(xIntercept2.GetX() - xIntercept1.GetX()) * Math.Abs(Extreme().GetY())) / 2;
            }

            return area;
        }

        /// <summary>
        /// Function will find area of the triangle between current <c>Parabola</c> instance and passed as <b>parameter</b> <c>Line</c> instance
        /// </summary>
        /// <param name="line"><c>Line</c> instance to check area between it and current <c>Parabola</c> instance</param>
        /// <returns>
        /// An area between current <c>Parabola</c> instance and <b>parameter</b> <c>Line</c> instance
        /// If there is no interception <c>Points</c> between current <c>Parabola</c> and <c>Line</c> function will return 0
        /// </returns>
        public double ExtremeArea(Line line)
        {
            double area = 0;
            double s, a, b, c;
            Point lineIntercept1 = InterceptLine(line)[0];
            Point lineIntercept2 = InterceptLine(line)[1];
            if (lineIntercept1 != null && lineIntercept2 != null)
            {
                a = lineIntercept1.Distance(lineIntercept2);
                b = lineIntercept1.Distance(Extreme());
                c = lineIntercept2.Distance(Extreme());
                s = (a + b + c) / 2;
                area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
            }

            return area;
        }

        /// <summary>
        /// When printing the instance of <c>Parabola</b> forses to print it in f(x) = ax² + bx + c format
        /// </summary>
        /// <returns>String with all <b>parameters</b> of <c>parabola</c> instance in f(x) = ax² + bx + c format</returns>
        public override string ToString()
        {
            return $"f(x) = {(this.a != 1 ? $"{this.a:0.##}" : "")}x²{this.b: + 0.##x; - 0.##x;''}{this.c: + 0.##; - 0.##;''}";
        }

        /// <summary>
        /// Prints current <c>Parabola</c> instance as vertext version of regular Parabola function
        /// f(x) = ax² + bx + c -> f(x) = (x - p)² + k
        /// </summary>
        /// <returns>A formatted string with (x - p)² + k function of current <c>Parabola</c> instance</returns>
        public string Vert() 
        {
            double p = (this.b / this.a) / -2;
            double k = this.c - (this.a * (p*p));
            return $"f(x) = (x{p: - 0.##; + 0.##;''})²{k: + 0.##; - 0.##;''}";
        }

        public static void UnitTest() 
        {
            Parabola parabola1 = new Parabola(1, 0, 0);
            Console.WriteLine(parabola1);

            Parabola parabola2 = new Parabola(3, -12, +20);
            Console.WriteLine(parabola2);

            Parabola parabola3 = new Parabola(3, 5);
            Console.WriteLine(parabola3);

            Parabola parabola4 = new Parabola(3, 0);
            Console.WriteLine(parabola4);

            Parabola parabola5 = new Parabola(0.1, 0, -10);

            Console.WriteLine($"Y intercepts: {parabola1.Yintercept()}, {parabola2.Yintercept()}, {parabola3.Yintercept()}, {parabola4.Yintercept()}, {parabola5.Yintercept()}");
            Console.WriteLine("Xintercepts:");

            PrintXintercept(parabola1);

            PrintXintercept(parabola2);

            PrintXintercept(parabola3);

            PrintXintercept(parabola4);

            PrintXintercept(parabola5);

            Console.WriteLine("GET Y:");
            Console.WriteLine(parabola1.GetY(5));
            Console.WriteLine(parabola2.GetY(5));
            Console.WriteLine(parabola3.GetY(5));
            Console.WriteLine(parabola4.GetY(5));
            Console.WriteLine(parabola5.GetY(5));

            Console.WriteLine("Is on parabola:");
            Console.WriteLine($"Is ( 1, 1 ) on {parabola1}: {parabola1.IsOnParabola(new Point(1, 1))}");
            Console.WriteLine($"Is ( 0, 1 ) on {parabola1}: {parabola1.IsOnParabola(new Point(0, 1))}");

            Console.WriteLine($"Is ( 2, 8 ) on {parabola2}: {parabola2.IsOnParabola(new Point(2, 8))}");
            Console.WriteLine($"Is ( 0, 1 ) on {parabola2}: {parabola2.IsOnParabola(new Point(0, 1))}");

            Console.WriteLine($"Is ( 4, 6 ) on {parabola3}: {parabola3.IsOnParabola(new Point(4, 6))}");
            Console.WriteLine($"Is ( 0, 1 ) on {parabola3}: {parabola3.IsOnParabola(new Point(0, 1))}");

            Console.WriteLine($"Is ( 2, 1 ) on {parabola4}: {parabola4.IsOnParabola(new Point(2, 1))}");
            Console.WriteLine($"Is ( 0, 1 ) on {parabola4}: {parabola4.IsOnParabola(new Point(0, 1))}");

            Console.WriteLine($"Is ( -20 30 ) on {parabola5}: {parabola5.IsOnParabola(new Point(-20, 30))}");
            Console.WriteLine($"Is ( 0, 1 ) on {parabola5}: {parabola5.IsOnParabola(new Point(0, 1))}");

            Console.WriteLine($"{parabola1} is on {parabola1.Extreme()}");
            Console.WriteLine($"{parabola2} is on {parabola2.Extreme()}");
            Console.WriteLine($"{parabola3} is on {parabola3.Extreme()}");
            Console.WriteLine($"{parabola4} is on {parabola4.Extreme()}");

            Console.WriteLine($"{parabola1} on g(4) has this tangent: {parabola1.Tangent(4)}");
            Console.WriteLine($"{parabola2} on g(4) has this tangent: {parabola2.Tangent(4)}");
            Console.WriteLine($"{parabola3.Vert()} on g(4) has this tangent: {parabola3.Tangent(4)}");
            Console.WriteLine($"{parabola4.Vert()} on g(4) has this tangent: {parabola4.Tangent(4)}");

            parabola1.PrintInterceptLine(new Line(0, 9));
            parabola1.PrintInterceptLine(new Line(5, 0));
            parabola1.PrintInterceptLine(new Line(0, 0));

            parabola2.PrintInterceptLine(new Line(0, 9));
            parabola2.PrintInterceptLine(new Line(5, 0));
            parabola2.PrintInterceptLine(new Line(0, 0));

            parabola3.PrintInterceptLine(new Line(0, 5));
            parabola3.PrintInterceptLine(new Line(5, 0));
            parabola3.PrintInterceptLine(new Line(0, 0));

            parabola4.PrintInterceptLine(new Line(0, 9));
            parabola4.PrintInterceptLine(new Line(5, 0));
            parabola4.PrintInterceptLine(new Line(0, 0));

            Console.WriteLine($"Parabola {parabola1} perpendicular in -3, 8 is {parabola1.PerpendicularFromPoint(new Point(-3, 8), 2)}");

            Console.WriteLine($"Parabola {parabola2} perpendicular in -3, 8 is {parabola2.PerpendicularFromPoint(new Point(-3, 8), 2)}");

            Console.WriteLine($"Parabola {parabola3} perpendicular in -3, 8 is {parabola3.PerpendicularFromPoint(new Point(-3, 8), 2)}");

            Console.WriteLine($"Parabola {parabola4} perpendicular in -3, 8 is {parabola4.PerpendicularFromPoint(new Point(-3, 8), 2)}");

            Console.WriteLine($"Parabola {parabola5} perpendicular in -3, 8 is {parabola4.PerpendicularFromPoint(new Point(-3, 8), 2)}");


            parabola1.PrintInterceptParabola(new Parabola(0.5, 0, 3));
            parabola1.PrintInterceptParabola(parabola1);
            parabola1.PrintInterceptParabola(parabola2);
            parabola1.PrintInterceptParabola(parabola3);
            parabola1.PrintInterceptParabola(parabola4);
            parabola1.PrintInterceptParabola(parabola5);

            Console.WriteLine(parabola1.ExtremeArea() != 0 ? $"Parabola {parabola1} area between X intercepts and Extreme point is: {parabola1.ExtremeArea()}" : $"Parabola {parabola1} doesn't have X intercepts and therefore no extreme area");
            Console.WriteLine(parabola2.ExtremeArea() != 0 ? $"Parabola {parabola2} area between X intercepts and Extreme point is: {parabola2.ExtremeArea()}" : $"Parabola {parabola2} doesn't have X intercepts and therefore no extreme area");
            Console.WriteLine(parabola3.ExtremeArea() != 0 ? $"Parabola {parabola3} area between X intercepts and Extreme point is: {parabola3.ExtremeArea()}" : $"Parabola {parabola3} doesn't have X intercepts and therefore no extreme area");
            Console.WriteLine(parabola4.ExtremeArea() != 0 ? $"Parabola {parabola4} area between X intercepts and Extreme point is: {parabola4.ExtremeArea()}" : $"Parabola {parabola4} doesn't have X intercepts and therefore no extreme area");
            Console.WriteLine(parabola5.ExtremeArea() != 0 ? $"Parabola {parabola5} area between X intercepts and Extreme point is: {parabola5.ExtremeArea()}" : $"Parabola {parabola5} doesn't have X intercepts and therefore no extreme area");

            Console.WriteLine(parabola1.ExtremeArea(new Line(4, 5)) != 0 ? $"Parabola {parabola1} area between {new Line(4, 5)} and Extreme point is: {parabola1.ExtremeArea(new Line(4, 5))}" : $"Parabola {parabola1} doesn't have two intercepts with {new Line(4, 5)} and therefore no extreme area");
            Console.WriteLine(parabola2.ExtremeArea(new Line(4, 5)) != 0 ? $"Parabola {parabola2} area between {new Line(4, 5)} and Extreme point is: {parabola2.ExtremeArea(new Line(4, 5))}" : $"Parabola {parabola2} doesn't have two intercepts with {new Line(4, 5)} and therefore no extreme area");
            Console.WriteLine(parabola3.ExtremeArea(new Line(4, 5)) != 0 ? $"Parabola {parabola3} area between {new Line(4, 5)} and Extreme point is: {parabola3.ExtremeArea(new Line(4, 5))}" : $"Parabola {parabola3} doesn't have two intercepts with {new Line(4, 5)} and therefore no extreme area");
            Console.WriteLine(parabola4.ExtremeArea(new Line(4, 5)) != 0 ? $"Parabola {parabola4} area between {new Line(4, 5)} and Extreme point is: {parabola4.ExtremeArea(new Line(4, 5))}" : $"Parabola {parabola4} doesn't have two intercepts with {new Line(4, 5)} and therefore no extreme area");
            Console.WriteLine(parabola5.ExtremeArea(new Line(4, 5)) != 0 ? $"Parabola {parabola5} area between {new Line(4, 5)} and Extreme point is: {parabola5.ExtremeArea(new Line(4, 5))}" : $"Parabola {parabola5} doesn't have two intercepts with {new Line(4, 5)} and therefore no extreme area");

        }
    }
}
