using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParameterizedTMPUGUI : TextMeshProUGUI {

    private bool sFirstTime = true;
    protected string sOriginalText = "";

    public void SetOriginalText(string s)
    {
        sOriginalText = s;
    }

    public virtual void SetTextParameters(params object[] args)
    {
        if (sFirstTime)
        {
            sOriginalText = base.text;
            sFirstTime = false;
        }

        base.text = string.Format(sOriginalText, args);
    }
}
