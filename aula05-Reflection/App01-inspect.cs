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
                Console.Write(t.Name);
                PrintInheritance(t);
                Console.WriteLine();
                PrintMethods(t);
            }
        }
    }
    static void PrintInheritance(Type klass) {
        klass = klass.BaseType;
        while(klass != null && klass != typeof(object)) {
            PrintInterfaces(klass);
            Console.Write(" --|> " + klass.Name);
            klass = klass.BaseType;
        }
    }
    static void PrintInterfaces(Type klass) {
        Type[] itfs = klass.GetInterfaces();
        if(itfs.Length == 0) return;
        Console.Write("(");
        foreach(Type itf in itfs)
            Console.Write(itf.Name);
        Console.Write(")");
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
        ParameterInfo[] parameters = m.GetParameters();
        if (parameters.Length == 0) return;
        Console.Write("(");
        foreach (ParameterInfo p in parameters)
        {
            Console.Write(p.Name + ":" + p.ParameterType.Name + " ");
        }
        Console.Write(")");
    }
}