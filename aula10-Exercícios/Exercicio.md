Na continuação do exercício da aula 9 implemente uma nova classe `Comparator`
que usa como critério de comparação os valores dos campos que
verifiquem uma das condições:
*	Ser do tipo `IComparable`. 
*	Estar anotados com o custom attribute `ComparisonAttribute`.

O atributo `ComparisonAttribute` recebe o critério de comparação por parâmetro
correspondente ao tipo de uma classe que implementa a interface `IComparer`.
Exemplo:

```csharp
class Student {
  int nr;             // int é IComparable
  string name;        // string é IComparable
  [Comparison(typeof(AdressByStreet))] Address a; // Address NÃO é IComparable
  [Comparison(typeof(AccountByBalance))] Account acc; // Account NÃO é IComparable
  ...
}
```

Neste caso, a classe `AdressByStreet` define um critério de comparação com o comportamento:

```csharp
((Address) x).street.CompareTo(((Address) y).street)
```

Considere o exemplo dos seguintes resultados de comparação de instâncias de `Student`:

```csharp
Student s1 = new Student(14000, "Ana", new Adress("Rua Amarela", 24), new Account(9900));
Student s2 = new Student(14000, "Ana", new Adress("Rua Rosa", 24), new Account(9900));
Student s3 = new Student(14000, "Ana", new Adress("Rua Rosa", 24), new Account(100));
Student s4 = new Student(14000, "Ana", new Adress("Rua Rosa", 97), new Account(100));
IComparer cmp = new Comparer(typeof(Student)); 
int res1 = cmp.Compare(s1, s2); // res < 0 porque Rua Amarela é menor que Rua Rosa
int res2 = cmp.Compare(s2, s3); // res > 0 porque 9900 é maior que 100
int res3 = cmp.Compare(s3, s4); // res = 0 porque os critérios de comparação de todas as propriedades dão 0 
```
