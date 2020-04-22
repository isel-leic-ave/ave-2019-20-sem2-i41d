using System;

class A {}
class B : A{}

struct Person {}
struct Teatcher : Person {} // !!!!! DÁ Erro de Compilação 

public struct Student {
    public readonly int nr;
    public readonly string name;
    public readonly int group;
    public readonly string githubId;
}

interface IGetter {
    string GetName();
    object Call(object target);
}

public class GetterFldStudentNr : IGetter {
    public string GetName(){ return "Nr"; }
    public object Call(object target) {
        Student st = (Student) target;
        return st.nr;
    }

}

static class App { static void Main() {}}