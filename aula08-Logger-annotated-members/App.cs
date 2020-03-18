using System;

public class LogFormatterFirstName : LogFormatterAttribute {
    public override object Format(object val) {
        string name = (string) val;
        return name.Split(' ')[0];
    }
}

public class LogFormatterTruncate : LogFormatterAttribute {
    private readonly int decimals;
    public LogFormatterTruncate(int decimals) {
        this.decimals = decimals;
    } 
    public override object Format(object val) {
        double nr = (double) val;
        return Math.Round(nr, decimals);
    }
}

public class Student {
    [ToLog] public int Nr { get; set; }
    [ToLog][LogFormatterFirstName] public string Name{ get; set; }
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
    [ToLog] [LogFormatterTruncate(1)] public double Module => Math.Sqrt(x*x + y*y);
}

class Account {
    public static readonly int CODE = 4342;
    public long balance;
    public Account(long b) { balance = b; }
}

class App {
    static void Main(){
        Point p = new Point(7, 9);
        Student s = new Student(154134, "Ze Manel", 5243, "ze");
        Account a = new Account(1300);
        Student[] classroom = {
            new Student(154134, "Ze Manel", 5243, "ze"),
            new Student(765864, "Maria El", 4677, "ma"),
            new Student(456757, "Antonias", 3153, "an"),
        };


        Logger.Log(p);      
        Logger.Log(s);      
        Logger.Log(a);
        Logger.Log(classroom);
    }
}







