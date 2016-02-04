using UnityEngine;
public class Point2  {
    public int x;
    public int y;
    public static Point2 NORTH = new Point2(0, 1);
    public static Point2 SOUTH = new Point2(0, -1);
    public static Point2 EAST = new Point2(1, 0);
    public static Point2 WEST = new Point2(-1, 0);
    public static Point2 ZERO = new Point2();

    public Point2() : this(0, 0)
    {
        
    }

    public Point2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void normalizeValues()
    {
        if (x != 0)
        {
            x /= ((int)Mathf.Abs(x));
        }
        if (y != 0)
        {
            y /= (int)Mathf.Abs(y);
        }
    }

    

    public static Point2 operator + (Point2 p1, Point2 p2)
    {
        return new Point2(p1.x + p2.x, p1.y + p2.y);
    }

    public static Point2 operator - (Point2 p1, Point2 p2)
    {
        return new Point2(p1.x - p2.x, p1.y - p2.y);
    }

    public static Point2 operator - (Point2 p)
    {
        return new Point2(-p.x, -p.y);
    }

    public static Point2 operator * (Point2 p, int i)
    {
        return new Point2(p.x * i, p.y * i);
    }

    public static Point2 operator * (int i, Point2 p)
    {
        return p * i;
    }

    public static Point2 operator / (Point2 p1, int i)
    {
        return new Point2(p1.x / i, p1.y / i);
    }

    public static bool operator == (Point2 p1, Point2 p2)
    {
        return p1.x == p2.x && p1.y == p2.y;
    }

    public static bool operator != (Point2 p1, Point2 p2)
    {
        return p1.x != p2.x || p1.y != p2.y;
    }

    public override string ToString()
    {
        return "(X = " + x + ", Y = " + y + ")"; 
    }
}
