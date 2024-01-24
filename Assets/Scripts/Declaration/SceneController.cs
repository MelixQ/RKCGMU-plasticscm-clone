using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _endingCanvas;
    [SerializeField] private AudioSource _correctSolutionSound;
    [SerializeField] private AudioSource _endingSolutionSound;

    public bool f1;
    public bool f2;

    public bool f3;
    public bool f4;

    private int count;

    public GameObject canv1;
    public GameObject canv2;
    public GameObject canv3;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        canv1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 1)
        {
            if (f1 & f2 & f3 & f4) { Next(canv1, canv2); }
        }
        if (count == 1)
        {
            if (f1 & f2 & f3 & f4) { Next(canv2, canv3); }
        }
        if (f1 & f2 & f3 & f4) 
        {
            End();
        }
    }

    private void Next(GameObject canv1, GameObject canv2)
    {
        f1 = f2 = f3 = f4 = false;
        count++;
        canv1.SetActive(false);
        canv2.SetActive(true);
        _correctSolutionSound.Play();
    }

    public void End() 
    {
        _endingCanvas.SetActive(true);
        //_endingSolutionSound.Play();
    }
}
