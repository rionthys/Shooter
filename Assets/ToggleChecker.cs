using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleChecker : MonoBehaviour
{
    public bool isFullScreen = false;
    
    private void ChangeCheck(){
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
}
