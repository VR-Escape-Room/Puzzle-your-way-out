using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GeoStateSwapper that holds a set of states. It can receive an "Advance" event which advances it to the next state.
// Each time it changes state, it can toggle a list of geos on and off. 
public class GeoStateSwapper : MonoBehaviour
{
    public int CurrentState=0;
    public GameObject[] StateObjects = null;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach(GameObject go in StateObjects)
            go.SetActive(false);

        if (StateObjects.Length > CurrentState)
            StateObjects[CurrentState].SetActive(true);
    }

    public void Activate()
    {
        StateObjects[CurrentState].SetActive(false);
        CurrentState++;
        if (CurrentState >= StateObjects.Length)
            CurrentState = 0;

        StateObjects[CurrentState].SetActive(true);
    }

}
