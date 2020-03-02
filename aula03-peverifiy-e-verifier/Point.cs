using System;


public class Point {
    public int x, y;
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public void Print() {
        Console.WriteLine("Point V1: ({0}, {1})", x, y);
    }
}