using System.Collections;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        sprite.color = new Color(1, 1, 1, Mathf.PingPong(Time.timeSinceLevelLoad, 1));
    }
}
