using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CloseDialogUtility
{
    public static void CloseDialogIn(this GameObject source, float delay)
    {
        foreach (MonoBehaviour comp in source.GetComponents<MonoBehaviour>())
        {
            if (comp.isActiveAndEnabled)
            {
                comp.StartCoroutine(Execute_CloseDialogIn(source, delay));
                return;
            }
        }
    }
    private static IEnumerator Execute_CloseDialogIn(GameObject window, float delay)
    {
        yield return new WaitForSeconds(delay);

        window.SetActive(false);
    }


}
