using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
class ColorAttribute : Attribute {
    public string myColor;
    public ColorAttribute() { this.myColor = "green"; }
    public ColorAttribute(string mc) { this.myColor = mc; }
}

class App {
    static void Main(){
        Student s = new Student(154134, "Ze Manel", 5243, "ze");
        object [] attrs= s.GetType().GetCustomAttributes(typeof(ColorAttribute), true);
        foreach(object att in attrs)
            Console.WriteLine(((ColorAttribute) att).myColor);

    }
}

[Color("pink")]
[Color("blue")]
public class Student {
    [Color("red")]
    public int Nr { get; set; }
    [Color]
    public string Name{ get; set; }
    public int Group{ get; set; }
    public string GithubId{ get; set; }

    public Student(int nr, string name, int group, string githubId)
    {
        this.Nr = nr;
        this.Name = name;
        this.Group = group;
        this.GithubId = githubId;
    }

}

class Point{
    readonly int x, y;
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public int X { get {return x; }}
    public int Y => y;
    public double Module => Math.Sqrt(x*x + y*y);
}

class Account {
    public static readonly int CODE = 4342;
    public long balance;
    public Account(long b) { balance = b; }
}






