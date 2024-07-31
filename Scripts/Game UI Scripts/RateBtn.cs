using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateBtn : MonoBehaviour
{
    public void Rate()
    {
        // "market" works for android  (iOS: put your app link
        Application.OpenURL("market://details?id=com.KARNA.BOOMMachade");
    }
}
