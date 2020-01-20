using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimation : MonoBehaviour
{
    private bool endOfCoroutine = false;

    private float deltaTime = 0.0f;
    public Camera cam;
    public Color bgColor;
    private IEnumerator coroutine;
    void Start()
    {
        cam.backgroundColor = new Color(Random.value, Random.value, Random.value);
        StartNewCoroutine();
    }

    void Update()
    {
        if (endOfCoroutine) {
            StopCoroutine(coroutine);
            StartNewCoroutine();
        }
    }
    private IEnumerator bgColorShifter(float waitTime)
    {
        endOfCoroutine = false;
        while (!endOfCoroutine)
        {
            
            bgColor = new Color(Random.value, Random.value, Random.value);
            float t = 0f;
            Color currentColor = Camera.main.backgroundColor;
            while (t < 1.0)
            {
                Camera.main.backgroundColor = Color.Lerp(currentColor, bgColor, t);
                yield return new WaitForSeconds(waitTime); 
                t += Time.deltaTime /6;
            }
            
       }
        endOfCoroutine = true;
    }
    private void StartNewCoroutine()
    {
        coroutine = bgColorShifter(0);
        StartCoroutine(coroutine);
    }
}

