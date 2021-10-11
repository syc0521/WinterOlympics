using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource source;
    public bool IsActive
    {
        get => isActive;
        set
        {
            isActive = value;
            StartCoroutine(FadeAudio());
        }
    }
    private bool isActive;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private IEnumerator FadeAudio()
    {
        int multiply = 1;
        int flag = IsActive ? 1 : -1;
        while (IsActive ? source.volume < 1.0f : source.volume > 0.2f)
        {
            source.volume += 0.00156f * multiply * flag;
            multiply++;
            yield return null;
        }
    }
}
