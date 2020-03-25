using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class ComparisonAttribute : Attribute
{
    public readonly Type klass;
    public ComparisonAttribute(Type klass)
    {
        if(!typeof(IComparer).IsAssignableFrom(klass) )
            throw new ArgumentException(klass + " must implement IComparer!");
        this.klass = klass;
    }
}

public class FieldComparer : IComparer
{
    public int Compare(object x, object y)
    {
        IComparable val1 = (IComparable)x;
        IComparable val2 = (IComparable)y;
        return val1.CompareTo(val2);
    }
}

public class Comparer : IComparer
{
    Type klass;
    Dictionary<FieldInfo, IComparer> dictionary;
    public Comparer(Type klass)
    {
        this.klass = klass;
        dictionary = searchFields(klass);
    }

    public int Compare(object x, object y)
    {
        if (x.GetType() != this.klass || y.GetType() != this.klass)
            throw new ArgumentException();

        foreach (KeyValuePair<FieldInfo, IComparer> entry in dictionary)
        {
            FieldInfo f = entry.Key;
            IComparer comparer = entry.Value;

            int diff = comparer.Compare(f.GetValue(x), f.GetValue(y));
            if (diff != 0)
                return diff;
        }
        return 0;
    }

    public Dictionary<FieldInfo, IComparer> searchFields(Type klass)
    {
        Dictionary<FieldInfo, IComparer> dictionary = new Dictionary<FieldInfo, IComparer>();
        FieldInfo[] fieldInfos = klass.GetFields();
        FieldComparer cmp = new FieldComparer();
        foreach (FieldInfo f in fieldInfos)
        {
            ComparisonAttribute attribute = (ComparisonAttribute)f.GetCustomAttribute(typeof(ComparisonAttribute));
            if (attribute != null)
            {
                dictionary.Add(f, (IComparer)Activator.CreateInstance(attribute.klass));
            }
            else if (typeof(IComparable).IsAssignableFrom(f.FieldType))
            {
                dictionary.Add(f, cmp);
            }
        }
        return dictionary;
    }
}

