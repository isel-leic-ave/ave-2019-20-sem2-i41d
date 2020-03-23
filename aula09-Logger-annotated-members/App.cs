using System;

public class LogFormatterFirstNameAttribute : LogFormatterAttribute {
    public override object Format(object val) {
        string name = (string) val;
        return name.Split(' ')[0];
    }
}

public class LogFormatterTruncateAttribute : LogFormatterAttribute {
    private readonly int decimals;
    public LogFormatterTruncateAttribute(int decimals) {
        this.decimals = decimals;
    } 
    public override object Format(object val) {
        double nr = (double) val;
        return Math.Round(nr, decimals);
    }
}
public class LogFormatterYearAttribute : LogFormatterAttribute{
    public override object Format(object val) {
        DateTime d = (DateTime)val;
        return d.Year;
    }
}
public class Student {
    [ToLog] public int Nr { get; set; }
    [ToLog][LogFormatterFirstName] public string Name{ get; set; }
    public int Group{ get; set; }
    public string GithubId{ get; set; }
    [ToLog] [LogFormatterYear] public DateTime Birth { get; set; }

    public Student(int nr, string name, int group, string githubId, DateTime birth)
    {
        this.Nr = nr;
        this.Name = name;
        this.Group = group;
        this.GithubId = githubId;
        this.Birth = birth;
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
        Student s = new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1980,4,25));
        Account a = new Account(1300);
        Student[] classroom = {
            new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1980,4,25)),
            new Student(765864, "Maria El", 4677, "ma", new DateTime(1956,11,26)),
            new Student(456757, "Antonias", 3153, "an", new DateTime(1997,5,28)),
        };

        Logger.Log(p);      
        Logger.Log(s);      
        Logger.Log(a);
        Logger.Log(classroom);
    }
}







