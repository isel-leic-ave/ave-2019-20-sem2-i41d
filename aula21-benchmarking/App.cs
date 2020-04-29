using System;

class App {
    static readonly Student s = new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1980,4,25));
    static readonly Student[] classroom = {
        new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1980,4,25)),
        new Student(765864, "Maria El", 4677, "ma", new DateTime(1956,11,26)),
        new Student(456757, "Antonias", 3153, "an", new DateTime(1997,5,28)),
    };
    static readonly IPrinter emptyPrt = new VoidPrinter();

    static void Main(){
        NBench.Bench(BenchLoggerReflection, "Logger via Reflection");
        NBench.Bench(BenchLoggerDynamic, "Logger Dynamic");
        /*
        LoggerDynamic.Log(s, new Printer());
        LoggerDynamic.Log(classroom, new Printer());
        Logger.Log(s, new Printer());
        Logger.Log(classroom, emptyPrt);
        */
    }

    static void BenchLoggerReflection() {
        Logger.Log(s, emptyPrt);
    }
    static void BenchLoggerDynamic() {
        LoggerDynamic.Log(s, emptyPrt);
    }
    
    class Printer : IPrinter {
        public void Write(object o) {
            Console.Write(o);
        }
        public void WriteLine(object o) {
            Console.WriteLine(o);
        }
    }

    class VoidPrinter : IPrinter {
        public void Write(object o) {}
        public void WriteLine(object o) {}
    }
}

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