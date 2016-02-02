
public class Point2  {
    public int x;
    public int y;
    public static Point2 NORTH = new Point2(0, 1);
    public static Point2 SOUTH = new Point2(0, -1);
    public static Point2 EAST = new Point2(1, 0);
    public static Point2 WEST = new Point2(-1, 0);

    int[] vec;

    public Point2() : this(0, 0)
    {
        
    }

    public Point2(int x, int y)
    {
        this.x = x;
        this.y = y;
        vec = new int[] { x, y };
    }
}
