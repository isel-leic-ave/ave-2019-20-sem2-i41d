using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

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
        AssemblyName ass = new AssemblyName("GetterFld" + klass.Name + f.Name);

        AssemblyBuilder builder = 
                AppDomain.CurrentDomain.DefineDynamicAssembly(ass, 
                AssemblyBuilderAccess.RunAndSave);

        ModuleBuilder mb = builder.DefineDynamicModule(ass.Name, ass.Name + ".dll");

        TypeBuilder tb = mb.DefineType(ass.Name,  TypeAttributes.Public);   
        tb.AddInterfaceImplementation(typeof(IGetter));
        // cria a classe GetterFldStudentNr e adiciona a interface

        MethodBuilder meth = tb.DefineMethod(
            "GetName", 
            MethodAttributes.Public | MethodAttributes.Virtual,
            typeof(string),
            Type.EmptyTypes);
        tb.DefineMethodOverride(meth, typeof(IGetter).GetMethod("GetName"));

        ILGenerator methOne = meth.GetILGenerator();
        methOne.Emit(OpCodes.Ldstr, f.Name);
        methOne.Emit(OpCodes.Ret);
        //define a implementação do método GetName

        meth = tb.DefineMethod(
            "Call", 
            MethodAttributes.Public | MethodAttributes.Virtual,
            typeof(object),
            new Type[]{typeof(object)});
        tb.DefineMethodOverride(meth, typeof(IGetter).GetMethod("Call"));

        ILGenerator methTwo = meth.GetILGenerator();
        methTwo.Emit(OpCodes.Ldarg_1);
        if(klass.IsValueType)
            methTwo.Emit(OpCodes.Unbox_Any, klass);
        else
            methTwo.Emit(OpCodes.Castclass, klass);
        methTwo.Emit(OpCodes.Ldfld, f);
        if(f.FieldType.IsValueType)
            methTwo.Emit(OpCodes.Box, f.FieldType);
        methTwo.Emit(OpCodes.Ret);
        //define a implementação do método Call

        Type t = tb.CreateType();
        builder.Save(ass.Name + ".dll");
        return t;
    }
}