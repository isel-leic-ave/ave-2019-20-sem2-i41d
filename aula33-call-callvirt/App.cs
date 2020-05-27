using System;

public class Steve : Bob { 
   public override void f() { 
       Console.WriteLine("I am Steve");
   } 
}
public class Bob { 
   public virtual void f() { 
        Console.WriteLine("I am Bob");
        Console.WriteLine(this.GetHashCode());
   } 
   static public void UseBob(int n, Bob b) { 
      b.f(); 
   } 
} 

class App {
    static void Main(){
        Bob b = new Bob();
        b.f();
        b = null;
        b.f();
    }
}







