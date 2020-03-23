using System; 

class Student {
    public Student(int nr, string name, School school, string nationality) {
        this.Nr = nr;
        this.Name = name;
        this.School = school;
        this.Nationality = nationality;
    }
    public int Nr { get; }
    public string Name { get; }
    public School School { get; }
    public string Nationality { get; }
}

class School {
    public School(string name, string location) {
        this.Name = name;
        this.Location = location;
    }
    public string Name { get; }
    public string Location { get; }
    public override string ToString() { return Name + " (" + Location + ")"; }
}
class App {
    public static void Main() 
    {
        School school = new School("ISEL", "Chelas");
        Student s1 = new Student(46049, "Pedro", school, "Portuguese");
        Student s2 = new Student(46065, "José", school, "Portuguese");
        IEquality eq1 = new Equality(typeof(Student), "Nr", "Name", "School");
        bool res1 = eq1.AreEqual(s1, s2);
        IEquality eq2 = new Equality(typeof(Student), "School", "Nationality");
        bool res2 = eq2.AreEqual(s1, s2);
        Console.WriteLine(res1);
        Console.WriteLine(res2);
    }
}