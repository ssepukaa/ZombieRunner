using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour
{
    float fps = 0f;
    string color = "yellow";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        fps = 1.0f/Time.deltaTime;
        
        if (fps > 35f) { fps = 35f; }
        if (fps<25f) { fps = 25f; }
        if (fps>25f && fps <35f) { fps = 30f; }
       
        if (fps<25) {color="red";}
        if (fps>35) { color ="blue";}
        if (fps>25f && fps <35f) { color="green"; }

    }
    void OnGUI()
    {

       

        GUILayout.Label("FPS = " + (int) fps, color);
    }
  
}

