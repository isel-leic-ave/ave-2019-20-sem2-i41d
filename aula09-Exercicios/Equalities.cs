using System;
using System.Reflection;


public interface IEquality
{
    bool AreEqual(object x, object y);
}

public class Equality : IEquality {

    readonly Type klass;
    PropertyInfo[] propertys;
                                //String... values
    public Equality(Type klass, params string[] values) {
        this.klass = klass;
        propertys = new PropertyInfo[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            propertys[i] = klass.GetProperty(values[i]);
            if(propertys[i] == null)
                throw new ArgumentException("There is no property with name " + values[i]);
        }
    }

    public bool AreEqual(object x, object y) {
        if (x.GetType() != this.klass || y.GetType() != this.klass) 
        {
            return false;
        }
        for (int i = 0; i < propertys.Length; i++)
        {
            if (!propertys[i].GetValue(x).Equals(propertys[i].GetValue(y)))
            {
                return false;
            }
        }
        return true;
    }
}