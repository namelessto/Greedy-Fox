using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsHardMode : MonoBehaviour
{
    public Toggle toggle;
    public static bool hardMode = false;

    public void IsToggled(bool toggled)
    {
        hardMode = toggled;
    }
}
