using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] Text text;
    float deltaTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        float fps = 1.0f / deltaTime;
        text.text = (Mathf.Round(fps)).ToString();
    }
}
