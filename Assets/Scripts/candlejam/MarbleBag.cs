using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBag<T>
{
    private IEnumerable<T> fullListing;
    private List<T> current = new List<T>();

    public MarbleBag(IEnumerable<T> contents)
    {
        fullListing = contents;
    }

    public void Reset()
    {
        current.Clear();
        foreach(T t in fullListing)
        {
            current.Insert(Random.Range(0, current.Count + 1), t);
        }
    }

    public T Next
    {
        get
        {
            if (current.Count == 0)
            {
                Reset();
            }

            T result = current[current.Count - 1];
            current.RemoveAt(current.Count - 1);

            return result;
        }
    }
}
