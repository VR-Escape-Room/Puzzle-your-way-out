using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlackboardExtensionMethods
{
    public static void SetBlackboardValue(this GameObject o, string key, object v)
    {
        Blackboard bb = o.GetComponent<Blackboard>();
        if (bb != null)
            bb.SetValue(key, v);
        else
            Debug.LogWarning("Tried to set " + key + " to " + v.ToString() + " on " + o.ToString() + " but it has no Blackboard component");

    }
    public static void SetBlackboardValue(this Component o, string key, object v)
    {
        Blackboard bb = o.GetComponent<Blackboard>();
        if (bb != null)
            bb.SetValue(key, v);
        else
            Debug.LogWarning("Tried to set " + key + " to " + v.ToString() + " on " + o.ToString() + " but it has no Blackboard component");
    }
    public static float GetBlackboardFloat(this GameObject o, string key)
    {
        Blackboard bb = o.GetComponent<Blackboard>();
        if (bb != null)
            return bb.GetFloatValue(key);
        else
            Debug.LogWarning("Tried to get " + key + " on " + o.ToString() + " but it has no Blackboard component");

        return 0.0f;
    }
    public static float GetBlackboardFloat(this Component o, string key)
    {
        Blackboard bb = o.GetComponent<Blackboard>();
        if (bb != null)
            return bb.GetFloatValue(key);
        else
            Debug.LogWarning("Tried to get " + key + " on " + o.ToString() + " but it has no Blackboard component");

        return 0.0f;
    }
    public static int GetBlackboardInt(this GameObject o, string key)
    {
        Blackboard bb = o.GetComponent<Blackboard>();
        if (bb != null)
            return bb.GetIntValue(key);
        else
            Debug.LogWarning("Tried to get " + key + " on " + o.ToString() + " but it has no Blackboard component");

        return 0;
    }
    public static int GetBlackboardInt(this Component o, string key)
    {
        Blackboard bb = o.GetComponent<Blackboard>();
        if (bb != null)
            return bb.GetIntValue(key);
        else
            Debug.LogWarning("Tried to get " + key + " on " + o.ToString() + " but it has no Blackboard component");

        return 0;
    }
}

public class Blackboard : MonoBehaviour {

    public Dictionary<string, object> Table = new Dictionary<string, object>();

    public object GetValue(string key)
    {
        if (Table.ContainsKey(key))
            return Table[key];

        return null;
    }

    public int GetIntValue(string key)
    {
        if (Table.ContainsKey(key))
            return (int) Table[key];

        return 0;
    }

    public float GetFloatValue(string key)
    {
        if (Table.ContainsKey(key))
            return (float)Table[key];

        return 0.0f;
    }

    public void SetValue(string key, object value)
    {
        Table[key] = value;
    }
}
