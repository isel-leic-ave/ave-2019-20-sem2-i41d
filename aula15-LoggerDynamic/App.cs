using System;

public class Student {
    [ToLog] public int nr;
    [ToLog] public string name;
    [ToLog] public int group;
    public string githubId;
    public DateTime birth;
    public Student(int nr, string name, int group, string githubId, DateTime birth) {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.githubId = githubId;
        this.birth = birth;
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
    [ToLog] public double Module => Math.Sqrt(x*x + y*y);
}

class Account {
    public static readonly int CODE = 4342;
    public long balance;
    public Account(long b) { balance = b; }
}

class App {
    static void Main(){
        Point p = new Point(7, 9);
        Student s = new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1980,4,25));
        Account a = new Account(1300);
        Student[] classroom = {
            new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1980,4,25)),
            new Student(765864, "Maria El", 4677, "ma", new DateTime(1956,11,26)),
            new Student(456757, "Antonias", 3153, "an", new DateTime(1997,5,28)),
        };

        LoggerDynamic.Log(p);      
        LoggerDynamic.Log(s);      
        LoggerDynamic.Log(a);
        LoggerDynamic.Log(classroom);
    }
}







