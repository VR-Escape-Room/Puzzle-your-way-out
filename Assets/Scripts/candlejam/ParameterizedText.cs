using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterizedText : UnityEngine.UI.Text {

    private bool sFirstTime = true;
    private string sOriginalText = "";

    public void SetOriginalText(string s)
    {
        sOriginalText = s;
    }

    public void SetTextParameters(params object[] args)
    {
        if (sFirstTime)
        {
            sOriginalText = base.text;
            sFirstTime = false;
        }

        base.text = string.Format(sOriginalText, args);
    }
}
