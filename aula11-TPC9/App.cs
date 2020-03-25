using System;
using System.Reflection;
using System.Collections;

public class AddressByStreet : IComparer
{
    public int Compare(object x, object y)
    {
        return ((Address) x).street.CompareTo(((Address) y).street);
    }
}

public class AccountByBalance : IComparer
{
    public int Compare(object x, object y)
    {
        return ((Account) x).balance - ((Account) y).balance;
    }
}

class Student
{
    public int nr;             // int é IComparable
    public string name;        // string é IComparable
    [Comparison(typeof(AddressByStreet))] public Address a; // Address NÃO é IComparable
    [Comparison(typeof(AccountByBalance))] public Account acc; // Account NÃO é IComparable
    public Student(int nr, string name, Address a, Account acc)
    {
        this.nr = nr;
        this.name = name;
        this.a = a;
        this.acc = acc;
    }
}

class Account
{
    public int balance;
    public Account(int n)
    {
        this.balance = n;
    }
}

class Address
{
    public string street;
    public int num;
    public Address(string s, int n)
    {
        this.street = s;
        this.num = n;
    }
}


class App
{
    static void Main()
    {
        Student s1 = new Student(14000, "Ana", new Address("Rua Amarela", 24), new Account(9900));
        Student s2 = new Student(14000, "Ana", new Address("Rua Rosa", 24), new Account(9900));
        Student s3 = new Student(14000, "Ana", new Address("Rua Rosa", 24), new Account(100));
        Student s4 = new Student(14000, "Ana", new Address("Rua Rosa", 97), new Account(100));

        IComparer cmp = new Comparer(typeof(Student));
        int res1 = cmp.Compare(s1, s2); // res < 0 porque Rua Amarela é menor que Rua Rosa
        int res2 = cmp.Compare(s2, s3); // res > 0 porque 9900 é maior que 100
        int res3 = cmp.Compare(s3, s4); // res = 0 porque os critérios de comparação de todas as propriedades dão 0
        Console.WriteLine(res1);
        Console.WriteLine(res2);
        Console.WriteLine(res3);
    }

}