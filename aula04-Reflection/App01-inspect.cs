using System;
using System.Reflection;

class Program {

    static void Main() {
        Inspect("RestSharp.dll");
    }
    static void Inspect(string path) {
        Assembly asm = Assembly.LoadFrom(path);
        Type[] types = asm.GetTypes();
        foreach (Type t in types) {
            if(t.IsPublic) {
                Console.WriteLine(t.Name);
                PrintMethods(t);
            }
        }
    }
    static void PrintMethods(Type klass) {
        MethodInfo[] methods = klass.GetMethods();
        foreach (MethodInfo m in methods)
        {
            Console.Write("     " + m.Name);
            PrintParameters(m);
            Console.WriteLine();
        }
    }
    static void PrintParameters(MethodInfo m) {
        /*
         * TO DO !
         */
    }
}