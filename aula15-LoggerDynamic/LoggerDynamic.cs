using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public interface IGetter {
    string GetName();
    object Call(object target);
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class ToLogAttribute : Attribute { }

public class LoggerDynamic {

    static Dictionary<Type, List<IGetter>> cache = new  Dictionary<Type, List<IGetter>>();

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
                IGetter getter = NewGetterField(klass, f);
                ms.Add(getter);
            }
        }
        cache.Add(klass, ms); // cache[klass] = ms
        return ms;
    }
    public static IGetter NewGetterField(Type klass, FieldInfo f) {
        Type getter = BuildType(klass, f);
        return (IGetter) Activator.CreateInstance(getter);
    }

    public static Type BuildType(Type klass, FieldInfo f) {
        // To DO
        return null;
    }
}