using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snow
{
    public class HitSound : MonoBehaviour
    {
        private AudioSource source;
        void Start()
        {
            source = GetComponent<AudioSource>();
            //source.volume = NoteController.hitVolume;
            StartCoroutine(DestroyAudio());
        }
        private IEnumerator DestroyAudio()
        {
            yield return new WaitForSeconds(source.clip.length);
            Destroy(gameObject);
        }
    }

}
