using System;
using System.Collections;
using System.Text;
using System.IO;


public interface Predicate { bool Invoke(object target); }
public interface Function { object Invoke(object target); }

class App {

    static IEnumerable Lines(string path)
    {
        string line;
        IList res = new ArrayList();
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
     
    static IEnumerable Convert(IEnumerable src, Function mapper) {
        IList res = new ArrayList();
        IEnumerator iter = src.GetEnumerator();
        while (iter.MoveNext())
        {
            res.Add(mapper.Invoke(iter.Current));
        }
        return res;
    }
    
    static IEnumerable Filter(IEnumerable src, Predicate pred) {
        IList res = new ArrayList();
        IEnumerator iter = src.GetEnumerator();
        while (iter.MoveNext())
        {
            if(pred.Invoke(iter.Current))
                res.Add(iter.Current);
        }
        return res;
    }
    static IEnumerable Distinct(IEnumerable src) {
        ArrayList res = new ArrayList();
        IEnumerator iter = src.GetEnumerator();
        while (iter.MoveNext()) {
            if(!res.Contains(iter.Current))
                res.Add(iter.Current);
        }
        return res;
    }
    static IEnumerable Limit(IEnumerable src, int size) {
        IList res = new ArrayList();
        IEnumerator iter = src.GetEnumerator();
        for (int i = 0; iter.MoveNext() && i < size; i++)
        {
            res.Add(iter.Current);
        }
        return res;
    }

    /**
     * Representa o domínio e o cliente App
     */
 
    static void Main()
    {
        IEnumerable names =
            Limit(
                Convert(                   // Seq<String>
                    Filter(                // Seq<Student>
                        Filter(            // Seq<Student>
                            Convert(       // Seq<Student> 
                                Lines("i41d.txt"),
                                new MapToStudent()),  // Seq<String>
                            new PredicateNrGreaterThan(46040)), 
                        new PredicateNameStartWith("J")),
                    new MapToFirstName()
                ),
            3);
    
        foreach(object l in names)
            Console.WriteLine(l);
    }
}

class MapToStudent : Function {
    public object Invoke(object item) { return Student.Parse(item.ToString()); }
}
class MapToFirstName : Function {
    public object Invoke(object item) { return ((Student) item).name.Split(' ')[0]; }
}
class PredicateNrGreaterThan : Predicate {
    readonly int nr;
    public PredicateNrGreaterThan(int nr) { this.nr = nr; }
    public bool Invoke(object item) { return ((Student) item).nr > nr; }
}
class PredicateNameStartWith : Predicate {
    readonly string prefix;
    public PredicateNameStartWith(string prefix) { this.prefix = prefix; }
    public bool Invoke(object item) { return ((Student) item).name.StartsWith(prefix); }
}

public class Student
{
    public readonly int nr;
    public readonly string name;
    public readonly int group;
    public readonly string email;
    public readonly string githubId;

    public Student(int nr, String name, int group, string email, string githubId)
    {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.email = email;
        this.githubId = githubId;
    }

    public override String ToString()
    {
        return String.Format("{0} {1} ({2}, {3})", nr, name, group, githubId);
    }
    public void Print()
    {
        Console.WriteLine(this.ToString());
    }
    
    public static Student Parse(string src){
        string [] words = src.Split('|');
        return new Student(
            int.Parse(words[0]),
            words[1],
            int.Parse(words[2]),
            words[3],
            words[4]);
    }
}
