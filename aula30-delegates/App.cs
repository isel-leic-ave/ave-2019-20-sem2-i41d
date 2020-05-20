using System;

delegate bool Predicate(object item);
delegate string StringSupplier();

class C {
    readonly string name;
    public C(string name) { this.name = name; }
    public override bool Equals(Object other) {
        C c = other as C;
        if(c == null) return false;
        return name.Equals(c.name); 
    }
}

class App {
    static String Foo() { return "ola"; }
    static void Bar(Predicate p) {}
    static void Main() {
        C obj = new C("ABC"); 

        // StringSupplier f1 = App.Foo; // ldnull + ldftn App:: Foo + newobj StringSupplier

        /**
         * Is there any difference in IL from the compilation of f1 and f2 assignment?
         * R: yes
         * Which instruction is different?
         * f1: ldftn      App/'<>c__DisplayClass2_0'::'<Main>b__0' // anymous method containing the lambda body
         * f2: ldvirtftn  Object::ToString()
         */
        StringSupplier f1 = () => obj.ToString(); // Anonymous method, Lambda
        StringSupplier f2 = obj.ToString;         // Method Reference <=> Java> obj::ToString
        // f1() == f2() // f1() two jumps and f2() one jump

        StringSupplier f3 = new StringSupplier(obj.ToString); // <=> f2

        Bar(obj.Equals); // ldloc0 + dup + ldvirtftn C::Equals + newobj Predicate + call App::Bar

        string s1 = f2.Invoke();
        string s2 = f2(); // <=> f2.Invoke()
    }
}
