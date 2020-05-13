using System;
using System.Collections;
using System.Text;
using System.IO;


public delegate bool Predicate(object target);
public delegate object Function(object target);

class App {

    static IEnumerable Lines(string path)
    {
        string line;
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
    
    static IEnumerable Convert(IEnumerable src, Function mapper) {
        IEnumerator iter = src.GetEnumerator();
        while (iter.MoveNext())
        {
            yield return mapper.Invoke(iter.Current);
        }
    }
    
    static IEnumerable Filter(IEnumerable src, Predicate pred) {
        IEnumerator iter = src.GetEnumerator();
        while (iter.MoveNext())
        {
            if(pred.Invoke(iter.Current))
                yield return iter.Current;
        }
    }
    static IEnumerable Distinct(IEnumerable src) {
        ArrayList res = new ArrayList();
        IEnumerator iter = src.GetEnumerator();
        while (iter.MoveNext()) {
            if(!res.Contains(iter.Current))
                yield return iter.Current;
        }
    }
    static IEnumerable Limit(IEnumerable src, int size) {
        IEnumerator iter = src.GetEnumerator();
        for (int i = 0; iter.MoveNext() && i < size; i++)
        {
            yield return iter.Current;
        }
    }

    static IEnumerable Skip(IEnumerable src, int first) {
        IEnumerator iter = src.GetEnumerator();
        for (int i = 0; iter.MoveNext(); i++)
        {
            if(first-- > 0)
                yield return iter.Current;
        }
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
                                item => { 
                                    Console.WriteLine("Parsing line... " + item);
                                    return Student.Parse(item.ToString());
                                }),  // Seq<String>
                            item => ((Student) item).nr > 46040), 
                        item => ((Student) item).name.StartsWith("J")),
                    item => ((Student) item).name.Split(' ')[0]),
                3);
        Console.WriteLine("Chaining pipeline !!!");
        Console.ReadLine();
        foreach(object l in names)
            Console.WriteLine(l);
    }
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
