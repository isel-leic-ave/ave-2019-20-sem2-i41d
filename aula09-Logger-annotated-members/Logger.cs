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

class  GetterFormatter : IGetter {
    readonly IGetter getter;
    readonly LogFormatterAttribute attr;
    public GetterFormatter(IGetter getter, LogFormatterAttribute attr) {
        this.getter = getter;
        this.attr = attr;
    }
    public string GetName() { return getter.GetName(); }
    public object Call(object target) { 
        object val = getter.Call(target);
        return attr.Format(val);
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class ToLogAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public abstract class LogFormatterAttribute : Attribute { 

    public abstract object Format(object val);
}


class GetterField : IGetter {
    readonly FieldInfo f; // readonly <=> final
    public GetterField(FieldInfo f) { this.f = f; }
    public string GetName() { return f.Name; }
    public object Call(object target) { return f.GetValue(target); }
}

public class Logger {

    static Dictionary<Type, List<IGetter>> cache = new  Dictionary<Type, List<IGetter>>();

    static IGetter CheckToFormatter(MemberInfo member, IGetter getter) {
        LogFormatterAttribute attr = (LogFormatterAttribute) member.GetCustomAttribute(typeof(LogFormatterAttribute));
        return attr != null
                ? new GetterFormatter(getter, attr)
                : getter;

    }
    
    static bool CheckToLog(MemberInfo member) {
        return member.IsDefined(typeof(ToLogAttribute));
    }
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
            if (CheckToLog(f)) {
                IGetter getter = new GetterField(f);
                ms.Add(CheckToFormatter(f, getter));
            }
        }
        foreach (PropertyInfo p in klass.GetProperties()) {
            if (CheckToLog(p)) {
                IGetter getter = new GetterProperty(p);
                ms.Add(CheckToFormatter(p, getter));
            }
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