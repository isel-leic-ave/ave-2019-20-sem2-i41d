using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

interface IGetter {
    string GetName();
    object call(object target);
}

class GetterMethod() : IGetter {
    readonly MethodInfo m; // readonly <=> final
    public GetterMethod(MethodInfo m) {this.m = m;}
    public string GetName() { return m.Name; }
    public object call(object target) { return m.Invoke(target, null); }
}

public class Logger {

    static Dictionary<Type, List<IGetter>> cache = new  Dictionary<Type, List<IGetter>>();

    public static void Log(object target) {
        Type klass = target.GetType();
        Console.Write(klass.Name);
        if(klass.IsArray) LogArray(klass, target);
        else LogObject(klass, target);
    }
    static void LogArray(Type klass, object target) {
        IEnumerable src = (IEnumerable) target;
        Console.WriteLine("[");
        foreach(object item in src) // <=> for(IEnumerator iter = src.GetEnumerator(); iter.MoveNext(); )
            LogObject(klass.GetElementType(), item); // item <=> iter.Current
        Console.WriteLine("]");
    }
    static void LogObject(Type klass, object target) {
        Console.Write("{");
        LogFields(klass, target);
        LogParameterlessMethods(klass, target);
        Console.WriteLine("}");
    }

    static void LogParameterlessMethods(Type klass, object target) {
        foreach (MethodInfo m in CheckMethodsOf(klass)) {
            Console.Write(" " + m.Name);
            Console.Write(": ");
            Console.Write(m.Invoke(target, null));
            Console.Write(",");
        }
    }
    static void LogFields(Type klass, object target) {
        foreach (FieldInfo f in klass.GetFields())
        {
            Console.Write(" " + f.Name);
            Console.Write(": ");
            Console.Write(f.GetValue(target));
            Console.Write(",");
        }
    }

    static List<IGetter> CheckMethodsOf(Type klass) {
        List<IGetter> ms;
        if(cache.TryGetValue(klass, out ms))
            return ms;
        ms = new List<IGetter>();
        foreach (MethodInfo m in klass.GetMethods()) {
            if(m.GetParameters().Length == 0 && m.ReturnType != typeof(void)) {
                ms.Add(....);
            }
        }
        cache.Add(klass, ms); // cache[klass] = ms
        return ms;
    }

}