using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    int width = 1920;
    int height = 1080;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(width, height, true);
    }
}
