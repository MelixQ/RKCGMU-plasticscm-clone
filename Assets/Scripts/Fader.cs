using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeSpeed = 5f;
    private bool _isPlaying = false;

    public void FadeIn()
    {
        if (_isPlaying) return;
        _canvasGroup.gameObject.SetActive(true);
        StartCoroutine(BeginFadeIn());
    }

    public void FadeOut()
    {
        if (_isPlaying) return;
        StartCoroutine(BeginFadeOut());
    }

    private IEnumerator BeginFadeIn()
    {
        _isPlaying = true;
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += Time.deltaTime * _fadeSpeed;
            yield return null;
        }

        _isPlaying = false;
    }

    private IEnumerator BeginFadeOut()
    {
        _isPlaying = true;
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime * _fadeSpeed;
            yield return null;
        }
        
        _canvasGroup.gameObject.SetActive(false);
        _isPlaying = false;
    }
}
