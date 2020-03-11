using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

interface IGetter {
    string GetName();
    object Call(object target);
}

class GetterMethod : IGetter {
    readonly MethodInfo m; // readonly <=> final
    public GetterMethod(MethodInfo m) {this.m = m;}
    public string GetName() { return m.Name; }
    public object Call(object target) { return m.Invoke(target, null); }
}
class GetterProperty : IGetter {
    readonly PropertyInfo p; 
    public GetterProperty( PropertyInfo p) {this.p = p;}
    public string GetName() { return p.Name; }
    public object Call(object target) { 
        // return p.GetGetValue().Invoke(target, null);
        return p.GetValue(target); 
    }
}

class GetterField : IGetter {
    readonly FieldInfo f; // readonly <=> final
    public GetterField(FieldInfo f) { this.f = f; }
    public string GetName() { return f.Name; }
    public object Call(object target) { return f.GetValue(target); }
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
        LogMembers(klass, target);
        Console.WriteLine("}");
    }

    static void LogMembers(Type klass, object target) {
        List<IGetter> ms = CheckMembersOf(klass);
        foreach (IGetter m in ms) {
            Console.Write(" " + m.GetName());
            Console.Write(": ");
            Console.Write(m.Call(target));
            Console.Write(",");
        }
    }
    
    static List<IGetter> CheckMembersOf(Type klass) {
        List<IGetter> ms;
        if(cache.TryGetValue(klass, out ms))
            return ms;
        ms = new List<IGetter>();
        foreach (FieldInfo f in klass.GetFields()) {
            ms.Add(new GetterField(f));
        }
        foreach (PropertyInfo p in klass.GetProperties()) {
            ms.Add(new GetterProperty(p));
        }
        /*
        foreach (MethodInfo m in klass.GetMethods()) {
            if(m.GetParameters().Length == 0 && m.ReturnType != typeof(void)) {
                ms.Add(new GetterMethod(m));
            }
        }*/

        cache.Add(klass, ms); // cache[klass] = ms
        return ms;
    }

}