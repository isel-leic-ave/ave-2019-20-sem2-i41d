using System;
using System.Reflection;
using System.Reflection.Emit;

public class DemoAssemblyBuilder {

    public interface IMultiplies {
        int Mul(int arg);
    }

    static void Main(){
        // Type klass = BuildType();
        // object target = Activator.CreateInstance(klass, new object[]{17});
        // object val = klass.GetMethod("Mul").Invoke(target, new object[]{2}); // !!!!!!!!!! OVERHEAD

        IMultiplies target = BuildMultiplies(17);
        int val = target.Mul(2); // Internamente NÃO usa Reflexão! 
        Console.WriteLine(val);
    }

    static IMultiplies  BuildMultiplies(int val) {
        Type klass = BuildType();
        return (IMultiplies) Activator.CreateInstance(klass, new object[]{17});        
    }
    static Type BuildType() {
        AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
        AssemblyBuilder ab = 
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                aName, 
                AssemblyBuilderAccess.RunAndSave);

        // For a single-module assembly, the module name is usually
        // the assembly name plus an extension.
        ModuleBuilder mb = 
            ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
      
        TypeBuilder tb = mb.DefineType(
            "MyDynamicType", 
             TypeAttributes.Public);   
        tb.AddInterfaceImplementation(typeof(IMultiplies));

        // Add a private field of type int (Int32).
        FieldBuilder fbNumber = tb.DefineField(
            "m_number", 
            typeof(int), 
            FieldAttributes.Private);
        BuildCtor(tb, fbNumber);
        BuildMethodMul(tb, fbNumber);

        // Finish the type.
        Type t = tb.CreateType();
     
        // The following line saves the single-module assembly. This
        // requires AssemblyBuilderAccess to include Save. You can now
        // type "ildasm MyDynamicAsm.dll" at the command prompt, and 
        // examine the assembly. You can also write a program that has
        // a reference to the assembly, and use the MyDynamicType type.
        // 
        ab.Save(aName.Name + ".dll");
        return t;
    }
    static void BuildCtor(TypeBuilder tb, FieldInfo fld){
        // Define a constructor that takes an integer argument and 
        // stores it in the private field. 
        Type[] parameterTypes = { typeof(int) };
        ConstructorBuilder ctor1 = tb.DefineConstructor(
            MethodAttributes.Public, 
            CallingConventions.Standard, 
            parameterTypes);

        ILGenerator ctor1IL = ctor1.GetILGenerator();
        // For a constructor, argument zero is a reference to the new
        // instance. Push it on the stack before calling the base
        // class constructor. Specify the default constructor of the 
        // base class (System.Object) by passing an empty array of 
        // types (Type.EmptyTypes) to GetConstructor.
        ctor1IL.Emit(OpCodes.Ldarg_0);
        ctor1IL.Emit(OpCodes.Call, 
            typeof(object).GetConstructor(Type.EmptyTypes));
        // Push the instance on the stack before pushing the argument
        // that is to be assigned to the private field m_number.
        ctor1IL.Emit(OpCodes.Ldarg_0);
        ctor1IL.Emit(OpCodes.Ldarg_1);
        ctor1IL.Emit(OpCodes.Stfld, fld);
        ctor1IL.Emit(OpCodes.Ret);
    }
    static void BuildMethodMul(TypeBuilder tb, FieldInfo fld) {
        // Define a method that accepts an integer argument and returns
        // the product of that integer and the private field m_number. This
        // time, the array of parameter types is created on the fly.
        MethodBuilder meth = tb.DefineMethod(
            "Mul", 
            MethodAttributes.Public | MethodAttributes.Virtual, 
            typeof(int), 
            new Type[] { typeof(int) });
        tb.DefineMethodOverride(meth, typeof(IMultiplies).GetMethod("Mul"));

        ILGenerator methIL = meth.GetILGenerator();
        // To retrieve the private instance field, load the instance it
        // belongs to (argument zero). After loading the field, load the 
        // argument one and then multiply. Return from the method with 
        // the return value (the product of the two numbers) on the 
        // execution stack.
        methIL.Emit(OpCodes.Ldarg_0);    // push this
        methIL.Emit(OpCodes.Ldfld, fld); // this.m_number
        methIL.Emit(OpCodes.Ldarg_1);    // push arg
        methIL.Emit(OpCodes.Mul);        // this.m_number * arg
        methIL.Emit(OpCodes.Ret);        // return this.m_number * arg
    }
}





