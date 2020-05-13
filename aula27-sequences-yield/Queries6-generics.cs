using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;


public delegate bool Predicate<T>(T target);
public delegate R Function<T, R>(T target);

class App {

    static IEnumerable<String> Lines(string path)
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
    
    static IEnumerable<R> Convert<T,R>(IEnumerable<T> src, Function<T,R> mapper) {
        IEnumerator<T> iter = src.GetEnumerator();
        while (iter.MoveNext())
        {
            yield return mapper.Invoke(iter.Current);
        }
    }
    
    static IEnumerable<T> Filter<T>(IEnumerable<T> src, Predicate<T> pred) {
        IEnumerator<T> iter = src.GetEnumerator();
        while (iter.MoveNext())
        {
            if(pred.Invoke(iter.Current))
                yield return iter.Current;
        }
    }
    static IEnumerable<T> Distinct<T>(IEnumerable<T> src) {
        List<T> res = new List<T>();
        IEnumerator<T> iter = src.GetEnumerator();
        while (iter.MoveNext()) {
            if(!res.Contains(iter.Current)) {
                res.Add(iter.Current);
                yield return iter.Current;
            }
        }
    }
    static IEnumerable<T> Limit<T>(IEnumerable<T> src, int size) {
        IEnumerator<T> iter = src.GetEnumerator();
        for (int i = 0; iter.MoveNext() && i < size; i++)
        {
            yield return iter.Current;
        }
    }

    static IEnumerable<T> Skip<T>(IEnumerable<T> src, int first) {
        IEnumerator<T> iter = src.GetEnumerator();
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
                                    return Student.Parse(item);
                                }),  // Seq<String>
                            item => item.nr > 46040), 
                        item => item.name.StartsWith("J")),
                    item => item.name.Split(' ')[0]),
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
