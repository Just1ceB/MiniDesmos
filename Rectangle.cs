using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Prjcts
{
    internal class Rectangle
    {
        private Point bottomLeft;
        private Point topRight;

        /// <summary>
        /// Creates a <c>Rectangle</c> instance by taking bottom left corner and top right left corner <c>Point</c> instances as <b>parameters</b>
        /// </summary>
        /// <param name="bottomLeft">Bottom left corner <c>Point</c> instance coordinates</param>
        /// <param name="topRight">Top right corner <c>Point</c> instance coordinates</param>
        public Rectangle(Point bottomLeft, Point topRight)
        {
            this.bottomLeft = bottomLeft;
            this.topRight = topRight;
        }

        /// <summary>
        /// Creates new <c>Rectangle</c> instance by taking bottom left corner <c>Point</c> instance  and rectangle width and height as <b>parameters</b>
        /// </summary>
        /// <param name="bottomLeft">Botton left corner <c>Point</c> instance coordinates</param>
        /// <param name="width">Rectangle width</param>
        /// <param name="height">Rectangle height</param>
        public Rectangle(Point bottomLeft, double width, double height)
        {
            this.bottomLeft = bottomLeft;
            this.topRight = new Point(this.bottomLeft.GetX() + width, this.bottomLeft.GetY() + height);
        }

        /// <summary>
        /// Get's current <c>Rectangle</c> instance bottom left point
        /// </summary>
        /// <returns>Bottom left point of current <c>Rectangle</c> instance</returns>
        public Point GetBottomLeft()
        {
            return this.bottomLeft;
        }

        /// <summary>
        /// Get's current <c>Rectangle</c> instance top right point
        /// </summary>
        /// <returns>Top right point of current <c>Rectangle</c> instance</returns>
        public Point GetTopRight()
        {
            return this.topRight;
        }

        /// <summary>
        /// Calculates width of the current <c>Rectangle</c> instance
        /// </summary>
        /// <returns>Width of a <c>Rectangle</c> instance</returns>
        public double GetWidth()
        {
            return this.topRight.GetX() - this.bottomLeft.GetX();
        }

        /// <summary>
        /// Calculates height of the current <c>Rectangle</c> instance
        /// </summary>
        /// <returns>Height of a <c>Rectangle</c> instance</returns>
        public double GetHeight()
        {
            return this.topRight.GetY() - this.bottomLeft.GetY();
        }

        /// <summary>
        /// Calculates are of the current <c>Rectangle</c> instance
        /// </summary>
        /// <returns>Current <c>Rectangle</c> instance area</returns>
        public double GetArea()
        {
            return GetHeight() * GetWidth();
        }

        /// <summary>
        /// Calculates perimeter of the current <c>Rectangle</c> instance
        /// </summary>
        /// <returns>Current <c>Rectangle</c> instance perimeter</returns>
        public double GetPerimeter()
        {
            return (GetHeight() * 2) + (GetWidth() * 2);
        }

        /// <summary>
        /// Moves current <c>Rectangle</c> instance by <c>deltaX</c> in <b>X</b> axis and by <c>deltaY</c> in <b>Y</b> axis
        /// </summary>
        /// <param name="deltaX">The distance to move the <c>Rectangle</c> instance in <b>X</b> axis</param>
        /// <param name="deltaY">The distance to move the <c>Rectangle</c> instance in <b>Y</b> axis</param>
        public void Move(double deltaX, double deltaY)
        {
            this.bottomLeft.SetX(this.bottomLeft.GetX() + deltaX);
            this.bottomLeft.SetY(this.bottomLeft.GetY() + deltaY);
            this.topRight.SetX(this.topRight.GetX() + deltaX);
            this.topRight.SetY(this.topRight.GetY() + deltaY);
        }

        /// <summary>
        /// Checks if <b>parameter</b> <c>Point</c> instance is within borders of current <c>Rectangle</c> instance
        /// </summary>
        /// <returns>Whether is <b>parameter</b> point is within current <c>Rectangle</c></returns>
        public bool PointIsIn(Point point)
        {
            return point.GetY() >= this.bottomLeft.GetY() && point.GetY() <= this.topRight.GetY() && point.GetX() >= this.bottomLeft.GetX() && point.GetX() <= this.topRight.GetX();
        }

        /// <summary>
        /// Calculates distance between current <c>Rectangle</c> instance middle point to <b>Parameter</b> <c>Rectangle</c> instance middle point
        /// </summary>
        /// <returns>Distance between two <c>Rectangle</c> instances middle points</returns>
        public double Distance(Rectangle other)
        {
            return this.bottomLeft.MiddlePoint(this.topRight).Distance(other.GetBottomLeft().MiddlePoint(other.GetTopRight()));
        }

        /// <summary>
        /// Makes printing <c>Rectangle</c> instance to print a string with <c>Rectangle</c> instance info as: <br/>
        /// <example>
        /// Rectangle:<br/>
        /// bottom-left point = ( 0, 0 )<br/>
        /// topright point = ( 15, 15 )
        /// </example>
        /// </summary>
        public override string ToString()
        {
            return $"Rectangle:\nbottom-left point = {bottomLeft}\ntop-right point = {topRight}".ToString();
        }
        public static void UnitTest()
        {
            Point point1 = new Point(0, 0);
            Point point2 = new Point(15, 10);

            Rectangle rect = new Rectangle(point1, point2);

            Console.WriteLine(rect.GetArea());
            Console.WriteLine(rect.GetPerimeter());

            Console.WriteLine(rect);

            Console.WriteLine("Moving rectangle:");
            rect.Move(10, 10);
            Console.WriteLine(rect);

            rect.Move(10, 0);

            Console.WriteLine(rect.PointIsIn(point2) ? $"{point2} is in rectangle" : $"{point2} Not in rectangle");
            Console.WriteLine(rect.PointIsIn(new Point(1,1)) ? $"{new Point(1,1)} is in rectangle" : $"{new Point(1,1)} Not in rectangle");

            Console.WriteLine(rect);
            Rectangle rect2 = new Rectangle(new Point(25,10), new Point(40, 20));

            Console.WriteLine(rect2);

            Console.WriteLine(rect.Distance(rect2));
        }
    }
}
