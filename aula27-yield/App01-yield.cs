using System;
using System.Collections;

class Program {

    static void Main() {
        Console.WriteLine("Calling Number() first time!");
        IEnumerator nrs = Numbers().GetEnumerator();
        Console.ReadLine();
        nrs.MoveNext();
        Console.WriteLine(nrs.Current);
        Console.ReadLine();
        nrs.MoveNext();
        Console.WriteLine(nrs.Current);
        Console.WriteLine("Calling Number() second time!");
        nrs = Numbers().GetEnumerator();
        Console.ReadLine();
        nrs.MoveNext();
        Console.WriteLine(nrs.Current);
        
    }
    static IEnumerable Numbers() {
        Console.WriteLine("Iteration started...");
        yield return 11;
        Console.WriteLine("Computing the 2nd element ...");
        yield return 17;
        Console.WriteLine("Computing the 3rd element ...");
        yield return 23;
    }
}