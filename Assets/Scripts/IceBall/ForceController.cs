using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceController : MonoBehaviour
{
    public bool showForce = false;
    public Image forceImage;
    public GameObject forceCanvas;
    void Start()
    {
        
    }

    void Update()
    {
        float force = IB_Round.force;
        forceImage.fillAmount = Mathf.Lerp(0, 1, Mathf.InverseLerp(900, 1350, force));
    }

    public void ShowForce()
    {
        forceCanvas.SetActive(true);
    }

    public void HideForce()
    {
        forceCanvas.SetActive(false);
    }
}
