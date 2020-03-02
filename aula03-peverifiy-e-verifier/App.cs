using System; // <=> import java.lang.*;

class App { // <=> private
    static void Main() {
        Point p = new Point(3, 7);
        p.Print();
        Console.WriteLine("x: " + p.x);
        Console.WriteLine("y: " + p.y);
        Console.WriteLine("z: " + p.y);
    }
}