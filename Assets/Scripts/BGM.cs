using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public bool canDestroy = false;
    private AudioSource source;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (canDestroy)
        {
            if (source.volume >= 0f)
            {
                source.volume -= 0.005f;
            }
        }
    }

    public IEnumerator DestroyAudio()
    {
        yield return new WaitForSeconds(0.65f);
        Destroy(gameObject);
    }
}
