using System;
using System.Collections;
using System.Text;
using System.IO;


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
     
    static IEnumerable ConvertToStudents(IEnumerable lines) {
        IList res = new ArrayList();
        IEnumerator iter = lines.GetEnumerator();
        while (iter.MoveNext())
        {
            string line = (string) iter.Current;
            Student std = Student.Parse(line);
            res.Add(std);
        }
        return res;
    }
    
    static IEnumerable ConvertToFirstName(IEnumerable stds) {
        IList res = new ArrayList();
        IEnumerator iter = stds.GetEnumerator();
        while (iter.MoveNext())
        {
            Student std = (Student) iter.Current;
            res.Add(std.name.Split(' ')[0]);
        }
        return res;
    }
    
    static IEnumerable FilterWithNumberGreaterThan(IEnumerable stds, int nr) {
        IList res = new ArrayList();
        IEnumerator iter = stds.GetEnumerator();
        while (iter.MoveNext())
        {
            Student std = (Student) iter.Current;
            if(std.nr > nr)
                res.Add(std);
        }
        return res;
    }
    
    static IEnumerable FilterNameStartsWith(IEnumerable stds, String prefix) {
        IList res = new ArrayList();
        IEnumerator iter = stds.GetEnumerator();
        while (iter.MoveNext())
        {
            Student std = (Student) iter.Current;
            if(std.name.StartsWith(prefix) )
                res.Add(std);
        }
        return res;
    }
    
    /**
     * Representa o domínio e o cliente App
     */
 
    static void Main()
    {
        IEnumerable names = 
            ConvertToFirstName(                  // Seq<String>
                FilterNameStartsWith(            // Seq<Student>
                    FilterWithNumberGreaterThan( // Seq<Student>
                        ConvertToStudents(       // Seq<Student> 
                            Lines("i41d.txt")),  // Seq<String>
                        46040), 
                    "J")
                );
    
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
