using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWear : MonoBehaviour
{
    [SerializeField] private GameObject _endingCanvas;
    [SerializeField] private AudioSource _incorrectSolutionSound;
    [SerializeField] private AudioSource _correctSolutionSound;
    private AudioSource _endingSound;

    public bool head;
    public bool body;
    public bool leg;
    public bool shose;

    private void Awake()
    {
        _endingSound = _endingCanvas.GetComponent<AudioSource>();
    }

    public void checkB() 
    {
        body = true;
    }

    public void isTrueMan() 
    {
        if (head & body & leg & shose)
        {
            _correctSolutionSound.Play();
            _endingCanvas.SetActive(true);
            _endingSound.PlayDelayed(.5f);
        }
        else
            _incorrectSolutionSound.Play();
    }

    public void isTrueWoman()
    {
        if (head & body & leg & shose)
        {
            _correctSolutionSound.Play();
            _endingCanvas.SetActive(true);
            _endingSound.PlayDelayed(.5f);
        }
        else
            _incorrectSolutionSound.Play();
    }

    public bool IsTrue_man() 
    {
        return head & body & leg & shose;
    }

    public bool IsTrue_woman()
    {
        return body & shose;
    }
}
