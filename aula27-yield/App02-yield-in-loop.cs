using System;
using System.Collections;

class Program {

    static void Main() {
        Console.WriteLine("Calling Number() first time!");
        IEnumerator nrs = Numbers(7, 11).GetEnumerator();
        Console.ReadLine();
        nrs.MoveNext();
        Console.WriteLine(nrs.Current);
        Console.ReadLine();
        nrs.MoveNext();
        Console.WriteLine(nrs.Current);
        Console.WriteLine("Calling Number() second time!");
        nrs = Numbers(23, 27).GetEnumerator();
        Console.ReadLine();
        nrs.MoveNext();
        Console.WriteLine(nrs.Current);
        
    }
    static IEnumerable Numbers(int from, int to) {
        Console.WriteLine("Iteration started...");
        for(int i = from; i <= to; i++) {
            yield return i;
        }
    }
}