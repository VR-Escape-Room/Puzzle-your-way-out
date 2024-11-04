using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtility
{
    public static Color FromHexCode(int code)
    {
        int red = (code & 0xff0000) >> 16;
        int green = (code & 0x00ff00) >> 8;
        int blue = code & 0x0000ff;
        return new Color((float)red/255f, (float)green/255f, (float)blue/255f);
    }
}
