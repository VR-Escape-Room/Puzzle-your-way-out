using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDUpdater : MonoBehaviour
{
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject InteractionHUDElements = null;
    public GameObject HintHUDElements = null;

    // Update is called once per frame
    void Update()
    {
        InteractionHUDElements.SetActive(playerController.LookingAtInteractible);
        HintHUDElements.SetActive(playerController.LookingAtHint);
    }
}
