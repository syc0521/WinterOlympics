using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManController : MonoBehaviour
{

    void Update()
    {
        float z = Mathf.PingPong(Time.timeSinceLevelLoad * 13, 18) - 9.0f;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
