using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choreographer : MonoBehaviour
{
    public GameObject StartDialog;
    public GameObject PrimaryHUD;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        StartDialog.SetActive(true);
        PrimaryHUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClicked()
    {
        StartDialog.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerController.enabled = true;
        PrimaryHUD.SetActive(true);
    }
}
