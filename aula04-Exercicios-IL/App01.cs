using System;

class App
{
    static void Foo(int nr) {
        Console.Write(nr);
        Console.Write(", ");
        if(nr == 1) { Console.WriteLine(); }
        else if(nr%2 == 0) { Foo(nr/2); }
        else Foo(nr*3 + 1);
    }

    static void Main() {
        Console.Write("Insert a valid integer: ");
        string input = Console.ReadLine();
        int nr = int.Parse(input);
        Foo(nr);
    }
}