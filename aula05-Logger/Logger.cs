using System;
using System.Reflection;

public class Logger {

    public static void Log(object target) {
        Type klass = target.GetType();
        Console.Write(klass.Name);
        Console.Write("{");
        LogFields(klass, target);
        LogParameterlessMethods(klass, target);
        Console.WriteLine("}");
    }

    static void LogParameterlessMethods(Type klass, object target) {
        MethodInfo[] ms = klass.GetMethods();
        if(ms.Length == 0) return;
        foreach (MethodInfo m in ms)
        {
            if(m.GetParameters().Length == 0 && m.ReturnType != typeof(void)) {
                Console.Write(" " + m.Name);
                Console.Write(": ");
                Console.Write(m.Invoke(target, null));
                Console.Write(",");
            }
        }
    }
    static void LogFields(Type klass, object target) {
        FieldInfo[] fs = klass.GetFields();
        if(fs.Length == 0) return;
        foreach (FieldInfo f in fs)
        {
            Console.Write(" " + f.Name);
            Console.Write(": ");
            Console.Write(f.GetValue(target));
            Console.Write(",");
        }
    }
}