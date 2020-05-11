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

    class EnumerableMapper : IEnumerable, IEnumerator {
        private readonly IEnumerable src;
        private IEnumerator iter;
        private readonly Function mapper;
        public EnumerableMapper(IEnumerable src, Function mapper) {
            this.src = src;
            this.mapper = mapper;
        }
        public IEnumerator GetEnumerator() {
            this.iter = src.GetEnumerator();
            return this;
        }
        public bool MoveNext() { return iter.MoveNext(); }
        public object Current { get { return mapper.Invoke(iter.Current); }}
        public void Reset() { iter.Reset(); }
    }
    class EnumerableFilter : IEnumerable, IEnumerator {
        private readonly IEnumerable src;
        private IEnumerator iter;
        private readonly Predicate pred;
        private object current;
        public EnumerableFilter(IEnumerable src,  Predicate pred) {
            this.src = src;
            this.pred = pred;
        }
        public IEnumerator GetEnumerator() {
            this.iter = src.GetEnumerator();
            return this;
        }
        public bool MoveNext() { 
            while(iter.MoveNext()) {
                current = iter.Current;
                if(pred.Invoke(current))
                    return true;
            }
            current = null;
            return false; 
        }
        public object Current { get { return current; }}
        public void Reset() { 
            current = null;
            iter.Reset(); 
        }
    }
     
    static IEnumerable Convert(IEnumerable src, Function mapper) {
        return new EnumerableMapper(src, mapper);
    }
    
    static IEnumerable Filter(IEnumerable src, Predicate pred) {
        return new EnumerableFilter(src, pred);
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
                                item => Student.Parse(item.ToString())),  // Seq<String>
                            item => ((Student) item).nr > 46040), 
                        item => ((Student) item).name.StartsWith("J")),
                    item => ((Student) item).name.Split(' ')[0]),
                3);
    
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
